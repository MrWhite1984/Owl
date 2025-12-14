using ConfHub.Core.Contracts.Responses.Sections;

namespace ConfHub.Core.Contracts.Responses.Conferences
{
    public record FullConferenceDataItem(Guid Id, string Title, DateTime StartDate, bool IsActive, string CollectionUrl, int MaxArticlesPerAuthor, bool AllowOnlineDefence, IEnumerable<SectionItem> SectionItems);
}
