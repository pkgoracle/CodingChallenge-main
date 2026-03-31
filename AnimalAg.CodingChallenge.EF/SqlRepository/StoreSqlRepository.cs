using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;
using AnimalAg.CodingChallenge.EF.DBContext;
using AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalAg.CodingChallenge.EF.SqlRepository
{
    /// <summary>
    /// Provides methods for accessing and managing store data using a SQL database context.
    /// </summary>
    /// <remarks>This repository implements data access operations for stores, abstracting the underlying
    /// Entity Framework database context. It is intended to be used as part of the application's data access
    /// layer.</remarks>
    public class StoreSqlRepository : IStoreSqlRepository
    {
        /// <summary>
        /// EFDBContext instance used for accessing the database. This field is typically initialized through dependency injection and should be used to perform database operations related to stores.
        /// </summary>
        private readonly EFDBContext dbContext;
        /// <summary>
        /// Initializes a new instance of the StoreSqlRepository class using the specified database context.
        /// </summary>
        /// <param name="_dbContext">The EFDBContext instance to be used for database operations. Cannot be null.</param>
        public StoreSqlRepository(EFDBContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        /// <summary>
        /// Asynchronously retrieves all active stores that match the specified request criteria.
        /// </summary>
        /// <param name="model">The request parameters used to filter or customize the store retrieval operation.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of store data
        /// transfer objects for all active stores matching the request.</returns>
        public async Task<IEnumerable<StoreDto>> GetAllStoresAsync(StoreRequestEntity model, CancellationToken cancellationToken = default)
        {
            return await dbContext.Stores
                        .Where(s => s.IsActive) // filter active stores
                        .Select(s => new StoreDto()
                        {
                            StoreId = s.Id,
                            StoreName = s.Name,
                            StoreLocation = s.Location
                        })
                        .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Asynchronously retrieves the catalog information for an active store by its identifier.
        /// </summary>
        /// <remarks>The returned catalog includes only active books associated with the specified
        /// store.</remarks>
        /// <param name="model">The request entity containing the identifier of the store to retrieve.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a StoreCatalogDto with the
        /// store's catalog information if found and active; otherwise, null.</returns>
        public async Task<StoreCatalogDto?> GetStoreCatalogByIdAsync(StoreRequestEntity model, CancellationToken cancellationToken = default)
        {
            return await dbContext.Stores
                        .Where(s => s.IsActive && s.Id == model.Id) // filter active store
                        .Include(s => s.StoreBooks)
                            .ThenInclude(sb => sb.Book)
                                .ThenInclude(b => b.BookAuthors)
                                    .ThenInclude(ba => ba.Author)
                        .Select(s => new StoreCatalogDto()
                        {
                            StoreId = s.Id,
                            StoreName = s.Name,
                            StoreLocation = s.Location,

                            Books = s.StoreBooks
                                .Where(sb => sb.Book.IsActive && sb.StoreId == s.Id) // filter active books connected with store
                                .Select(sb => new BookDto()
                                {
                                    BookId = sb.Book.Id,
                                    BookName = sb.Book.Name,
                                    ISBN = sb.Book.ISBN,
                                    Price = sb.Book.Price,
                                    StoreBookPrice = sb.StoreBookPrice,
                                    Inventory = sb.Inventory
                                })
                                .ToList()
                        })
                        .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Asynchronously validates and saves a store entity to the database if it does not already exist with the same
        /// name and location.
        /// </summary>
        /// <remarks>If a store with the same name and location already exists, the operation does not
        /// create a new store and returns an error result. The method performs validation before attempting to save the
        /// store.</remarks>
        /// <param name="model">The store entity to validate and save. Must not be null and must contain valid store details.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>A CommonResultEntity indicating the outcome of the operation. If successful, the Result property contains
        /// the ID of the newly created store. If validation fails or a duplicate store exists, returns an error with
        /// the appropriate error code and message.</returns>
        public async Task<CommonResultEntity> SaveStoreAsync(Store model, CancellationToken cancellationToken = default)
        {
            var validateResult = ValidateStoreDetail(model);
            if (!validateResult.IsSuccess)
                return validateResult;

            // Use AnyAsync instead of FirstOrDefaultAsync when only existence matters
            bool storeExists = await dbContext.Stores
                .AnyAsync(s => s.Name == model.Name && s.Location == model.Location, cancellationToken);

            if (storeExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Store_005",
                    ErrorMessage = "Store with the same name and location already exists."
                };
            }

            await dbContext.Stores.AddAsync(model, cancellationToken);

            //async save
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CommonResultEntity
            {
                IsSuccess = true,
                ErrorCode = "Error_Store_100",
                ErrorMessage = "Record saved successfully",
                Result = model.Id
            };
        }

        /// <summary>
        /// Valid Store detail common method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CommonResultEntity ValidateStoreDetail(Store model)
        {
            if (model == null)
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Store_001", ErrorMessage = "Please provide store detail" };

            if (string.IsNullOrEmpty(Convert.ToString(model.Id)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Store_002", ErrorMessage = "Please provide store Id" };

            if (string.IsNullOrEmpty(model.Name))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Store_003", ErrorMessage = "Please provide store name" };

            if (string.IsNullOrEmpty(model.Location))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Store_004", ErrorMessage = "Please provide store location" };

            return new CommonResultEntity() { IsSuccess = true }; //all data is fine
        }

        /// <summary>
        /// Validate Store Book Detail Common Method
        /// </summary>
        /// <param name="model"></param>
        /// <param name="IsSave"></param>
        /// <returns></returns>
        private CommonResultEntity ValidateStoreBookDetail(StoreBookMapping model, bool IsSave = false)
        {
            if (model == null)
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Store_006", ErrorMessage = "Please provide store book detail" };

            if (IsSave && string.IsNullOrEmpty(Convert.ToString(model.Id)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Store_007", ErrorMessage = "Please provide store book Id" };

            if (string.IsNullOrEmpty(Convert.ToString(model.StoreId)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Store_008", ErrorMessage = "Please provide store Id" };

            if (string.IsNullOrEmpty(Convert.ToString(model.BookId)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Store_009", ErrorMessage = "Please provide book Id" };

            return new CommonResultEntity() { IsSuccess = true }; //all data is fine
        }

        /// <summary>
        /// Save Store Book Mapping
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonResultEntity> SaveStoreBookMappingAsync(StoreBookMapping model, CancellationToken cancellationToken = default)
        {
            var validateResult = ValidateStoreBookDetail(model, true);
            if (!validateResult.IsSuccess)
                return validateResult;

            // Check if store exists and is active
            bool storeExists = await dbContext.Stores
                .AnyAsync(s => s.IsActive && s.Id == model.StoreId, cancellationToken);

            if (!storeExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Store_010",
                    ErrorMessage = "Please provide a valid store Id"
                };
            }

            // Check if book exists and is active
            bool bookExists = await dbContext.Books
                .AnyAsync(b => b.IsActive && b.Id == model.BookId, cancellationToken);

            if (!bookExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Store_011",
                    ErrorMessage = "Please provide a valid book Id"
                };
            }

            await dbContext.StoreBookMappings.AddAsync(model, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CommonResultEntity
            {
                IsSuccess = true,
                ErrorCode = "Error_Store_100",
                ErrorMessage = "Record saved successfully",
                Result = model.Id
            };
        }


        /// <summary>
        /// Delete Store Book Mapping by updating IsActive flag
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonResultEntity> DeleteStoreBookMappingAsync(StoreBookMapping model, CancellationToken cancellationToken = default)
        {
            var validateResult = ValidateStoreBookDetail(model);
            if (!validateResult.IsSuccess)
                return validateResult;

            // Find the mapping
            var storeBookMapping = await dbContext.StoreBookMappings
                .FirstOrDefaultAsync(s => s.IsActive && s.StoreId == model.StoreId && s.BookId == model.BookId, cancellationToken);

            if (storeBookMapping == null)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Store_012",
                    ErrorMessage = "Store book mapping doesn't exist"
                };
            }

            await dbContext.Stores
                 .Where(b => b.Id == model.Id) // filter the store you want to update
                 .ExecuteUpdateAsync(
                     setters => setters
                         .SetProperty(b => b.ModifiedBy, model.ModifiedBy)
                         .SetProperty(b => b.ModifiedDate, model.ModifiedDate)
                         .SetProperty(b => b.IsActive, false),
                     cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CommonResultEntity
            {
                IsSuccess = true,
                ErrorCode = "Error_Store_101",
                ErrorMessage = "Record deleted successfully",
                Result = model.Id
            };
        }
    }
}
