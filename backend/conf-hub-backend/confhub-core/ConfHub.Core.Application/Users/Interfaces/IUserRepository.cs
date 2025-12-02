using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersByPersonIdAsync(Guid id);
        Task AddAsync (User user);
        void Update (User user);
    }
}
