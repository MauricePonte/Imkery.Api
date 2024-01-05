using Imkery.Domain.Hives;
using Imkery.Infrastructure.Common.Persistence;

namespace Imkery.Infrastructure.Hives.Persistence;
internal class HivesRepository(ApplicationDbContext _dbContext)
    : Repository<Hive>(_dbContext), IHivesRepository
{
}
