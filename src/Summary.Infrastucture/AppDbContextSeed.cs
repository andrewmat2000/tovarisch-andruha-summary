using Summary.Infrastucture;

public class AppDbContextSeed : AppDbContext {

  public async Task SeedAsync() {
    if (!Articles.Any()) {
      await Articles.AddAsync(new Summary.Domain.Models.Article() {
        ArticleCategories = [],
        MarkdownText = "ASdasdasdasdd",
        Title = "sadasdasd"
      });
    }

    await SaveChangesAsync();
  }
}