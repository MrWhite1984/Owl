using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.ProjectParticipants.Interfaces
{
    public interface IProjectParticipantRepository
    {
        Task<IEnumerable<ProjectParticipant>> GetProjectParticipantsByProjectIdAsync(Guid projectId);
        Task AddAsync(ProjectParticipant projectParticipant);
        void Update(ProjectParticipant projectParticipant);
        Task DeleteAsync(Guid projectParticipantId);
    }
}
