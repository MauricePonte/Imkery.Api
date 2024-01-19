namespace Imkery.Domain.Common;
public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
    void Delete(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity, CancellationToken cancellationToken);
}