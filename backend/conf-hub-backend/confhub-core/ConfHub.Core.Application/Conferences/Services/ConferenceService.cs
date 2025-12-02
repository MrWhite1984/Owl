using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Conferences.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Conferences.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConferenceService(IConferenceRepository conferenceRepository, IUnitOfWork unitOfWork)
        {
            _conferenceRepository = conferenceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(string title, DateTime startDate)
        {
            Conference conference = new Conference(Guid.NewGuid(), title, startDate, false, string.Empty, string.Empty);
            await _conferenceRepository.AddAsync(conference);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Conference>> GetAllAsync()
        {
            var currentConferences = await _conferenceRepository.GetAllAsync();
            return currentConferences;
        }

        public async Task<Conference?> GetByIdAsync(Guid id)
        {
            var currentConference = await _conferenceRepository.GetByIdAsync(id);
            return currentConference;
        }

        public async Task UpdateAsync(Conference conference)
        {
            _conferenceRepository.Update(conference);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
