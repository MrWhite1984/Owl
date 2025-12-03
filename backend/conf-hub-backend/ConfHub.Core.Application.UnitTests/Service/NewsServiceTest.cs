using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.News.Interfaces;
using ConfHub.Core.Application.News.Services;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class NewsServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesNewsWithCorrectProperties()
        {
            var mockRepo = new Mock<INewsRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new NewsService(mockRepo.Object, mockUow.Object);

            string title = "News";
            string content = "News content";
            Guid authorId = Guid.NewGuid();

            await service.AddAsync(title, content, authorId);

            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.News>(m =>
                m.Title == title &&
                m.Content == content &&
                m.AuthorPersonId == authorId
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
