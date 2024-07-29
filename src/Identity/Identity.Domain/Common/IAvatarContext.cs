using Microsoft.EntityFrameworkCore;
using TovarischAndruha.Summary.Identity.Domain.Entities;

namespace TovarischAndruha.Summary.Identity.Domain.Common;

public interface IAvatarContext : IUnitOfWork {
  public DbSet<Avatar> Avatars { get; set; }
}