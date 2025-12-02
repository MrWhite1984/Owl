namespace ConfHub.Core.Application.ConferenceSettings.Interfaces
{
    public interface IConferenceSettingsRepository
    {
        Task<Domain.Entities.ConferenceSettings> GetByConferenceIdAsync(Guid conferenceId);
        Task AddAsync(Domain.Entities.ConferenceSettings conferenceSettings);
        Task UpdateAsync(Domain.Entities.ConferenceSettings conferenceSettings);
    }
}
