namespace ConfHub.Core.Contracts.Responses.News
{
    public record NewsItem(
        Guid Id,
        string Title,
        string Content,
        DateTime CreatedAt,
        AuthorInfo Author
    );
}
