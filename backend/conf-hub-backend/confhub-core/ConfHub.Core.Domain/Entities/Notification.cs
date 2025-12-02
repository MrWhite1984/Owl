namespace ConfHub.Core.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; private set; }
        public Guid TargetPersonId { get; private set; }
        public string Title { get; private set; } = default!;
        public string Content { get; private set; } = default!;
        public bool IsChecked { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Notification() { }
        public Notification(Guid id, Guid targetPersonId, string title, string content, bool isChecked, DateTime createdAt)
        {
            Id = id;
            TargetPersonId = targetPersonId;
            Title = title;
            Content = content;
            IsChecked = isChecked;
            CreatedAt = createdAt;
        }
    }
}
