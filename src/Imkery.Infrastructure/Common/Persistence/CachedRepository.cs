using Imkery.Domain.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Imkery.Infrastructure.Common.Persistence;
internal class CachedRepository<TEntity>(IReadOnlyRepository<TEntity> _repository, IMemoryCache _cache)
    : IReadOnlyRepository<TEntity> where TEntity : Entity
{
    public Task<TEntity?> GetByIdAsync(Guid itemId, CancellationToken cancellationToken)
    {
        var key = $"{nameof(TEntity)}-{itemId}";
        return _cache.GetOrCreateAsync(key, async entry =>
        {
            return await _repository.GetByIdAsync(itemId,cancellationToken);
        });
    }

    public IEnumerable<TEntity> GetAll()
    {
        var key = $"{nameof(TEntity)}-all";
        var result = _cache.GetOrCreate(key, entry =>
        {
            return _repository.GetAll();
        });
        return result ?? Enumerable.Empty<TEntity>();
    }

    public IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate)
    {
        var key = $"{nameof(TEntity)}-all";
        var result = _cache.GetOrCreate(key, entry =>
        {
            return _repository.GetWhere(predicate);
        });
        return result ?? Enumerable.Empty<TEntity>();
    }
}
