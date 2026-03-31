using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;

namespace AnimalAg.CodingChallenge.Service.Interfaces
{
    /// <summary>
    /// Defines a contract for retrieving store information asynchronously.
    /// </summary>
    /// <remarks>Implementations of this interface should provide methods to access store data, typically from
    /// a database or external service. Methods are asynchronous to support non-blocking operations and
    /// cancellation.</remarks>
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllStoresAsync(StoreRequestEntity model, CancellationToken cancellationToken = default);
        Task<StoreCatalogDto?> GetStoreCatalogByIdAsync(StoreRequestEntity model, CancellationToken cancellationToken = default);
        Task<CommonResultEntity> SaveStoreAsync(Store model, CancellationToken cancellationToken = default);
        Task<CommonResultEntity> SaveStoreBookMappingAsync(StoreBookMapping model, CancellationToken cancellationToken = default);
        Task<CommonResultEntity> DeleteStoreBookMappingAsync(StoreBookMapping model, CancellationToken cancellationToken = default);
    }
}
