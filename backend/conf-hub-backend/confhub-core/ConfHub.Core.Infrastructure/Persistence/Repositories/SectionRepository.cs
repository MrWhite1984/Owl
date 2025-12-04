using ConfHub.Core.Application.Sections.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly AppDbContext _appDbContext;

        public SectionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Section section)
        {
            await _appDbContext.Sections.AddAsync(section);
        }

        public async Task<Section?> GetSectionByIdAsync(Guid id)
        {
            var currentSection = await _appDbContext.Sections.FindAsync(id);
            return currentSection;
        }

        public async Task<IEnumerable<Section>> GetSectionsByConferenceIdAsync(Guid conferenceId)
        {
            var currentSections = await _appDbContext.Sections.Where(x => x.ConferenceId.Equals(conferenceId)).ToListAsync();
            return currentSections;
        }

        public void Update(Section section)
        {
            _appDbContext.Sections.Update(section);
        }
    }
}
