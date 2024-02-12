using System.Net;
using EasyFinance.Application;
using EasyFinance.Application.Mappers;
using EasyFinance.Domain.Models.AccessControl;
using EasyFinance.Persistence;
using EasyFinance.Persistence.DatabaseContext;
using EasyFinance.Server.Extensions;
using EasyFinance.Server.Mappers;
using EasyFinance.Server.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(typeof(ApiAutoMapperProfiles), typeof(AutoMapperProfiles));

// Add services to the container.
builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddAuthorizationBuilder();

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.Name = "AuthCookie";
        options.Events.OnRedirectToAccessDenied = UnAuthorizedResponse;
        options.Events.OnRedirectToLogin = UnAuthorizedResponse;
    });

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<EasyFinanceDatabaseContext>()
    .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>()
    .AddApiEndpoints();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    // Default SignIn settings.
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGroup("/api/account")
    .MapIdentityApi<User>()
    .WithTags("AccessControl");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMigration();
    app.MapHealthChecks("/healthcheck/readness");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

static Task UnAuthorizedResponse(RedirectContext<CookieAuthenticationOptions> context)
{
    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    return Task.CompletedTask;
}