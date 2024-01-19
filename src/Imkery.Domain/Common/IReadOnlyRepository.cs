namespace Imkery.Domain.Common;
public interface IReadOnlyRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid itemId, CancellationToken cancellationToken);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate);
}
