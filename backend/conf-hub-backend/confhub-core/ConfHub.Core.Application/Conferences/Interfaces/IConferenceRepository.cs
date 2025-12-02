using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Conferences.Interfaces
{
    public interface IConferenceRepository
    {
        Task<Conference?> GetByIdAsync(Guid id);
        Task<IEnumerable<Conference>> GetAllAsync();
        Task AddAsync(Conference conference);
        void Update(Conference conference);
    }
}
