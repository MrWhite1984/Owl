using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfHub.Core.Infrastructure.Persistence.Configurations
{
    public class ProjectParticipantConfiguration : IEntityTypeConfiguration<ProjectParticipant>
    {
        public void Configure(EntityTypeBuilder<ProjectParticipant> builder)
        {
            builder.ToTable("ProjectParticipants");
            builder.HasKey(x => new { x.ProjectId, x.PersonId});

            builder.HasOne<Project>().WithMany().HasForeignKey(x => x.PersonId);
            builder.HasOne<Person>().WithMany().HasForeignKey(y => y.PersonId);
        }
    }
}
