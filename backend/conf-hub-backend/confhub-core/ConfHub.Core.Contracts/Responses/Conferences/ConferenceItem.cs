namespace ConfHub.Core.Contracts.Responses.Conferences
{
    public record ConferenceItem(Guid Id, string Title, DateTime StartDate, bool IsActive);
}
