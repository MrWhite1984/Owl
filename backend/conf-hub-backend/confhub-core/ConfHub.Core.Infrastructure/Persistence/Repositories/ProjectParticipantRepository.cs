using ConfHub.Core.Application.ProjectParticipants.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class ProjectParticipantRepository : IProjectParticipantRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProjectParticipantRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(ProjectParticipant projectParticipant)
        {
            await _appDbContext.ProjectParticipants.AddAsync(projectParticipant);
        }

        public async Task DeleteAsync(Guid projectParticipantId)
        {
            await _appDbContext.ProjectParticipants.Where(x => new { x.ProjectId, x.PersonId }.Equals(projectParticipantId)).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<ProjectParticipant>> GetProjectParticipantsByProjectIdAsync(Guid projectId)
        {
            var currentProjectParticipants = await _appDbContext.ProjectParticipants.Where(x => x.ProjectId.Equals(projectId)).ToListAsync();
            return currentProjectParticipants;
        }

        public void Update(ProjectParticipant projectParticipant)
        {
            _appDbContext.ProjectParticipants.Update(projectParticipant);
        }
    }
}
