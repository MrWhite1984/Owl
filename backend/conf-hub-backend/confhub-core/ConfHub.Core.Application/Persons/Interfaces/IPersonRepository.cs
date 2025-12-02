using ConfHub.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfHub.Core.Application.Persons.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person?> GetPersonByIdAsync(Guid id);
        Task<IEnumerable<Person>?> GetPersonsBySurnameAsync(string surname);
        Task<IEnumerable<Person>?> GetPersonsByNameAsync(string name);
        Task<IEnumerable<Person>?> GetPersonsByPatronimycAsync(string patronimyc);
        Task<IEnumerable<Person>?> GetPersonsByPartOfFullNameAsync(string partOfFullName);
        Task<IEnumerable<Person>?> GetPersonsByEducationalInstitutionAsync(string educationalInstitution);
        Task<IEnumerable<Person>?> GetPersonsByCityAsync(string city);
        Task<IEnumerable<Person>?> GetPersonByJobTitleAsync(string jobTitle);
        Task<Person?> GetPersonByEmailAsync(string email);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
    }
}
