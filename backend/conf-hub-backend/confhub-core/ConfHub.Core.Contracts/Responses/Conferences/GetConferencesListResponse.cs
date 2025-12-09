namespace ConfHub.Core.Contracts.Responses.Conferences
{
    public record GetConferencesListResponse(IEnumerable<ConferenceItem> ConferenceItems);
}
