using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Sections.Interfaces;
using ConfHub.Core.Application.Sections.Services;
using ConfHub.Core.Application.Users.Interfaces;
using ConfHub.Core.Application.Users.Services;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class UserServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesUserWithCorrectProperties()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var mockPas = new Mock<IPasswordHasher>();
            var service = new UserService(mockRepo.Object, mockPas.Object, mockUow.Object);

            Guid personId = Guid.NewGuid();
            string role = "Role";
            string password = "Password";

            await service.AddAsync(personId, role, password);

            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.User>(m =>
                m.PersonId == personId &&
                m.Role == role
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
