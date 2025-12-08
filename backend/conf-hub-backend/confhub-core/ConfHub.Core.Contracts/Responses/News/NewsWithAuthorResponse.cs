namespace ConfHub.Core.Contracts.Responses.News
{
    public record NewsWithAuthorResponse(IEnumerable<NewsItem> News, DateTime NextDateTime);
}
