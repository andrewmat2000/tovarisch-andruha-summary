/*
                        GNU GENERAL PUBLIC LICENSE
                          Version 3, 29 June 2007
 Copyright (C) 2022 Mohammed Ahmed Hussien babiker Free Software Foundation, Inc. <https://fsf.org/>
 Everyone is permitted to copy and distribute verbatim copies
 of this license document, but changing it is not allowed.
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TovarischAndruha.Summary.Auth.Configuration;
using TovarischAndruha.Summary.Auth.Models.Context;
using TovarischAndruha.Summary.Auth.Models.Entities;
using TovarischAndruha.Summary.Auth.Services;
using TovarischAndruha.Summary.Auth.Services.CodeService;
using TovarischAndruha.Summary.Auth.Services.Users;
using TovarischAndruha.Summary.Auth.Validations;

var builder = WebApplication.CreateBuilder(args);
var configServices = builder.Configuration;
var connectionString = "Data Source=test.db";
builder.Services.AddDbContext<BaseDBContext>(op => op.UseSqlite(connectionString));


builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
  options.SignIn.RequireConfirmedEmail = false;
  // options.Password.RequireDigit = true;
  options.Password.RequireLowercase = false;
  options.Password.RequiredLength = 4;
  options.Password.RequireUppercase = false;
  options.Password.RequireNonAlphanumeric = false;
  // options.Lockout.MaxFailedAccessAttempts = 5;
  options.User.RequireUniqueEmail = true;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<BaseDBContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication();

builder.Services.ConfigureApplicationCookie(options => {
  options.LoginPath = "/Accounts/Login";
  options.AccessDeniedPath = "/Accounts/AccessDenied";
  options.ExpireTimeSpan = TimeSpan.FromHours(2);
});


builder.Services.Configure<OAuthServerOptions>(configServices.GetSection("OAuthOptions"));
builder.Services.AddScoped<IAuthorizeResultService, AuthorizeResultService>();
builder.Services.AddSingleton<ICodeStoreService, CodeStoreService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();
builder.Services.AddScoped<ITokenRevocationService, TokenRevocationService>();
builder.Services.AddScoped<ITokenIntrospectionService, TokenIntrospectionService>();
builder.Services.TryAddScoped<ITokenRevocationValidation, TokenRevocationValidation>();
builder.Services.TryAddScoped<ITokenIntrospectionValidation, TokenIntrospectionValidation>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IBearerTokenUsageTypeValidation, BearerTokenUsageTypeValidation>();
builder.Services.AddScoped<IDeviceAuthorizationValidation, DeviceAuthorizationValidation>();
builder.Services.AddScoped<IDeviceAuthorizationService, DeviceAuthorizationService>();

builder.Services.AddHttpContextAccessor();

//builder.Services.Configure<RouteOptions>(options =>
//{
//    options.LowercaseQueryStrings = true;
//    options.LowercaseUrls = true;
//});

builder.Services.AddCors(options => {
  options.AddPolicy("UserInfoPolicy", o => {
    o.AllowAnyOrigin();
    o.AllowAnyHeader();
    o.AllowAnyMethod();
  });
});
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseCors("UserInfoPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
app.Run();
