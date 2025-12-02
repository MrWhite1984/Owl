using ConfHub.Core.Application.Notifications.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _appDbContext;

        public NotificationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Notification notification)
        {
            await _appDbContext.Notifications.AddAsync(notification);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _appDbContext.Notifications.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync();
        }

        public async Task<Notification?> GetNotificationByIdAasync(Guid id)
        {
            var currentNotification = await _appDbContext.Notifications.FindAsync(id);
            return currentNotification;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByPersonIdAsync(Guid personId)
        {
            var currentNotifications = await _appDbContext.Notifications.Where(x => x.TargetPersonId.Equals(personId)).ToListAsync();
            return currentNotifications;
        }

        public async Task<IEnumerable<Notification>> GetPartOfNotificationsByPersonIdAndDateTimeAsync(Guid personId, DateTime startDateTime, int partSize)
        {
            var currentNotifications = await _appDbContext.Notifications
                .Where(x => x.TargetPersonId.Equals(personId))
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.CreatedAt < startDateTime)
                .Take(partSize + 1)
                .ToListAsync();
            return currentNotifications;
        }

        public async Task<IEnumerable<Notification>> GetUncheckedNotificationsByPersonIdAsync(Guid personId)
        {
            var currentNotifications = await _appDbContext.Notifications
                .Where(x => x.TargetPersonId.Equals(personId) && !x.IsChecked)
                .ToListAsync();
            return currentNotifications;
        }

        public void Update(Notification notification)
        {
            _appDbContext.Notifications.Update(notification);
        }
    }
}
