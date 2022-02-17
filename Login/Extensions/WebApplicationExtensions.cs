using IdentityServer4.EntityFramework.DbContexts;
using Login.Data;
using Microsoft.EntityFrameworkCore;

namespace Login.Extensions;

public static class WebApplicationExtensions
{
    public static void InitialiazeDatabase(this WebApplication webApplication)
    {
        using var serviceScope = webApplication.Services.GetService<IServiceScopeFactory>()?.CreateScope();

        serviceScope?.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        serviceScope?.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
        serviceScope?.ServiceProvider.GetRequiredService<LoginDbContext>().Database.Migrate();
    }
}