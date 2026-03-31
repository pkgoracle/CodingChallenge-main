using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;
using AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces;
using AnimalAg.CodingChallenge.Service.Interfaces;

namespace AnimalAg.CodingChallenge.Service.Services
{
    /// <summary>
    /// Provides store-related operations by interacting with the underlying data repository.
    /// </summary>
    /// <remarks>This service acts as an abstraction over the data access layer for store entities. It is
    /// intended to be used as part of the application's business logic layer and should be registered for dependency
    /// injection where store operations are required.</remarks>
    public class StoreService : IStoreService
    {
        /// <summary>
        /// Provides access to store-related data operations using a SQL-based repository.
        /// </summary>
        /// <remarks>Intended for internal use to interact with the underlying store data source. This
        /// field is typically initialized through dependency injection.</remarks>
        private readonly IStoreSqlRepository storeSqlRepository;
        /// <summary>
        /// Initializes a new instance of the StoreService class using the specified store SQL repository.
        /// </summary>
        /// <param name="storeSqlRepository">The repository used to access and manage store data in the underlying SQL database. Cannot be null.</param>
        public StoreService(IStoreSqlRepository storeSqlRepository)
        {
            this.storeSqlRepository = storeSqlRepository;
        }
        public async Task<IEnumerable<StoreDto>> GetAllStoresAsync(StoreRequestEntity model, CancellationToken cancellationToken = default) => await storeSqlRepository.GetAllStoresAsync(model, cancellationToken);
        public async Task<StoreCatalogDto?> GetStoreCatalogByIdAsync(StoreRequestEntity model, CancellationToken cancellationToken = default) => await storeSqlRepository.GetStoreCatalogByIdAsync(model, cancellationToken);
        public async Task<CommonResultEntity> SaveStoreAsync(Store model, CancellationToken cancellationToken = default) => await storeSqlRepository.SaveStoreAsync(model, cancellationToken);
        public async Task<CommonResultEntity> SaveStoreBookMappingAsync(StoreBookMapping model, CancellationToken cancellationToken = default) => await storeSqlRepository.SaveStoreBookMappingAsync(model, cancellationToken);
        public async Task<CommonResultEntity> DeleteStoreBookMappingAsync(StoreBookMapping model, CancellationToken cancellationToken = default) => await storeSqlRepository.DeleteStoreBookMappingAsync(model, cancellationToken);
    }
}
