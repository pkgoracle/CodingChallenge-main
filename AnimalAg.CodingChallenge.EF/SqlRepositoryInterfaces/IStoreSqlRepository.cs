using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;

namespace AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces
{
    public interface IStoreSqlRepository
    {
        Task<IEnumerable<StoreDto>> GetAllStoresAsync(StoreRequestEntity model, CancellationToken cancellationToken = default);
        Task<StoreCatalogDto?> GetStoreCatalogByIdAsync(StoreRequestEntity model, CancellationToken cancellationToken = default);
        Task<CommonResultEntity> SaveStoreAsync(Store model, CancellationToken cancellationToken = default);
        Task<CommonResultEntity> SaveStoreBookMappingAsync(StoreBookMapping model, CancellationToken cancellationToken = default);
        Task<CommonResultEntity> DeleteStoreBookMappingAsync(StoreBookMapping model, CancellationToken cancellationToken = default);
    }
}
