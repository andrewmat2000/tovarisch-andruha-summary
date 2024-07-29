using Microsoft.EntityFrameworkCore;

namespace AmwesProtocol.Infrastructure.Common;

public interface IDbContextOptionsBuilderAction<T> where T : DbContext {
  public Action<DbContextOptionsBuilder> GetDbContextOptionsBuilder();
}