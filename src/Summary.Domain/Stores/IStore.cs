using Summary.Domain.Common;

namespace Summary.Domain.Stores;

public interface IStore<T> where T : BaseDbModel {
  public Task<T?> GetArticleByIdAsync(Guid guid);
  public Task<bool> AddAsync(T article);
  public Task<bool> RemoveAsync(Guid guid);
  public Task UpdateAsync(Action action);
}