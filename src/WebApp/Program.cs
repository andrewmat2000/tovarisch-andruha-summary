using TovarischAndruha.Summary.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot/build");

var app = builder.Build();

app.UseSvelteSpaRouting(["/api"]);
app.UseSpaStaticFiles();
app.UseSpa(configuration => configuration.Options.SourcePath = "wwwroot/build");

app.Run();
