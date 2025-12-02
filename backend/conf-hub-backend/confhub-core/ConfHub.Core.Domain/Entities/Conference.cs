namespace ConfHub.Core.Domain.Entities
{
    public class Conference
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = default!;
        public DateTime StartDate { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public string CollectionUrl { get; private set; } = default!;
        public Conference() { }
        public Conference(Guid id, string title, DateTime startDate, bool isActive, string collectionUrl)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            IsActive = isActive;
            CollectionUrl = collectionUrl;
        }
    }
}
