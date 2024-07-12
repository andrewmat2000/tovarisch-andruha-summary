using Summary.Domain.Common;

namespace Summary.Domain.Models;

public class ArticleCategory : BaseDbModel {
  public string Name { get; set; } = null!;
  public string DisplayName { get; set; } = null!;
  public string? Description { get; set; }
}