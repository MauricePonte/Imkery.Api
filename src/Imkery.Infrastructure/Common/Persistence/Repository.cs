using Imkery.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Imkery.Infrastructure.Common.Persistence;
internal abstract class Repository<TEntity>(DbContext dbContext)
    : IRepository<TEntity> where TEntity : Entity
{
    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid itemId, CancellationToken cancellationToken)
    {
        return await dbContext.Set<TEntity>().FindAsync(itemId, cancellationToken);
    }

    public void Update(TEntity entity, CancellationToken cancellationToken)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity, CancellationToken cancellationToken)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }
}
