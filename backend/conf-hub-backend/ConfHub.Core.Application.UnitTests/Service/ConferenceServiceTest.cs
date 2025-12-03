using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Conferences.Interfaces;
using ConfHub.Core.Application.Conferences.Services;
using ConfHub.Core.Domain.Entities;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class ConferenceServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesConferenceWithCorrectProperties()
        {
            var mockRepo = new Mock<IConferenceRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new ConferenceService(mockRepo.Object, mockUow.Object);

            string title = "Conference name";
            DateTime startDate = DateTime.UtcNow;

            await service.AddAsync(title, startDate);

            mockRepo.Verify(r => r.AddAsync(It.Is<Conference>(m =>
                m.Title == title &&
                m.StartDate == startDate
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
