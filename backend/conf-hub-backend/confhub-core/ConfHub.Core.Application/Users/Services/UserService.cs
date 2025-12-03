using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Users.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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

        public async Task AddAsync(Guid personId, string role)
        {
            User user = new User(personId, role);
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var postgresEx = ex.InnerException as PostgresException
                      ?? ex.InnerException?.InnerException as PostgresException;

                if (postgresEx != null && postgresEx.SqlState == PostgresErrorCodes.ForeignKeyViolation)
                {
                    if (postgresEx.ConstraintName?.Contains("person", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        throw new ArgumentException($"Person с ID '{personId}' не существует.", nameof(personId));
                    }

                    throw new ArgumentException($"Указанный Person (ID: {personId}) не найден.", nameof(personId));
                }
                throw new InvalidOperationException("Ошибка при создании пользователя.", ex);
            }
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
