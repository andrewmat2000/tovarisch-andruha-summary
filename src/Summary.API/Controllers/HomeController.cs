using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Summary.Domain;
using Summary.Domain.Models;
using Summary.Domain.Stores;

namespace Summary.API.Controllers;

[Route("/")]
public class HomeController : ControllerBase {
  private readonly AppConfiguration _options;
  private readonly IArticleStore _articleStore;
  private readonly ILogger<HomeController> _logger;

  [HttpGet()]
  public async Task<Article[]> Index() {
    return await _articleStore.GetArticlesAsync();
  }

  public HomeController(IArticleStore articleStore, IOptions<AppConfiguration> options, ILogger<HomeController> logger) {
    _options = options.Value;
    _articleStore = articleStore;
    _logger = logger;
  }
}