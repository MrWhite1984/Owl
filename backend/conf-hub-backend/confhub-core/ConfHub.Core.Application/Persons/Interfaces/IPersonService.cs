using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Persons.Interfaces
{
    public interface IPersonService
    {
        Task<Person?> GetPersonByIdAsync(Guid id);
        Task<Person?> AuthenticateAsync(string email, string password);
        Task<IEnumerable<Person>?> GetPersonsBySurnameAsync(string surname);
        Task<IEnumerable<Person>?> GetPersonsByNameAsync(string name);
        Task<IEnumerable<Person>?> GetPersonsByPatronymicAsync(string patronimyc);
        Task<IEnumerable<Person>?> GetPersonsByPartOfFullNameAsync(string partOfFullName, string allowedRoles);
        Task<IEnumerable<Person>?> GetPersonsByEducationalInstitutionAsync(string educationalInstitution);
        Task<IEnumerable<Person>?> GetPersonsByCityAsync(string city);
        Task<IEnumerable<Person>?> GetPersonByJobTitleAsync(string jobTitle);
        Task<Person?> GetPersonByEmailAsync(string email);
        Task<Person?> AddAsync(string surname, string name, string patronymic, string educationalInstitution, string jobTitle, string city, string phone, string email, bool isVerified, bool isDeleted, string elibraryProfileUrl, string password);
        Task UpdateAsync(Guid id, string? surname, string? name, string? patronymic, string? educationalInstitution, string? jobTitle, string? city, string? phone, string? email, string? elibraryProfileUrl, string? password);
    }
}
