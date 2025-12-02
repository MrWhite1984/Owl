using ConfHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfHub.Core.Infrastructure.Persistence.Configurations
{
    public class ConferenceSettingsConfiguration : IEntityTypeConfiguration<ConferenceSettings>
    {
        public void Configure(EntityTypeBuilder<ConferenceSettings> builder)
        {
            builder.ToTable("ConferenceSettings");
            builder.HasKey(x => x.ConferenceId);

            builder.HasOne<Conference>().WithOne().HasForeignKey<ConferenceSettings>(x => x.ConferenceId);
        }
    }
}
