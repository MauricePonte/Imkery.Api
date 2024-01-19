using Imkery.Domain.Common;

namespace Imkery.Infrastructure.Common.Persistence;
internal abstract class Repository<TEntity>(ApplicationDbContext dbContext)
    : IRepository<TEntity> where TEntity : Entity
{
    public async Task<TEntity?> GetByIdAsync(Guid itemId, CancellationToken cancellationToken)
    {
        return await dbContext.Set<TEntity>().FindAsync(itemId, cancellationToken);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return dbContext.Set<TEntity>().AsEnumerable();
    }

    public IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate)
    {
        return dbContext.Set<TEntity>()
            .Where(predicate).AsEnumerable();
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
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
