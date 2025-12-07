using ConfHub.Core.Application.Persons.Interfaces;
using ConfHub.Core.Contracts.Requests.Persons;
using ConfHub.Core.Contracts.Responses.Persons;
using ConfHub.Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConfHub.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("get-full-person-data-by-id/{id}")]
        [Authorize]
        public async Task<ActionResult<GetFullPersonDataResponse>> GetFullPersonDataById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var subClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var roleClaim = User.FindFirst(ClaimTypes.Role);
                if (subClaim == null || roleClaim == null || !Guid.TryParse(subClaim.Value, out var currentPersonId))
                {
                    return BadRequest("Невозможно определить пользователя из токена.");
                }
                if (roleClaim.Value.Equals(Roles.User) && !Guid.Parse(subClaim.Value).Equals(id))
                    return Forbid();

                var currentPerson = await _personService.GetPersonByIdAsync(id);
                if(currentPerson == null)
                    return NotFound();
                return Ok(new GetFullPersonDataResponse(
                    currentPerson.Id,
                    currentPerson.Surname,
                    currentPerson.Name,
                    currentPerson.Patronymic,
                    currentPerson.EducationalInstitution,
                    currentPerson.JobTitle,
                    currentPerson.City,
                    currentPerson.Phone,
                    currentPerson.Email,
                    currentPerson.IsVerified,
                    currentPerson.ElibraryProfileUrl
                    ));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-short-person-data-by-id/{id}")]
        [Authorize]
        public async Task<ActionResult<GetShortPersonDataResponse>> GetShortPersonDataById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentPerson = await _personService.GetPersonByIdAsync(id);
                if (currentPerson == null)
                    return NotFound();
                return Ok(new GetShortPersonDataResponse(
                    currentPerson.Id,
                    currentPerson.Surname,
                    currentPerson.Name,
                    currentPerson.Patronymic,
                    currentPerson.EducationalInstitution,
                    currentPerson.JobTitle,
                    currentPerson.City
                    ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-short-persons-data-by-surname/{surname}")]
        [Authorize]
        public async Task<ActionResult<GetShortPersonsDataListResponse>> GetShortPersonsDataListBySurname(string surname)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentPersons = await _personService.GetPersonsBySurnameAsync(surname);
                if (currentPersons == null || !currentPersons.Any())
                    return NotFound();

                List<GetShortPersonDataResponse> data = new List<GetShortPersonDataResponse>();

                foreach (var item in currentPersons)
                {
                    GetShortPersonDataResponse getShortPersonDataResponse = new GetShortPersonDataResponse(
                    item.Id,
                    item.Surname,
                    item.Name,
                    item.Patronymic,
                    item.EducationalInstitution,
                    item.JobTitle,
                    item.City
                    );
                    data.Add( getShortPersonDataResponse );
                }

                return Ok(new GetShortPersonsDataListResponse(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-short-persons-data-by-name/{name}")]
        [Authorize]
        public async Task<ActionResult<GetShortPersonsDataListResponse>> GetShortPersonsDataListByName(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentPersons = await _personService.GetPersonsByNameAsync(name);
                if (currentPersons == null || !currentPersons.Any())
                    return NotFound();

                List<GetShortPersonDataResponse> data = new List<GetShortPersonDataResponse>();

                foreach (var item in currentPersons)
                {
                    GetShortPersonDataResponse getShortPersonDataResponse = new GetShortPersonDataResponse(
                    item.Id,
                    item.Surname,
                    item.Name,
                    item.Patronymic,
                    item.EducationalInstitution,
                    item.JobTitle,
                    item.City
                    );
                    data.Add(getShortPersonDataResponse);
                }

                return Ok(new GetShortPersonsDataListResponse(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-short-persons-data-by-patronymic/{patronymic}")]
        [Authorize]
        public async Task<ActionResult<GetShortPersonsDataListResponse>> GetShortPersonsDataListByPatronymic(string patronymic)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentPersons = await _personService.GetPersonsByPatronymicAsync(patronymic);
                if (currentPersons == null || !currentPersons.Any())
                    return NotFound();

                List<GetShortPersonDataResponse> data = new List<GetShortPersonDataResponse>();

                foreach (var item in currentPersons)
                {
                    GetShortPersonDataResponse getShortPersonDataResponse = new GetShortPersonDataResponse(item.Id,
                    item.Surname,
                    item.Name,
                    item.Patronymic,
                    item.EducationalInstitution,
                    item.JobTitle,
                    item.City
                    );
                    data.Add(getShortPersonDataResponse);
                }

                return Ok(new GetShortPersonsDataListResponse(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-short-persons-data-by-education-institution/{educationInstitution}")]
        [Authorize]
        public async Task<ActionResult<GetShortPersonsDataListResponse>> GetShortPersonsDataListByEducationInstitution(string educationInstitution)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentPersons = await _personService.GetPersonsByEducationalInstitutionAsync(educationInstitution);
                if (currentPersons == null || !currentPersons.Any())
                    return NotFound();

                List<GetShortPersonDataResponse> data = new List<GetShortPersonDataResponse>();

                foreach (var item in currentPersons)
                {
                    GetShortPersonDataResponse getShortPersonDataResponse = new GetShortPersonDataResponse(item.Id,
                    item.Surname,
                    item.Name,
                    item.Patronymic,
                    item.EducationalInstitution,
                    item.JobTitle,
                    item.City
                    );
                    data.Add(getShortPersonDataResponse);
                }

                return Ok(new GetShortPersonsDataListResponse(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-short-persons-data-by-job-title/{jobTitle}")]
        [Authorize]
        public async Task<ActionResult<GetShortPersonsDataListResponse>> GetShortPersonsDataListByJobTitle(string jobTitle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentPersons = await _personService.GetPersonByJobTitleAsync(jobTitle);
                if (currentPersons == null || !currentPersons.Any())
                    return NotFound();

                List<GetShortPersonDataResponse> data = new List<GetShortPersonDataResponse>();

                foreach (var item in currentPersons)
                {
                    GetShortPersonDataResponse getShortPersonDataResponse = new GetShortPersonDataResponse(item.Id,
                    item.Surname,
                    item.Name,
                    item.Patronymic,
                    item.EducationalInstitution,
                    item.JobTitle,
                    item.City
                    );
                    data.Add(getShortPersonDataResponse);
                }

                return Ok(new GetShortPersonsDataListResponse(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-short-persons-data-by-city/{city}")]
        [Authorize]
        public async Task<ActionResult<GetShortPersonsDataListResponse>> GetShortPersonsDataListByCity(string city)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentPersons = await _personService.GetPersonsByCityAsync(city);
                if (currentPersons == null || !currentPersons.Any())
                    return NotFound();

                List<GetShortPersonDataResponse> data = new List<GetShortPersonDataResponse>();

                foreach (var item in currentPersons)
                {
                    GetShortPersonDataResponse getShortPersonDataResponse = new GetShortPersonDataResponse(item.Id,
                    item.Surname,
                    item.Name,
                    item.Patronymic,
                    item.EducationalInstitution,
                    item.JobTitle,
                    item.City
                    );
                    data.Add(getShortPersonDataResponse);
                }

                return Ok(new GetShortPersonsDataListResponse(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-short-persons-data-by-part-of-name-and-role/{partOfName}/{role}")]
        [Authorize]
        public async Task<ActionResult<GetShortPersonsDataListResponse>> GetShortPersonsDataListByPartOfNameAndRole(string partOfname, string role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(string.IsNullOrEmpty(partOfname) || string.IsNullOrEmpty(role) || !Roles.All.Contains(role))
                return BadRequest(ModelState);

            try
            {
                var currentPersons = await _personService.GetPersonsByPartOfFullNameAsync(partOfname, role);
                if (currentPersons == null || !currentPersons.Any())
                    return NotFound();

                List<GetShortPersonDataResponse> data = new List<GetShortPersonDataResponse>();

                foreach (var item in currentPersons)
                {
                    GetShortPersonDataResponse getShortPersonDataResponse = new GetShortPersonDataResponse(item.Id,
                    item.Surname,
                    item.Name,
                    item.Patronymic,
                    item.EducationalInstitution,
                    item.JobTitle,
                    item.City
                    );
                    data.Add(getShortPersonDataResponse);
                }

                return Ok(new GetShortPersonsDataListResponse(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-person-by-id/{id}")]
        [Authorize]
        public async Task<ActionResult> UpdatePersonById(Guid id, [FromBody] UpdatePersonRequest updatePersonRequest)
        {
            var subClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (subClaim?.Value is not { } subValue || !Guid.TryParse(subValue, out var currentPersonId))
                return BadRequest("Невозможно определить пользователя из токена.");
            if (id != currentPersonId && !User.IsInRole(Roles.Admin))
                return Forbid();

            if (updatePersonRequest.Email != null && string.IsNullOrWhiteSpace(updatePersonRequest.Email))
                return BadRequest("Email не может быть пустым.");
            if (updatePersonRequest.Phone != null && string.IsNullOrWhiteSpace(updatePersonRequest.Phone))
                return BadRequest("Номер телефона не может быть пустым.");

            try
            {
                await _personService.UpdateAsync(id,
                    surname: updatePersonRequest.Surname,
                    name: updatePersonRequest.Name,
                    patronymic: updatePersonRequest.Patronymic,
                    educationalInstitution: updatePersonRequest.EducationalInstitution,
                    jobTitle: updatePersonRequest.JobTitle,
                    city: updatePersonRequest.City,
                    phone: updatePersonRequest.Phone,
                    email: updatePersonRequest.Email,
                    elibraryProfileUrl: updatePersonRequest.ElibraryProfileUrl,
                    password: updatePersonRequest.NewPassword);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-person")]
        [Authorize]
        public async Task<ActionResult> UpdatePerson(Guid id, [FromBody] UpdatePersonRequest updatePersonRequest)
        {
            var subClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (subClaim?.Value is not { } subValue || !Guid.TryParse(subValue, out var currentPersonId))
                return BadRequest("Невозможно определить пользователя из токена.");
            if (id != currentPersonId && !User.IsInRole(Roles.Admin))
                return Forbid();

            if (updatePersonRequest.Email != null && string.IsNullOrWhiteSpace(updatePersonRequest.Email))
                return BadRequest("Email не может быть пустым.");
            if (updatePersonRequest.Phone != null && string.IsNullOrWhiteSpace(updatePersonRequest.Phone))
                return BadRequest("Номер телефона не может быть пустым.");

            try
            {
                await _personService.UpdateAsync(currentPersonId,
                    surname: updatePersonRequest.Surname,
                    name: updatePersonRequest.Name,
                    patronymic: updatePersonRequest.Patronymic,
                    educationalInstitution: updatePersonRequest.EducationalInstitution,
                    jobTitle: updatePersonRequest.JobTitle,
                    city: updatePersonRequest.City,
                    phone: updatePersonRequest.Phone,
                    email: updatePersonRequest.Email,
                    elibraryProfileUrl: updatePersonRequest.ElibraryProfileUrl,
                    password: updatePersonRequest.NewPassword);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
