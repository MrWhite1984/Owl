using ConfHub.Core.Application.Projects.DTO;

namespace ConfHub.Core.Application.Sections.DTO
{
    public record SectionItem(Guid Id, string Title, IEnumerable<ProjectShortItem> ProjectsItems);
}
