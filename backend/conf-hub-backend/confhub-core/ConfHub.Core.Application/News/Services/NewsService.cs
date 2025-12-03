using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.News.Interfaces;

namespace ConfHub.Core.Application.News.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(INewsRepository newsRepository, IUnitOfWork unitOfWork)
        {
            _newsRepository = newsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(string title, string content, Guid authorId)
        {
            Domain.Entities.News news = new Domain.Entities.News(Guid.NewGuid(), title, content, authorId, DateTime.UtcNow);
            await _newsRepository.AddAsync(news);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _newsRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Domain.Entities.News?> GetNewsByIdAsync(Guid id)
        {
            var currentNews = await _newsRepository.GetNewsByIdAsync(id);
            return currentNews;
        }

        public async Task<IEnumerable<Domain.Entities.News>> GetPartOfNewsByDateTimeAsync(DateTime startDateTime, int partSize)
        {
            var currentNews = await _newsRepository.GetPartOfNewsByDateTimeAsync(startDateTime, partSize);
            return currentNews;
        }

        public async Task UpdateAsync(Domain.Entities.News entity)
        {
            _newsRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
