namespace ConfHub.Core.Application.ConferenceSettings.Interfaces
{
    public interface IConferenceSettingsService
    {
        Task<Domain.Entities.ConferenceSettings?> GetByConferenceIdAsync(Guid conferenceId);
        Task AddAsync(int maxArticlesPerAuthors, bool allowOnlineDefence, bool isPublicPageEnabled);
        Task UpdateAsync(Domain.Entities.ConferenceSettings conferenceSettings);
    }
}
