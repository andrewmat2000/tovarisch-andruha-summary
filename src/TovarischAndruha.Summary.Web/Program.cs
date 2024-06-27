using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
    options.Authority = Environment.GetEnvironmentVariable("AUTH_SERVER_URL");
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters {
      //ValidateIssuer = true,
      //ValidateAudience = true,
      //ValidateIssuerSigningKey = true,

      ValidateAudience = false,
      ValidateIssuer = false,
    };
  });

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddControllers();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

app.UseMvc()
   .UseRouting();
app.MapControllers();

app.Use(async (ctx, next) => {
  if (ctx.Request.Cookies.ContainsKey("Authorization") && !ctx.Request.Headers.ContainsKey("Authorization")) {
    ctx.Request.Headers.Authorization = $"Bearer {ctx.Request.Cookies["Authorization"]}";
  }

  await next();
});

app.Use(async (ctx, next) => {
  if (ctx.User.Identity?.IsAuthenticated == true && ctx.Request.Path.StartsWithSegments("/users/sign_in")) {
    ctx.Response.Redirect("/data");
    return;
  }

  if (ctx.User.Identity?.IsAuthenticated != true &&
        !ctx.Request.Path.StartsWithSegments("/users") &&
        ctx.Request.Headers["Sec-Fetch-Dest"] == "document") {

    ctx.Response.StatusCode = 401;
    ctx.Response.Redirect("/users/sign_in");
    return;
  }

  await next();
});

app.UseSpa(spa => {
  spa.UseProxyToSpaDevelopmentServer(Environment.GetEnvironmentVariable("SPA_DEV_SERVER_URL"));
});

app.Run();
