using ConfHub.Core.Application.Persons.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _appDbContext;

        public PersonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Person person)
        {
            await _appDbContext.Persons.AddAsync(person);
        }

        public async Task<Person?> GetPersonByEmailAsync(string email)
        {
            var currentPerson = await _appDbContext.Persons.FirstOrDefaultAsync(x => x.Email.Equals(email));
            return currentPerson;
        }

        public async Task<Person?> GetPersonByIdAsync(Guid id)
        {
            var currentPerson = await _appDbContext.Persons.FindAsync(id);
            return currentPerson;
        }

        public async Task<IEnumerable<Person>?> GetPersonByJobTitleAsync(string jobTitle)
        {
            var currentPersons = await _appDbContext.Persons.Where(x => x.JobTitle.Equals(jobTitle)).ToListAsync();
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByCityAsync(string city)
        {
            var currentPersons = await _appDbContext.Persons.Where(x => x.City.Equals(city)).ToListAsync();
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByEducationalInstitutionAsync(string educationalInstitution)
        {
            var currentPersons = await _appDbContext.Persons.Where(x => x.EducationalInstitution.Equals(educationalInstitution)).ToListAsync();
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByNameAsync(string name)
        {
            var currentPersons = await _appDbContext.Persons.Where(x => x.Name.Equals(name)).ToListAsync();
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByPartOfFullNameAsync(string partOfFullName)
        {
            var currentPersons = await _appDbContext.Persons
                .Where(x => string.Join(' ', new { x.Surname, x.Name, x.Patronymic}).Contains(partOfFullName))
                .ToListAsync();
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsByPatronimycAsync(string patronimyc)
        {
            var currentPersons = await _appDbContext.Persons.Where(x => x.Patronymic.Equals(patronimyc)).ToListAsync();
            return currentPersons;
        }

        public async Task<IEnumerable<Person>?> GetPersonsBySurnameAsync(string surname)
        {
            var currentPersons = await _appDbContext.Persons.Where(x => x.Name.Equals(surname)).ToListAsync();
            return currentPersons;
        }

        public void Update(Person person)
        {
            _appDbContext.Persons.Update(person);
        }
    }
}
