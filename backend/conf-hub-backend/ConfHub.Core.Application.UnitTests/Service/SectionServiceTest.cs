using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Projects.Interfaces;
using ConfHub.Core.Application.Projects.Services;
using ConfHub.Core.Application.Sections.Interfaces;
using ConfHub.Core.Application.Sections.Services;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class SectionServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesSectionWithCorrectProperties()
        {
            var mockRepo = new Mock<ISectionRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new SectionService(mockRepo.Object, mockUow.Object);

            Guid conferenceId = Guid.NewGuid();
            string title = "Title";

            await service.AddAsync(conferenceId, title);

            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.Section>(m =>
                m.Title == title &&
                m.ConferenceId == conferenceId
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
