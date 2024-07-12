using Summary.Domain.Models;

namespace Summary.Domain.Dtos;

public record ArticleFilterDto(ArticleCategory[] ArticleCategories, string[] Names, string ContainingText);