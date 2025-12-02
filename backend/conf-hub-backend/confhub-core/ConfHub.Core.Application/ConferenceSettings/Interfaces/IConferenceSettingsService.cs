namespace ConfHub.Core.Application.ConferenceSettings.Interfaces
{
    public interface IConferenceSettingsService
    {
        Task<Domain.Entities.ConferenceSettings?> GetByConferenceIdAsync(Guid conferenceId);
        Task AddAsync(int maxArticlesPerAuthors, bool allowOnlineDefence, bool isPublicPageEnabled);
        void Update(Domain.Entities.ConferenceSettings conferenceSettings);
    }
}
