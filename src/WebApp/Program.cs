var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot/build");

builder.Services.AddMyAuthentication();

if (builder.Environment.IsDevelopment()) {
  builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" }));
}

var app = builder.Build();

app.UseMyAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
    c.RoutePrefix = "swagger";
  });
}


app.UseSvelteSpaRouting(["/api"]);
app.UseSpaStaticFiles();
app.UseSpa(configuration => configuration.Options.SourcePath = "wwwroot/build");

app.Run();
