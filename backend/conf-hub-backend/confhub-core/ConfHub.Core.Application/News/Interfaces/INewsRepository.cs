using ConfHub.Core.Application.News.DTO;

namespace ConfHub.Core.Application.News.Interfaces
{
    public interface INewsRepository
    {
        Task<Domain.Entities.News?> GetNewsByIdAsync(Guid id);
        Task<PartNewsDto> GetPartOfNewsByDateTimeAsync(DateTime startDateTime, int partSize);
        Task AddAsync(Domain.Entities.News entity);
        void Update(Domain.Entities.News entity);
        Task DeleteAsync(Guid id);

    }
}
