namespace ConfHub.Core.Application.News.Interfaces
{
    public interface INewsService
    {
        Task<Domain.Entities.News> GetNewsByIdAsync(Guid id);
        Task<IEnumerable<Domain.Entities.News>> GetPartOfNewsByDateTimeAsync(DateTime startDateTime, int partSize);
        Task AddAsync(string title, string content, Guid authorId);
        Task UpdateAsync(Domain.Entities.News entity);
        Task DeleteAsync(Guid id);
    }
}
