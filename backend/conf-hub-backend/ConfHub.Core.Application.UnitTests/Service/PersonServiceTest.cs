using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Notifications.Interfaces;
using ConfHub.Core.Application.Notifications.Services;
using ConfHub.Core.Application.Persons.Interfaces;
using ConfHub.Core.Application.Persons.Services;
using Moq;

namespace ConfHub.Core.Application.UnitTests.Service
{
    public class PersonServiceTest
    {
        [Fact]
        public async Task AddAsync_CreatesPersonWithCorrectProperties()
        {
            var mockRepo = new Mock<IPersonRepository>();
            var mockUow = new Mock<IUnitOfWork>();
            var mockPas = new Mock<IPasswordHasher>();
            var service = new PersonService(mockRepo.Object, mockPas.Object, mockUow.Object);

            string surname = "UserSurname";
            string name = "UserName";
            string patronymic = "UserPatronymic";
            string educationalInstitution = "University";
            string jobTitle = "Student";
            string city = "City";
            string phone = "Phone";
            string email = "Email";
            bool isVerified = false;
            bool isDeleted = false;
            string elibraryProfileUrl = "url";
            string password = "password";

            await service.AddAsync(surname, name, patronymic, educationalInstitution, jobTitle, city, phone, email, isVerified, isDeleted, elibraryProfileUrl, password);

            mockRepo.Verify(r => r.AddAsync(It.Is<Domain.Entities.Person>(m =>
                m.Surname == surname &&
                m.Name == name &&
                m.Patronymic == patronymic &&
                m.EducationalInstitution == educationalInstitution &&
                m.JobTitle == jobTitle &&
                m.City == city &&
                m.Phone == phone &&
                m.Email == email &&
                m.IsVerified == isVerified &&
                m.IsDeleted == isDeleted &&
                m.ElibraryProfileUrl == elibraryProfileUrl
            )), Times.Once);

            mockUow.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
