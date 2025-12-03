using ConfHub.Core.Application.ChatMessages.Interfaces;
using ConfHub.Core.Application.ChatMessages.Services;
using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Domain.Entities;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class ChatMessageServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesChatMessageWithCorrectProperties()
        {
            var mockRepo = new Mock<IChatMessageRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var service = new ChatMessageService(mockRepo.Object, mockUow.Object);

            var projectId = Guid.NewGuid();
            var personId = Guid.NewGuid();
            var content = "Hello";

            await service.AddAsync(projectId, personId, content);

            mockRepo.Verify(r => r.AddAsync(It.Is<ChatMessage>(m =>
                m.ProjectId == projectId &&
                m.PersonId == personId &&
                m.Content == content &&
                m.CreatedAt <= DateTime.UtcNow
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
