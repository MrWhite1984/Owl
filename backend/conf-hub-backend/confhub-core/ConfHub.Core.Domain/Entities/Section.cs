namespace ConfHub.Core.Domain.Entities
{
    public class Section
    {
        public Guid Id { get; private set; }
        public Guid ConferenceId { get; private set; }
        public string Title { get; private set; } = default!;
        public bool IsDeleted { get; private set; }
        public Section() { }
        public Section(Guid id, Guid conferenceId, string title)
        {
            Id = id;
            ConferenceId = conferenceId;
            Title = title;
        }
    }
}
