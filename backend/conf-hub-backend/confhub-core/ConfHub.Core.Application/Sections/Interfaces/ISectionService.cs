using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Sections.Interfaces
{
    public interface ISectionService
    {
        Task<Section?> GetSectionByIdAsync(Guid id);
        Task<IEnumerable<Section>> GetSectionsByConferenceIdAsync(Guid conferenceId);
        Task AddAsync(Guid conferenceId, string title);
        Task UpdateAsync(Section section);
    }
}
