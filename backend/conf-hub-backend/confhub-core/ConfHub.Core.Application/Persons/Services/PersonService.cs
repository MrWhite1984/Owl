using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Persons.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ConfHub.Core.Application.Persons.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Person?> AddAsync(string surname, string name, string patronymic, string educationalInstitution, string jobTitle, string city, string phone, string email, bool isVerified, bool isDeleted, string elibraryProfileUrl)
        {
            Person person = new Person(Guid.NewGuid(), surname, name, patronymic, educationalInstitution, jobTitle, city, phone, email, isVerified, isDeleted, elibraryProfileUrl, "", DateTime.UtcNow);
            try
            {
                await _personRepository.AddAsync(person);
                await _unitOfWork.SaveChangesAsync();
                return person;
            }
            catch (DbUpdateException ex)
            {
                var postgresEx = ex.InnerException as PostgresException
                   ?? ex.InnerException?.InnerException as PostgresException;

                if (postgresEx != null && postgresEx.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    if (postgresEx.ConstraintName != null)
                    {
                        if (postgresEx.ConstraintName.Contains("email", StringComparison.OrdinalIgnoreCase))
                            throw new InvalidOperationException("Email уже зарегистрирован.");
                        if (postgresEx.ConstraintName.Contains("phone", StringComparison.OrdinalIgnoreCase))
                            throw new InvalidOperationException("Номер телефона уже зарегистрирован.");
                    }
                    throw new InvalidOperationException("Нарушено правило уникальности.");
                }

                throw new InvalidOperationException("Нарушено правило уникальности.");
            }
        }

        public async Task<Person?> GetPersonByEmailAsync(string email)
        {
            var currentPerson = await _personRepository.GetPersonByEmailAsync(email);
            return currentPerson;
        }

        public async Task<Person?> GetPersonByIdAsync(Guid id)
        {
            var currentPerson = await _personRepository.GetPersonByIdAsync(id);
            return currentPerson;
        }

        public async Task<IEnumerable<Person>?> GetPersonByJobTitleAsync(string jobTitle)
        {
            var curentPersons = await _personRepository.GetPersonByJobTitleAsync(jobTitle);
            return curentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByCityAsync(string city)
        {
            var currentPersons = await _personRepository.GetPersonsByCityAsync(city);
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByEducationalInstitutionAsync(string educationalInstitution)
        {
            var currentPersons = await _personRepository.GetPersonsByEducationalInstitutionAsync(educationalInstitution);
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByNameAsync(string name)
        {
            var currentPersons = await _personRepository.GetPersonsByNameAsync(name);
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByPartOfFullNameAsync(string partOfFullName)
        {
            var currentPersons = await _personRepository.GetPersonsByPartOfFullNameAsync(partOfFullName);
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByPatronymicAsync(string patronimyc)
        {
            var currentPersons = await _personRepository.GetPersonsByPatronymicAsync(patronimyc);
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsBySurnameAsync(string surname)
        {
            var currentPersons = await _personRepository.GetPersonsBySurnameAsync(surname);
            return currentPersons;
        }

        public async Task UpdateAsync(Person person)
        {
            _personRepository.Update(person);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
