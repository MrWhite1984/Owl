using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Persons.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person?> GetPersonByIdAsync(Guid id);
        Task<IEnumerable<Person>?> GetPersonsBySurnameAsync(string surname);
        Task<IEnumerable<Person>?> GetPersonsByNameAsync(string name);
        Task<IEnumerable<Person>?> GetPersonsByPatronymicAsync(string patronimyc);
        Task<IEnumerable<Person>?> GetPersonsByPartOfFullNameAsync(string partOfFullName);
        Task<IEnumerable<Person>?> GetPersonsByEducationalInstitutionAsync(string educationalInstitution);
        Task<IEnumerable<Person>?> GetPersonsByCityAsync(string city);
        Task<IEnumerable<Person>?> GetPersonByJobTitleAsync(string jobTitle);
        Task<Person?> GetPersonByEmailAsync(string email);
        Task AddAsync(Person person);
        void Update(Person person);
    }
}
