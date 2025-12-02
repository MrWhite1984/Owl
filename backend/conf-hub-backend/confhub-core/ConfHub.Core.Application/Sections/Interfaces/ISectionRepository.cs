using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Sections.Interfaces
{
    public interface ISectionRepository
    {
        Task<Section?> GetSectionByIdAsync(Guid id);
        Task<IEnumerable<Section>> GetSectionsByConferenceId(Guid conferenceId);
        Task AddAsync(Section section);
        void Update(Section section);
    }
}
