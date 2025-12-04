using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.ProjectParticipants.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.ProjectParticipants.Services
{
    public class ProjectParticipantService : IProjectParticipantService
    {
        private readonly IProjectParticipantRepository _projectParticipantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectParticipantService(IProjectParticipantRepository projectParticipantRepository, IUnitOfWork unitOfWork)
        {
            _projectParticipantRepository = projectParticipantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Guid projectId, Guid personId, bool isScientificSupervisor, string confirmationStatus, string acceptionFileUrl)
        {
            ProjectParticipant projectParticipant = new ProjectParticipant(projectId, personId, isScientificSupervisor, confirmationStatus, acceptionFileUrl);
            await _projectParticipantRepository.AddAsync(projectParticipant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid projectParticipantId)
        {
            await _projectParticipantRepository.DeleteAsync(projectParticipantId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectParticipant>> GetProjectParticipantsByProjectIdAsync(Guid projectId)
        {
            var currentProjectParticipant = await _projectParticipantRepository.GetProjectParticipantsByProjectIdAsync(projectId);
            return currentProjectParticipant;
        }

        public async Task UpdateAsync(ProjectParticipant projectParticipant)
        {
            _projectParticipantRepository.Update(projectParticipant);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
