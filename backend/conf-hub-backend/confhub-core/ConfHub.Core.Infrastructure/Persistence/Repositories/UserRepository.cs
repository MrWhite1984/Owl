using ConfHub.Core.Application.Users.Interfaces;
using ConfHub.Core.Domain.Entities;
using ConfHub.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(User user)
        {
            await _appDbContext.Users.AddAsync(user);
        }

        public async Task<bool> HasAnyUserAsync()
        {
            var anyUsers = await _appDbContext.Users.AnyAsync();
            return anyUsers;
        }

        public async Task<IEnumerable<User>> GetUsersByPersonIdAsync(Guid id)
        {
            var currentUser = await _appDbContext.Users.Where(x => x.PersonId.Equals(id)).ToListAsync();
            return currentUser;
        }

        public void Update(User user)
        {
            _appDbContext.Users.Update(user);
        }
    }
}
