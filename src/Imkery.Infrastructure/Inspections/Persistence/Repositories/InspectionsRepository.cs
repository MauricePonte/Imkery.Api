using Imkery.Domain.Inspections;
using Imkery.Domain.Inspections.Repositories;
using Imkery.Infrastructure.Common.Persistence;

namespace Imkery.Infrastructure.Inspections.Persistence.Repositories;
internal class InspectionsRepository(ApplicationDbContext dbContext) 
    : Repository<Inspection>(dbContext), IInspectionsRepository
{
}