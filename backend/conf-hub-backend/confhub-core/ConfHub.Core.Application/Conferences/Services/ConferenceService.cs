using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Conferences.DTO;
using ConfHub.Core.Application.Conferences.Interfaces;
using ConfHub.Core.Application.ConferenceSettings.Interfaces;
using ConfHub.Core.Application.Persons.DTO;
using ConfHub.Core.Application.Persons.Interfaces;
using ConfHub.Core.Application.ProjectParticipants.DTO;
using ConfHub.Core.Application.ProjectParticipants.Interfaces;
using ConfHub.Core.Application.Projects.DTO;
using ConfHub.Core.Application.Projects.Interfaces;
using ConfHub.Core.Application.Sections.DTO;
using ConfHub.Core.Application.Sections.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Conferences.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IConferenceSettingsRepository _conferenceSettingsRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectParticipantRepository _participantRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConferenceService(
            IConferenceRepository conferenceRepository,
            IConferenceSettingsRepository conferenceSettingsRepository,
            ISectionRepository sectionRepository,
            IProjectRepository projectRepository,
            IProjectParticipantRepository projectParticipantRepository,
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork)
        {
            _conferenceRepository = conferenceRepository;
            _conferenceSettingsRepository = conferenceSettingsRepository;
            _sectionRepository = sectionRepository;
            _projectRepository = projectRepository;
            _participantRepository = projectParticipantRepository;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(string title, DateTime startDate)
        {
            Conference conference = new Conference(Guid.NewGuid(), title, startDate, false, string.Empty);
            await _conferenceRepository.AddAsync(conference);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Conference>> GetAllAsync()
        {
            var currentConferences = await _conferenceRepository.GetAllAsync();
            return currentConferences;
        }

        public async Task<Conference?> GetByIdAsync(Guid id)
        {
            var currentConference = await _conferenceRepository.GetByIdAsync(id);
            return currentConference;
        }

        public async Task<FullConferenceDataItem?> GetFullDataConferenceAsync(Guid id)
        {
            var currentConference = await _conferenceRepository.GetByIdAsync(id);
            var currentConferenceSettings = await _conferenceSettingsRepository.GetByConferenceIdAsync(id);

            if (currentConference == null || currentConferenceSettings == null)
                return null;

            var currentSections = await _sectionRepository.GetSectionsByConferenceIdAsync(id);

            var sectionItems = new List<SectionItem>();

            if (currentSections?.Any() == true)
            {
                foreach (var section in currentSections)
                {
                    var projectItems = new List<ProjectShortItem>();
                    var projects = await _projectRepository.GetProjectsBySectionIdAsync(section.Id);
                    if(projects?.Any() == true)
                    {
                        foreach(var project in projects)
                        {
                            var projectParticipiantsItems = new List<ProjectParticipiantShortItem>();
                            var projectParticipiants = await _participantRepository.GetProjectParticipantsByProjectIdAsync(project.Id);
                            if(projectParticipiants?.Any() == true)
                            {
                                foreach(var participiant in projectParticipiants)
                                {
                                    var currentPerson = await _personRepository.GetPersonByIdAsync(participiant.PersonId);
                                    if(currentPerson != null)
                                        projectParticipiantsItems.Add(new ProjectParticipiantShortItem
                                            (participiant.PersonId,
                                            new PersonShortItem(currentPerson.Surname, currentPerson.Name, currentPerson.Patronymic, currentPerson.JobTitle),
                                            participiant.IsScientificSupervisor
                                            ));
                                }
                            }

                            projectItems.Add(new ProjectShortItem(
                            project.Id,
                            project.Title,
                            projectParticipiantsItems));
                        }
                    }

                    sectionItems.Add(new SectionItem(
                        section.Id,
                        section.Title,
                        projectItems
                    ));
                }
            }

            return new FullConferenceDataItem(
                id,
                currentConference.Title,
                currentConference.StartDate,
                currentConference.IsActive,
                currentConference.CollectionUrl,
                currentConferenceSettings.MaxArticlesPerAuthor,
                currentConferenceSettings.AllowOnlineDefence,
                sectionItems
            );
        }

        public async Task UpdateAsync(Conference conference)
        {
            _conferenceRepository.Update(conference);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
