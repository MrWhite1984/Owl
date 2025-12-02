using ConfHub.Core.Application.Conferences.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly AppDbContext _appDbContext;

        public ConferenceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Conference conference)
        {
            await _appDbContext.Conferences.AddAsync(conference);
        }

        public async Task<IEnumerable<Conference>> GetAllAsync()
        {
            var currentConferences = await _appDbContext.Conferences.ToListAsync();
            return currentConferences;
        }

        public async Task<Conference?> GetByIdAsync(Guid id)
        {
            var currentConference = await _appDbContext.Conferences.FindAsync(id);
            return currentConference;
        }

        public void Update(Conference conference)
        {
            _appDbContext.Conferences.Update(conference);
        }

    }
}
