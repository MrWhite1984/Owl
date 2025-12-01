namespace ConfHub.Core.Domain.Entities
{
    public class News
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = default!;
        public string Content { get; private set; } = default!;
        public Guid AuthorPersonId { get; private set; }
        public DateTime PublishedAt { get; private set; }

        public News() { }
        public News (Guid id, string title, string content, Guid authorPersonId, DateTime publishedAt)
        {
            Id = id;
            Title = title;
            Content = content;
            AuthorPersonId = authorPersonId;
            PublishedAt = publishedAt;
        }
    }
}
