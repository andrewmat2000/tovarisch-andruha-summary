using Microsoft.EntityFrameworkCore;
using Summary.Domain.Dtos;
using Summary.Domain.Models;
using Summary.Domain.Stores;
using Summary.Persistence.Interfaces;

namespace Summary.Persistence;

public class ArticleRepository : IArticleStore {
  private readonly IArticleDbContext _articleDbContext;

  public async Task<bool> AddAsync(Article article) {
    await _articleDbContext.Articles.AddAsync(article);

    return await _articleDbContext.SaveChangesAsync() > 0;
  }

  public async Task<Article?> GetArticleByIdAsync(Guid guid) {
    if (await _articleDbContext.Articles.FirstOrDefaultAsync(x => x.Id == guid) is not Article article) {
      return null;
    }
    return article;
  }

  public async Task<Article[]> GetArticlesAsync(ArticleFilterDto? articleFilterDto) {
    if (articleFilterDto == null) {
      return await _articleDbContext.Articles.ToArrayAsync();
    }

    Func<Article, bool> func = (x) => {
      if (articleFilterDto.ArticleCategories.Length > 0 && !articleFilterDto.ArticleCategories.Contains()) {
        return false;
      }

      return true;
    };

    return _articleDbContext.Articles.Include(x => x.ArticleCategories).Where(func).ToArray();
  }

  public Task<bool> RemoveAsync(Guid guid) {
    throw new NotImplementedException();
  }

  public Task UpdateAsync(Action action) {
    throw new NotImplementedException();
  }

  public ArticleRepository(IArticleDbContext articleDbContext) {
    _articleDbContext = articleDbContext;
  }
}