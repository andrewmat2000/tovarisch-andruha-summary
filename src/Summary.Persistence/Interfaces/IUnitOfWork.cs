using Microsoft.EntityFrameworkCore;

namespace Summary.Persistence.Interfaces;

public interface IUnitOfWork : IDisposable {
  public Task<int> SaveChangesAsync();
}