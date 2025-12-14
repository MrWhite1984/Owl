using ConfHub.Core.Application.ProjectParticipants.DTO;

namespace ConfHub.Core.Application.Projects.DTO
{
    public record ProjectShortItem(Guid Id, string Title, IEnumerable<ProjectParticipiantShortItem> ProjectParticipiants);
}
