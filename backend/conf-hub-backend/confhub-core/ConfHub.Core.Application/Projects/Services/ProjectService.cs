using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Projects.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(string title, Guid sectionId, string status, string articleFileUrl, string reviewFileUrl, string elibraryUrl)
        {
            Project project = new Project(Guid.NewGuid(), title, sectionId, status, articleFileUrl, reviewFileUrl, elibraryUrl, DateTime.UtcNow);
            await _projectRepository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(Guid projectId)
        {
            var currentProject = await _projectRepository.GetProjectByIdAsync(projectId);
            return currentProject;
        }

        public async Task<Project?> GetProjectByTitleAsync(string title)
        {
            var currentProject = await _projectRepository.GetProjectByTitleAsync(title);
            return currentProject;
        }

        public async Task<IEnumerable<Project>> GetProjectsBySectionIdAsync(Guid sectionId)
        {
            var currentProjects = await _projectRepository.GetProjectsBySectionIdAsync(sectionId);
            return currentProjects;
        }

        public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(string status)
        {
            var currentProjects = await _projectRepository.GetProjectsByStatusAsync(status);
            return currentProjects;
        }

        public async Task UpdateAsync(Project project)
        {
            _projectRepository.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
