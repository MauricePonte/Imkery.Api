using Imkery.Domain.Apiaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imkery.Infrastructure.Apiaries.Persistence;
internal class ApiaryConfiguration : IEntityTypeConfiguration<Apiary>
{
    public void Configure(EntityTypeBuilder<Apiary> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        //builder.Property<List<Guid>>("_hiveIds") // TODO: Fix
        //    .HasColumnName("HiveIds")
        //    .HasListOfIdsConverter();

        builder.Property(x => x.Name);

        builder.ComplexProperty(x => x.Coordinate);
    }
}
