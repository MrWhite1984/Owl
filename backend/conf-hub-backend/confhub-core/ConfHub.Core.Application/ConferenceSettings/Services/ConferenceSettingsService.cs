using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.ConferenceSettings.Interfaces;

namespace ConfHub.Core.Application.ConferenceSettings.Services
{
    public class ConferenceSettingsService : IConferenceSettingsService
    {
        private readonly IConferenceSettingsRepository _conferenceSettingsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConferenceSettingsService(IConferenceSettingsRepository conferenceSettingsRepository, IUnitOfWork unitOfWork)
        {
            _conferenceSettingsRepository = conferenceSettingsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(int maxArticlesPerAuthors, bool allowOnlineDefence, bool isPublicPageEnabled)
        {
            Domain.Entities.ConferenceSettings conferenceSettings = new Domain.Entities.ConferenceSettings(Guid.NewGuid(), maxArticlesPerAuthors, allowOnlineDefence, isPublicPageEnabled);
            await _conferenceSettingsRepository.AddAsync(conferenceSettings);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Domain.Entities.ConferenceSettings?> GetByConferenceIdAsync(Guid conferenceId)
        {
            var currentConferenceSettings = await _conferenceSettingsRepository.GetByConferenceIdAsync(conferenceId);
            return currentConferenceSettings;
        }

        public async Task UpdateAsync(Domain.Entities.ConferenceSettings conferenceSettings)
        {
            _conferenceSettingsRepository.Update(conferenceSettings);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
