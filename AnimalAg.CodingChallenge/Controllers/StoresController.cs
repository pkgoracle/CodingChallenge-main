using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;
using AnimalAg.CodingChallenge.Service.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAg.CodingChallenge.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        /// <summary>
        /// Provides access to store-related operations through the IStoreService implementation.
        /// </summary>
        private readonly IStoreService storeService;
        /// <summary>
        /// Initializes a new instance of the StoresController class with the specified store service.
        /// </summary>
        /// <param name="storeService">The service used to perform store-related operations. Cannot be null.</param>
        public StoresController(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("stores/all")]
        public async Task<CommonResultEntity> GetAllStoresAsync([FromBody] StoreRequestEntity model, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await storeService.GetAllStoresAsync(model, cancellationToken);
                return (new CommonResultEntity() { IsSuccess = true, ErrorCode = string.Empty, ErrorMessage = string.Empty, Result = result });
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("stores/catalogById")]
        public async Task<CommonResultEntity> GetStoreCatalogByIdAsync([FromBody] StoreRequestEntity model, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await storeService.GetStoreCatalogByIdAsync(model, cancellationToken);
                return (new CommonResultEntity() { IsSuccess = true, ErrorCode = string.Empty, ErrorMessage = string.Empty, Result = result });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("stores/save")]
        public async Task<CommonResultEntity> SaveStoreAsync([FromBody] Store model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await storeService.SaveStoreAsync(model, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("storebooks/save")]
        public async Task<CommonResultEntity> SaveStoreBookMappingAsync([FromBody] StoreBookMapping model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await storeService.SaveStoreBookMappingAsync(model, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("storebooks/delete")]
        public async Task<CommonResultEntity> DeleteStoreBookMappingAsync([FromBody] StoreBookMapping model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await storeService.DeleteStoreBookMappingAsync(model, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
