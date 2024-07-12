using Microsoft.EntityFrameworkCore;
using Summary.Domain.Models;

namespace Summary.Persistence.Interfaces;

public interface IArticleDbContext : IUnitOfWork {
  public DbSet<Article> Articles { get; set; }
}