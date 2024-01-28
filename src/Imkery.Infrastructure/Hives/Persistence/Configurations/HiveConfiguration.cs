using Imkery.Domain.Hives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imkery.Infrastructure.Hives.Persistence.Configurations;
internal class HiveConfiguration : IEntityTypeConfiguration<Hive>
{
    public void Configure(EntityTypeBuilder<Hive> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}
