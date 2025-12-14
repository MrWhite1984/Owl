using ConfHub.Core.Application.Persons.DTO;

namespace ConfHub.Core.Application.ProjectParticipants.DTO
{
    public record ProjectParticipiantShortItem(Guid Id, PersonShortItem PersonShortItem, bool IsScientificSupervisor);
}
