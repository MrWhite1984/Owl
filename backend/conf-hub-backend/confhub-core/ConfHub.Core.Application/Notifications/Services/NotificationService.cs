using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.Notifications.Interfaces;
using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = notificationRepository;
        }

        public async Task AddAsync(Guid targetPersonId, string title, string content, bool isChecked)
        {
            Notification notification = new Notification(Guid.NewGuid(), targetPersonId, title, content, isChecked, DateTime.UtcNow);
            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _notificationRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Notification?> GetNotificationByIdAsync(Guid id)
        {
            var currentNotification = await _notificationRepository.GetNotificationByIdAasync(id);
            return currentNotification;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByPersonIdAsync(Guid personId)
        {
            var currentNotifications = await _notificationRepository.GetNotificationsByPersonIdAsync(personId);
            return currentNotifications;
        }

        public async Task<IEnumerable<Notification>> GetPartOfNotificationsByPersonIdAndDateTimeAsync(Guid personId, DateTime startDateTime, int partSize)
        {
            var currentNotifications = await _notificationRepository.GetPartOfNotificationsByPersonIdAndDateTimeAsync(personId, startDateTime, partSize);
            return currentNotifications;
        }

        public async Task<IEnumerable<Notification>> GetUncheckedNotificationsByPersonIdAsync(Guid personId)
        {
            var currentNotifications = await _notificationRepository.GetUncheckedNotificationsByPersonIdAsync(personId);
            return currentNotifications;
        }

        public async Task UpdateAsync(Notification notification)
        {
            _notificationRepository.Update(notification);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
