namespace ConfHub.Core.Application.News.DTO
{
    public record NewsWithAuthorDto(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    AuthorDto Author
    );
}
