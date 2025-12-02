namespace ConfHub.Core.Domain.Entities
{
    public class ProjectParticipant
    {
        public Guid ProjectId { get; private set; }
        public Guid PersonId { get; private set; }
        public bool IsScientificSupervisor { get; private set; }
        public string ConfirmationStatus { get; private set; } = default!;
        public string AcceptionFileUrl { get; private set; } = default!;
        public ProjectParticipant() { }
        public ProjectParticipant(Guid projectId, Guid personId, bool isScientificSupervisor, string confirmationStatus, string acceptionFileUrl)
        {
            ProjectId = projectId;
            PersonId = personId;
            IsScientificSupervisor = isScientificSupervisor;
            ConfirmationStatus = confirmationStatus;
            AcceptionFileUrl = acceptionFileUrl;
        }
    }
}
