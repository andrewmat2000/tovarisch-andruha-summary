using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TovarischAndruha.Summary.Identity.Data;
using TovarischAndruha.Summary.Identity.Extensions;

var builder = WebApplication.CreateBuilder(args);

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
const string ConnectionString = "Data Source = test.db";

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(ConnectionString));

builder.Services.AddIdentityServer()
  .AddConfigurationStore(options => {
    options.ConfigureDbContext = b => b.UseSqlite(ConnectionString,
      sql => sql.MigrationsAssembly(migrationsAssembly));
  })
  .AddOperationalStore(options => {
    options.ConfigureDbContext = b => b.UseSqlite(ConnectionString,
      sql => sql.MigrationsAssembly(migrationsAssembly));
  })
;

builder.Services.AddIdentity<AppUser, IdentityRole>(x => {
  x.Password.RequireNonAlphanumeric = false;
  x.Password.RequireDigit = false;
  x.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

app.MapControllers();

app.UseIdentityServer();

app.SeedDb();

app.Run();
