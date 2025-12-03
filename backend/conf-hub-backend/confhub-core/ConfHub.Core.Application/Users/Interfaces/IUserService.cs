using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Users.Interfaces
{
    public interface IUserService
    {
        Task <IEnumerable<User>> GetUsersByPersonIdAsync(Guid id);
        Task AddAsync(Guid personId, string role, string passwordHash);
        Task UpdateAsync(User user);
    }
}
