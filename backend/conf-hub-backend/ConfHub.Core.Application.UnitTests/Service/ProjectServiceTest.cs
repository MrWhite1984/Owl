using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.ProjectParticipants.Interfaces;
using ConfHub.Core.Application.ProjectParticipants.Services;
using ConfHub.Core.Application.Projects.Interfaces;
using ConfHub.Core.Application.Projects.Services;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class ProjectServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesProjectWithCorrectProperties()
        {
            var mockRepo = new Mock<IProjectRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new ProjectService(mockRepo.Object, mockUow.Object);

            string title = "Title";
            Guid sectionId = Guid.NewGuid();
            string status = "Status";
            string articleFileUrl = "url";
            string reviewFileUrl = "url";
            string elibraryUrl = "url";

            await service.AddAsync(title, sectionId, status, articleFileUrl, reviewFileUrl, elibraryUrl);

            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.Project>(m =>
                m.Title == title &&
                m.SectionId == sectionId &&
                m.Status == status &&
                m.ArticleFileUrl == articleFileUrl &&
                m.ReviewFileUrl == reviewFileUrl &&
                m.ElibraryUrl == elibraryUrl
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
