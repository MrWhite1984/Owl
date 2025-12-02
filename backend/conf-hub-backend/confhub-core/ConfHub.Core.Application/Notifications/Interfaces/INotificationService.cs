using ConfHub.Core.Domain.Entities;

namespace ConfHub.Core.Application.Notifications.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> GetNotificationByIdAasync(Guid id);
        Task<IEnumerable<Notification>> GetNotificationsByPersonIdAsync(Guid personId);
        Task<IEnumerable<Notification>> GetPartOfNotificationsByPersonIdAndDateTimeAsync(Guid personId, DateTime startDateTime, int partSize);
        Task<IEnumerable<Notification>> GetUncheckedNotificationsByPersonIdAsync(Guid personId);
        Task AddAsync(Guid targetPersonId, string title, string content, bool isChecked);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(Guid id);
    }
}
