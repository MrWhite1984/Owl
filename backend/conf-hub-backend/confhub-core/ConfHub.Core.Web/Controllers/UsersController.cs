using ConfHub.Core.Application.Users.Interfaces;
using ConfHub.Core.Contracts.Requests.Users;
using ConfHub.Core.Contracts.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace ConfHub.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add-role")]
        public async Task<ActionResult<AddUserResponse>> AddUser([FromBody] AddUserRequest addUserRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.AddAsync(addUserRequest.PersonId, addUserRequest.Role);
                return Ok(new AddUserResponse());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
