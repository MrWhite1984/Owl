using ConfHub.Core.Application.Persons.Interfaces;
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

        
    }
}
