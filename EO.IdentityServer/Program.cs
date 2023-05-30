using EO.IdentityServer.IdentityConfiguration;
using IdentityServer4.Extensions;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.HttpLogging;


var builder = WebApplication.CreateBuilder(args);

var origins = builder.Configuration.GetSection("AllowedOrigins").AsEnumerable()
    .Select(x => x.Value)
    .Where(x => !x.IsNullOrEmpty())
    .ToArray();

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryClients(IdentityConfig.GetClients(origins.First()))
    .AddInMemoryApiResources(IdentityConfig.ApiResources)
    .AddTestUsers(TestUsers.Users)
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All ^ HttpLoggingFields.RequestHeaders;
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();

app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

app.UseHttpLogging();

app.UseCors();

app.Run();
