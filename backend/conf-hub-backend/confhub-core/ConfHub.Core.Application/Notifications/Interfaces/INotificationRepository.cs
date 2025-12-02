using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Notifications.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotificationByIdAasync(Guid id);
        Task<IEnumerable<Notification>> GetNotificationsByPersonIdAsync(Guid personId);
        Task<IEnumerable<Notification>> GetPartOfNotificationsByPersonIdAndDateTimeAsync(Guid personId, DateTime startDateTime, int partSize);
        Task<IEnumerable<Notification>> GetUncheckedNotificationsByPersonIdAsync(Guid personId);
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(Guid id);
    }
}
