using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.ProjectParticipants.Interfaces
{
    public interface IProjectParticipantService
    {
        Task<IEnumerable<ProjectParticipant>> GetProjectParticipantsByProjectIdAsync(Guid projectId);
        Task AddAsync(Guid projectId, Guid personId, bool isScientificSupervisor, string confirmationStatus, string acceptionFileUrl);
        Task UpdateAsync(ProjectParticipant projectParticipant);
        Task DeleteAsync(Guid projectParticipantId);
    }
}
