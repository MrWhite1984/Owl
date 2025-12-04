using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.ProjectParticipants.Interfaces;
using ConfHub.Core.Application.ProjectParticipants.Services;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class ProjectParticipantServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesProjectParticipantWithCorrectProperties()
        {
            var mockRepo = new Mock<IProjectParticipantRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new ProjectParticipantService(mockRepo.Object, mockUow.Object);

            Guid projectId = Guid.NewGuid();
            Guid personId = Guid.NewGuid();
            bool isScientificSupervisor = false;
            string confirmationStatus = "status";
            string acceptionFileUrl = "url";

            await service.AddAsync(projectId, personId, isScientificSupervisor, confirmationStatus, acceptionFileUrl);

            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.ProjectParticipant>(m =>
                m.ProjectId == projectId &&
                m.PersonId == personId &&
                m.IsScientificSupervisor == false &&
                m.ConfirmationStatus == confirmationStatus &&
                m.AcceptionFileUrl == acceptionFileUrl
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
