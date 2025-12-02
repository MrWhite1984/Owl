using ConfHub.Core.Application.ConferenceSettings.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class ConferenceSettingsRepository : IConferenceSettingsRepository
    {
        private readonly AppDbContext _appDbContext;

        public ConferenceSettingsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(ConferenceSettings conferenceSettings)
        {
            await _appDbContext.ConferenceSettings.AddAsync(conferenceSettings);
        }

        public async Task<ConferenceSettings?> GetByConferenceIdAsync(Guid conferenceId)
        {
            var currentConferenceSettings = await _appDbContext.ConferenceSettings.FindAsync(conferenceId);
            return currentConferenceSettings;
        }

        public void Update(ConferenceSettings conferenceSettings)
        {
            _appDbContext.ConferenceSettings.Update(conferenceSettings);
        }
    }
}
