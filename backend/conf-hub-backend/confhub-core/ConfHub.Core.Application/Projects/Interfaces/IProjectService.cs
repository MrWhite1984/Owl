using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Projects.Interfaces
{
    public interface IProjectService
    {
        Task<Project?> GetProjectByIdAsync(Guid projectId);
        Task<Project?> GetProjectByTitleAsync(string title);
        Task<IEnumerable<Project>> GetProjectsBySectionIdAsync(Guid sectionId);
        Task<IEnumerable<Project>> GetProjectsByStatusAsync(string status);
        Task AddAsync(string title, Guid sectionId, string status, string articleFileUrl, string eeviewFileUrl, string elibraryUrl);
        Task UpdateAsync(Project project);
    }
}
