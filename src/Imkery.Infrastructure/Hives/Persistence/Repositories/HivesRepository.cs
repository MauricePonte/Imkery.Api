using Imkery.Domain.Hives;
using Imkery.Infrastructure.Common.Persistence;

namespace Imkery.Infrastructure.Hives.Persistence.Repositories;
internal class HivesRepository(ApplicationDbContext dbContext)
    : Repository<Hive>(dbContext), IHivesRepository
{
}
