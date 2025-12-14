using ConfHub.Core.Contracts.Responses.Persons;

namespace ConfHub.Core.Contracts.Responses.ProjectParticipants
{
    public record ProjectParticipiantShortItem(PersonShortItem PersonShortItem, bool IsScientificSupervisor);
}
