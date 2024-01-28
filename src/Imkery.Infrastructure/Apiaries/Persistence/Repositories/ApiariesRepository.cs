using Imkery.Domain.Apiaries;
using Imkery.Infrastructure.Common.Persistence;

namespace Imkery.Infrastructure.Apiaries.Persistence.Repositories;
internal class ApiariesRepository(ApplicationDbContext _dbContext)
    : Repository<Apiary>(_dbContext), IApiariesRepository
{
}
