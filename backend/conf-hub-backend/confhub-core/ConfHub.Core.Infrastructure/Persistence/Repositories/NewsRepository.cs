using ConfHub.Core.Application.News.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _appDbContext;

        public NewsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(News entity)
        {
            await _appDbContext.News.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _appDbContext.News.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync();
        }

        public async Task<News?> GetNewsByIdAsync(Guid id)
        {
            var currentNews = await _appDbContext.News.FindAsync(id);
            return currentNews;
        }

        public async Task<IEnumerable<News>> GetPartOfNewsByDateTimeAsync(DateTime startDateTime, int partSize)
        {
            var currentNews = await _appDbContext.News.OrderByDescending(x => x.PublishedAt).Where(x => x.PublishedAt < startDateTime).Take(partSize + 1).ToListAsync();
            return currentNews;
        }

        public void Update(News entity)
        {
            _appDbContext.News.Update(entity);
        }
    }
}
