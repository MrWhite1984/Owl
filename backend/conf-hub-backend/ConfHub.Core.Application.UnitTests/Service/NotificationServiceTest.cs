using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.News.Interfaces;
using ConfHub.Core.Application.News.Services;
using ConfHub.Core.Application.Notifications.Interfaces;
using ConfHub.Core.Application.Notifications.Services;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class NotificationServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesNotificationWithCorrectProperties()
        {
            var mockRepo = new Mock<INotificationRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new NotificationService(mockRepo.Object, mockUow.Object);

            string title = "Notification";
            string content = "Notification content";
            Guid targetPerson = Guid.NewGuid();
            bool isChecked = false;

            await service.AddAsync(targetPerson, title, content, isChecked);

            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.Notification>(m =>
                m.Title == title &&
                m.Content == content &&
                m.TargetPersonId == targetPerson &&
                m.IsChecked == isChecked
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
