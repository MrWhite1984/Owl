using ConfHub.Core.Application.Conferences.Interfaces;
using ConfHub.Core.Contracts.Responses.Conferences;
using Microsoft.AspNetCore.Mvc;

namespace ConfHub.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferenceService _conferenceService;
        public ConferencesController(IConferenceService conferenceService)
        {
            _conferenceService = conferenceService;
        }

        [HttpGet("get-conferences-list")]
        public async Task<ActionResult<GetConferencesListResponse>> GetConferencesList()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentConferences = await _conferenceService.GetAllAsync();
                return Ok(new GetConferencesListResponse(currentConferences.Select(o => new ConferenceItem(o.Id, o.Title, o.StartDate, o.IsActive))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
