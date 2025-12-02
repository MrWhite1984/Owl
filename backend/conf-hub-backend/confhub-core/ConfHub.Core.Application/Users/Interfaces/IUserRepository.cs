using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByPersonIdAsync(Guid id);
        Task AddAsync (User user);
        Task UpdateAsync (User user);
    }
}
