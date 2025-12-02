using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Conferences.Interfaces
{
    public interface IConferenceRepository
    {
        Task<Conference> GetByIdAsync(Guid Id);
        Task<IEnumerable<Conference>> GetAllAsync();
        Task AddAsync(Conference conference);
        Task UpdateAsync(Conference conference);
    }
}
