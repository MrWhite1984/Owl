using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Persons.Interfaces;
using ConfHub.Core.Application.Users.Interfaces;
using ConfHub.Core.Contracts.Requests.Auth;
using ConfHub.Core.Contracts.Responses.Auth;
using ConfHub.Core.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ConfHub.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public AuthController(IPersonService personService, IUserService userService, ITokenService tokenService)
        {
            _personService = personService;
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var person = await _personService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);
            if (person == null)
                return BadRequest("Неверный email или пароль");

            var users = await _userService.GetUsersByPersonIdAsync(person.Id);
            var roles = users?.Select(u => u.Role).ToList() ?? new List<string>();

            if (!roles.Any())
                return BadRequest("У пользователя отсутствуют роли.");

            if (roles.Count() == 1)
            {
                var token = _tokenService.GenerateToken(person.Id, roles.First());
                return Ok(new LoginResponse(
                    Token: token, PersonId: person.Id, Role: roles.First()
                    ));
            }
            else
            {
                var token = _tokenService.GenerateToken(person.Id, roles.First());
                return Ok(new LoginResponse(PersonId: person.Id, Roles: roles, Token: token));
            }
        }

        [HttpPost("registration")]
        public async Task<ActionResult<RegistrationResponse>> Registration([FromBody] RegistrationRequest registrationRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newPerson = await _personService.AddAsync(
                    registrationRequest.Surname,
                    registrationRequest.Name,
                    registrationRequest.Patronymic,
                    registrationRequest.EducationalInstitution,
                    registrationRequest.JobTitle,
                    registrationRequest.City,
                    registrationRequest.Phone,
                    registrationRequest.Email,
                    registrationRequest.IsVerified,
                    false,
                    registrationRequest.ElibraryProfileUrl,
                    registrationRequest.Password);
                if (newPerson == null)
                    return BadRequest("Error while saving user");

                var anyUsers = await _userService.HasAnyUserAsync();
                var role = anyUsers ? Roles.User : Roles.Admin;
                await _userService.AddAsync(newPerson.Id, role);

                var token = _tokenService.GenerateToken(newPerson.Id, role);

                return Ok(new RegistrationResponse(newPerson.Id, token, role));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("change-role")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ChangeRoleResponse>> ChangeRole([FromBody]ChangeRoleRequest changeRoleRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var subClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (subClaim == null || !Guid.TryParse(subClaim.Value, out var currentPersonId))
                {
                    return BadRequest("Невозможно определить пользователя из токена.");
                }
                var users = await _userService.GetUsersByPersonIdAsync(currentPersonId);
                var userRoles = users.Select(o => o.Role).ToList();
                if (!userRoles.Any() || !userRoles.Contains(changeRoleRequest.Role))
                    return BadRequest("У вас нет доступа к этой роли.");

                var newToken = _tokenService.GenerateToken(currentPersonId, changeRoleRequest.Role);
                return Ok(new ChangeRoleResponse(currentPersonId, newToken, changeRoleRequest.Role));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
