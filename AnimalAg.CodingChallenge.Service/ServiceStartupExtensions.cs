using AnimalAg.CodingChallenge.Service.Interfaces;
using AnimalAg.CodingChallenge.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalAg.CodingChallenge.Service
{
    /// <summary>
    /// Service StartupExtensions class provides extension methods for registering services in the dependency injection container. It contains a method AddServices that registers the necessary services for the application, such as IAuthorService, IBookService, and IStoreService. This class should be called in the startup configuration of the application to ensure that the services are properly registered and can be injected into controllers and other components that require them.
    /// </summary>
    public static class ServiceStartupExtensions
    {
        /// <summary>
        /// Adds application-specific services to the specified service collection.
        /// </summary>
        /// <remarks>This method registers the core services required by the application for dependency
        /// injection. Call this method during application startup to ensure all required services are
        /// available.</remarks>
        /// <param name="services">The service collection to which the application services will be added. Cannot be null.</param>
        public static void AddServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IStoreService, StoreService>();
        }
    }
}
