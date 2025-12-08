namespace ConfHub.Core.Application.News.DTO
{
    public record NewsWithAuthorListDto(IEnumerable<NewsWithAuthorDto> NewsWithAuthorDtos, DateTime NextDateTime);
}
