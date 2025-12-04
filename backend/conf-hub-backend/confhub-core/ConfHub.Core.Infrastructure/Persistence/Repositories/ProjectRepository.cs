using ConfHub.Core.Application.Projects.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProjectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Project project)
        {
            await _appDbContext.Projects.AddAsync(project);
        }

        public async Task<Project?> GetProjectByIdAsync(Guid projectId)
        {
            var currentProject = await _appDbContext.Projects.FindAsync(projectId);
            return currentProject;
        }

        public async Task<Project?> GetProjectByTitleAsync(string title)
        {
            var currentProject = await _appDbContext.Projects.Where(x => x.Title.Equals(title)).FirstOrDefaultAsync();
            return currentProject;
        }

        public async Task<IEnumerable<Project>> GetProjectsBySectionIdAsync(Guid sectionId)
        {
            var currentProjects = await _appDbContext.Projects.Where(x => x.SectionId.Equals(sectionId)).ToListAsync();
            return currentProjects;
        }

        public async Task<IEnumerable<Project>> GetProjectsByStatusAsync(string status)
        {
            var currentProjects = await _appDbContext.Projects.Where(x => x.Status.Equals(status)).ToListAsync();
            return currentProjects;
        }

        public void Update(Project project)
        {
            _appDbContext.Projects.Update(project);
        }
    }
}
