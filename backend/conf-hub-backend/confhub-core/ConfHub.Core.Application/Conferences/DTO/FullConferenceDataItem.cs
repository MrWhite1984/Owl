using ConfHub.Core.Application.Sections.DTO;

namespace ConfHub.Core.Application.Conferences.DTO
{
    public record FullConferenceDataItem(Guid Id, string Title, DateTime StartDate, bool IsActive, string CollectionUrl, int MaxArticlesPerAuthor, bool AllowOnlineDefence, IEnumerable<SectionItem> Sections);
}
