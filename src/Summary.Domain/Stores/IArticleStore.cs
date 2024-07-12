using Summary.Domain.Dtos;
using Summary.Domain.Models;

namespace Summary.Domain.Stores;

public interface IArticleStore : IStore<Article> {
  public Task<Article[]> GetArticlesAsync(ArticleFilterDto? articleFilterDto = null);
}