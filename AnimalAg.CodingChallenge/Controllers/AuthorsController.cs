using AnimalAg.CodingChallenge.Data.Entities.Authors;
using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Service.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAg.CodingChallenge.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        /// <summary>
        /// Provides access to author-related operations.
        /// </summary>
        private readonly IAuthorService authorService;
        /// <summary>
        /// Initializes a new instance of the AuthorsController class with the specified author service.
        /// </summary>
        /// <param name="authorService">The service used to perform author-related operations. Cannot be null.</param>
        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("authors/save")]
        public async Task<CommonResultEntity> SaveAuthorAsync([FromBody] Author model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await authorService.SaveAuthorAsync(model, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("authors/all")]
        public async Task<CommonResultEntity> GetAllAuthorsAsync([FromBody] Author model, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await authorService.GetAllAuthorsAsync(cancellationToken);
                return (new CommonResultEntity() { IsSuccess = true, ErrorCode = string.Empty, ErrorMessage = string.Empty, Result = result });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("authors/catalogById")]
        public async Task<CommonResultEntity> GetAuthorCatalogByIdAsync([FromBody] Author model, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await authorService.GetAuthorCatalogByIdAsync(model, cancellationToken);
                return (new CommonResultEntity() { IsSuccess = true, ErrorCode = string.Empty, ErrorMessage = string.Empty, Result = result });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
