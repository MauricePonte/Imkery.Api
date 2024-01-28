using Imkery.Domain.Inspections;
using Imkery.Infrastructure.Common.Persistence;

namespace Imkery.Infrastructure.Inspections.Persistence.Repositories;
internal class InspectionItemsRepository(ApplicationDbContext dbContext) 
    : Repository<InspectionItem>(dbContext), IInspectionItemsRepository
{
}