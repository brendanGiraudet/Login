using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Login.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void InitialiazeDatabase(this WebApplication webApplication)
        {
            using IServiceScope? serviceScope = webApplication.Services.GetService<IServiceScopeFactory>()?.CreateScope();

            serviceScope?.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            serviceScope?.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
        }
    }
}
