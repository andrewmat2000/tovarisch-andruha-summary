using Duende.IdentityServer.Services;
using TovarischAndruha.Summary.Identity.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICorsPolicyService>((container) => {
  var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
  return new DefaultCorsPolicyService(logger) {
    AllowedOrigins = { AppSettings.HostUrl }
  };
});

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" }));

builder.Services.AddControllersWithViews();

builder.Services.AddMyIdentity();

builder.Services.AddIdentityServer(options => {
  options.Events.RaiseErrorEvents = true;
  options.Events.RaiseInformationEvents = true;
  options.Events.RaiseFailureEvents = true;
  options.Events.RaiseSuccessEvents = true;

  options.EmitStaticAudienceClaim = true;
})
    .AddDbInfrastructure()
    .AddDeveloperSigningCredential()
;

builder.Services.AddAuthentication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
  c.RoutePrefix = "swagger";
});

app.MapControllers();

if (app.Environment.IsDevelopment()) {
  app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
}

app.EnsureSeedData();

app.Use(async (ctx, next) => {
  var serverUrls = ctx.RequestServices.GetRequiredService<IServerUrls>();
  serverUrls.Origin = serverUrls.Origin = AppSettings.HostUrl;

  await next();
});

app.UseRouting();

app.Use((context, next) => {
  context.Request.Scheme = "http";
  return next();
});

app.UseIdentityServer();

app.Run();
