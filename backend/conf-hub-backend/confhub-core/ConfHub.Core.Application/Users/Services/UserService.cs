using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Users.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Guid personId, string role, string passwordHash)
        {
            User user = new User(personId, role, passwordHash);
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByPersonIdAsync(Guid id)
        {
            var currentUser = await _userRepository.GetUsersByPersonIdAsync(id);
            return currentUser;
        }

        public async Task UpdateAsync(User user)
        {
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
