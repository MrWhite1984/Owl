using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Sections.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Sections.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SectionService(ISectionRepository sectionRepository, IUnitOfWork unitOfWork)
        {
            _sectionRepository = sectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Guid conferenceId, string title)
        {
            Section section = new Section(Guid.NewGuid(), conferenceId, title);
            await _sectionRepository.AddAsync(section);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Section?> GetSectionByIdAsync(Guid id)
        {
            var currentSection = await _sectionRepository.GetSectionByIdAsync(id);
            return currentSection;
        }

        public async Task<IEnumerable<Section>> GetSectionsByConferenceIdAsync(Guid conferenceId)
        {
            var currentSections = await _sectionRepository.GetSectionsByConferenceIdAsync(conferenceId);
            return currentSections;
        }

        public async Task UpdateAsync(Section section)
        {
            _sectionRepository.Update(section);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
