using ConfHub.Core.Contracts.Responses.ProjectParticipants;

namespace ConfHub.Core.Contracts.Responses.Projects
{
    public record ProjectShortItem(string Title, IEnumerable<ProjectParticipiantShortItem> ProjectParticipiants);
}
