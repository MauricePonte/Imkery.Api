using Imkery.Application.Common.Interfaces;
using Imkery.Domain.Apiaries;
using Imkery.Domain.Hives;
using Imkery.Domain.Locations;
using Microsoft.EntityFrameworkCore;

namespace Imkery.Infrastructure.Common.Persistence;
internal class ApplicationDbContext(DbContextOptions options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Apiary> Apiaries { get; set; } = null!;
    public DbSet<Hive> Hives { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ComplexProperties<Coordinates>();
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}
