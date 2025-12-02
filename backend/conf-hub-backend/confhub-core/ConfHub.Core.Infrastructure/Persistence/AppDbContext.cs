using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfHub.Core.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons => Set<Person>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Conference> Conferences => Set<Conference>();
        public DbSet<ConferenceSettings> ConferenceSettings => Set<ConferenceSettings>();
        public DbSet<News> News => Set<News>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectParticipant> ProjectParticipants => Set<ProjectParticipant>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
