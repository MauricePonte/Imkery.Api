namespace Imkery.Domain.Common;
public interface IRepository<TEntity> where TEntity : Entity
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
    void Delete(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(Guid itemId, CancellationToken cancellationToken);
    void Update(TEntity entity, CancellationToken cancellationToken);
}