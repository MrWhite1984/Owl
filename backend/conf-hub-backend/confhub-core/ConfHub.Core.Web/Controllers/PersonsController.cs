using ConfHub.Core.Application.Persons.Interfaces;
using ConfHub.Core.Contracts.Requests.Persons;
using ConfHub.Core.Contracts.Responses.Persons;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<AddPersonResponse>> AddPerson([FromBody] AddPersonRequest addPersonRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _personService.AddAsync(
                    addPersonRequest.Surname,
                    addPersonRequest.Name,
                    addPersonRequest.Patronymic,
                    addPersonRequest.EducationalInstitution,
                    addPersonRequest.JobTitle,
                    addPersonRequest.City,
                    addPersonRequest.Phone,
                    addPersonRequest.Email,
                    addPersonRequest.IsVerified,
                    false,
                    addPersonRequest.ElibraryProfileUrl);
                if (response == null)
                    return BadRequest("Error while saving user");
                return Ok(new AddPersonResponse(response.Id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
