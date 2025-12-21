using ConfHub.Core.Application.Conferences.Interfaces;
using ConfHub.Core.Contracts.Responses.Conferences;
using ConfHub.Core.Contracts.Responses.Persons;
using ConfHub.Core.Contracts.Responses.ProjectParticipants;
using ConfHub.Core.Contracts.Responses.Projects;
using ConfHub.Core.Contracts.Responses.Sections;
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

        [HttpGet("get-full-conference-data/{id:guid}")]
        public async Task<ActionResult<FullConferenceDataItem>> GetFullConferenceDataItem(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dto = await _conferenceService.GetFullDataConferenceAsync(id);
                if (dto == null)
                    return BadRequest("Конференция с таким Id не найдена");
                var response = new FullConferenceDataItem(
            Id: dto.Id,
            Title: dto.Title,
            StartDate: dto.StartDate,
            IsActive: dto.IsActive,
            CollectionUrl: dto.CollectionUrl,
            MaxArticlesPerAuthor: dto.MaxArticlesPerAuthor,
            AllowOnlineDefence: dto.AllowOnlineDefence,
            SectionItems: dto.Sections.Select(section => new SectionItem(
                Id: section.Id,
                Title: section.Title,
                ProjectsItems: section.ProjectsItems.Select(project => new ProjectShortItem(
                    project.Title,
                    project.ProjectParticipiants.Select(projectParticipiant => new ProjectParticipiantShortItem(
                        new PersonShortItem(
                            projectParticipiant.PersonShortItem.Surname, 
                            projectParticipiant.PersonShortItem.Name,
                            projectParticipiant.PersonShortItem.Patronymic,
                            projectParticipiant.PersonShortItem.JobTitle
                        ),
                        projectParticipiant.IsScientificSupervisor
                    )).ToList())).ToList())).ToList());

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
