using static System.Collections.Specialized.BitVector32;

namespace ConfHub.Core.Application.Sections.Interfaces
{
    public interface ISectionService
    {
        Task<Section?> GetSectionByIdAsync(Guid id);
        Task<IEnumerable<Section>> GetSectionsByConferenceId(Guid conferenceId);
        Task AddAsync(Guid conferenceId, string title, bool isDeleted);
        Task UpdateAsync(Section section);
    }
}
