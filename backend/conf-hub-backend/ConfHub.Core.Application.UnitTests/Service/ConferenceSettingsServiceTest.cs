using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Conferences.Interfaces;
using ConfHub.Core.Application.Conferences.Services;
using ConfHub.Core.Application.ConferenceSettings.Interfaces;
using ConfHub.Core.Application.ConferenceSettings.Services;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class ConferenceSettingsServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesConferenceSettingsWithCorrectProperties()
        {
            var mockRepo = new Mock<IConferenceSettingsRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new ConferenceSettingsService(mockRepo.Object, mockUow.Object);

            int maxArticlesPerAuthors = 5;
            bool allowOnlineDefence = true;
            bool isPublicPageEnabled = true;

            await service.AddAsync(maxArticlesPerAuthors, allowOnlineDefence, isPublicPageEnabled);

            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.ConferenceSettings>(m =>
                m.MaxArticlesPerAuthor == 5 &&
                m.AllowOnlineDefence == true &&
                m.IsPublicPageEnabled == true
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
