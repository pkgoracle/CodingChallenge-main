using AnimalAg.CodingChallenge.EF.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalAg.CodingChallenge.EF
{
    public static class EFStartupExtensions
    {
        /// <summary>
        /// Register DBContext for Entity Framework in the dependency injection container. This method should be called in the startup configuration of the application to ensure that the DbContext is properly registered and can be injected into services and controllers that require database access.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void AddEF(this IServiceCollection services, string? connectionString)
        {
            // Register your EF DbContext here
            //EFDBContext
            services.AddDbContext<EFDBContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("AnimalAg.CodingChallenge.Api")));
        }
    }
}
