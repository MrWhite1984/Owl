using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.News.DTO
{
    public record PartNewsDto(IEnumerable<Domain.Entities.News> News, DateTime NextDateTime);
}
