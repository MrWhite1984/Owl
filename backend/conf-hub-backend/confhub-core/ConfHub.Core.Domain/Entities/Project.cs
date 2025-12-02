namespace ConfHub.Core.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = default!;
        public Guid SectionId { get; private set; }
        public string Status { get; private set; } = default!;
        public string ArticleFileUrl { get; private set; } = default!;
        public string ReviewFileUrl { get; private set; } = default!;
        public string ElibraryUrl { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public Project() { }
        public Project(Guid id, string title, Guid sectionId, string status, string articleFileUrl, string reviewFileUrl, string elibraryUrl, DateTime createdAt)
        {
            Id = id;
            Title = title;
            SectionId = sectionId;
            Status = status;
            ArticleFileUrl = articleFileUrl;
            ReviewFileUrl = reviewFileUrl;
            ElibraryUrl = elibraryUrl;
            CreatedAt = createdAt;
        }
    }
}
