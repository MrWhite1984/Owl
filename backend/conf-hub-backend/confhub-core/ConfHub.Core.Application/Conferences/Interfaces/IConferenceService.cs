using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Conferences.Interfaces
{
    public interface IConferenceService
    {
        Task<Conference?> GetByIdAsync(Guid id);
        Task<IEnumerable<Conference>> GetAllAsync();
        Task AddAsync(string title, DateTime startDate, bool isActive, bool isDeleted);
        void Update(Conference conference);
    }
}
