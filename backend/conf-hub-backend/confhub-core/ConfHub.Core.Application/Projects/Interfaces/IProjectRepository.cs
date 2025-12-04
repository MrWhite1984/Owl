using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Projects.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetProjectByIdAsync(Guid projectId);
        Task<Project?> GetProjectByTitleAsync(string title);
        Task<IEnumerable<Project>> GetProjectsBySectionIdAsync(Guid sectionId);
        Task<IEnumerable<Project>> GetProjectsByStatusAsync(string status);
        Task AddAsync(Project project);
        void Update(Project project);
    }
}
