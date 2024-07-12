using Summary.Domain;
using Summary.Domain.Stores;
using Summary.Infrastucture;
using Summary.Persistence;
using Summary.Persistence.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<AppConfiguration>();

builder.Services.AddDbContext<IArticleDbContext, AppDbContext>();

builder.Services.AddTransient<IArticleStore, ArticleRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

#if DEBUG
using var seedContext = new AppDbContextSeed();

await seedContext.SeedAsync();
#endif

app.Run();
