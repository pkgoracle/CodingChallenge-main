using AnimalAg.CodingChallenge.Data.Entities.Books;
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
    public class BooksController : ControllerBase
    {
        /// <summary>
        /// Book service instance used for handling business logic related to books. This field is typically initialized through dependency injection and should be used to perform operations such as retrieving, creating, updating, or deleting book data.
        /// </summary>
        private readonly IBookService bookService;
        /// <summary>
        /// Initializes a new instance of the BooksController class with the specified book service.
        /// </summary>
        /// <param name="bookService">The service used to perform book-related operations. Cannot be null.</param>
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("books/save")]
        public async Task<CommonResultEntity> SaveBookAsync([FromBody] Book model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await bookService.SaveBookAsync(model, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("books/update")]
        public async Task<CommonResultEntity> UpdateBookAsync([FromBody] Book model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await bookService.UpdateBookAsync(model, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("books/filter")]
        public async Task<CommonResultEntity> GetAllBooksWithFilterAsync([FromBody] BookRequestEntity model, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await bookService.GetAllBooksWithFilterAsync(model, cancellationToken);
                return (new CommonResultEntity() { IsSuccess = true, ErrorCode = string.Empty, ErrorMessage = string.Empty, Result = result });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("books/catalogById")]
        public async Task<CommonResultEntity> GetBookCatalogByIdAsync([FromBody] BookRequestEntity model, CancellationToken cancellationToken = default)
        {
            try
            {
                var result =  await bookService.GetBookCatalogByIdAsync(model, cancellationToken);
                return (new CommonResultEntity() { IsSuccess = true, ErrorCode = string.Empty, ErrorMessage = string.Empty, Result = result });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("bookauthor/save")]
        public async Task<CommonResultEntity> SaveBookAuthorMappingAsync([FromBody] BookAuthorMapping model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await bookService.SaveBookAuthorMappingAsync(model, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("bookauthor/delete")]
        public async Task<CommonResultEntity> DeleteBookAuthorMappingAsync([FromBody] BookAuthorMapping model, CancellationToken cancellationToken = default)
        {
            try
            {
                return await bookService.DeleteBookAuthorMappingAsync(model, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
