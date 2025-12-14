using ConfHub.Core.Contracts.Responses.ProjectParticipants;
using ConfHub.Core.Contracts.Responses.Projects;

namespace ConfHub.Core.Contracts.Responses.Sections
{
    public record SectionItem(Guid Id, string Title, IEnumerable<ProjectShortItem> ProjectsItems);
}
