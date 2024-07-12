using Summary.Domain.Common;

namespace Summary.Domain.Models;

public class Article : BaseDbModel {
  public string Title { get; set; } = null!;
  public string MarkdownText { get; set; } = null!;
  public IReadOnlyCollection<ArticleCategory> ArticleCategories { get; set; } = [];
}