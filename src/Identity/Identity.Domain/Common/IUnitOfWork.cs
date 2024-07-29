namespace TovarischAndruha.Summary.Identity.Domain.Common;

public interface IUnitOfWork {
  public int SaveChanges();
  public Task<int> SaveChangesAsync();
}