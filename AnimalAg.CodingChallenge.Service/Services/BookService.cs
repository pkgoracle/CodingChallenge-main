using AnimalAg.CodingChallenge.Data.Entities.Books;
using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;
using AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces;
using AnimalAg.CodingChallenge.Service.Interfaces;

namespace AnimalAg.CodingChallenge.Service.Services
{
    public class BookService : IBookService
    {
        /// <summary>
        /// Provides access to store-related data operations using a SQL-based repository.
        /// </summary>
        /// <remarks>Intended for internal use to interact with the underlying store data source. This
        /// field is typically initialized through dependency injection.</remarks>
        private readonly IBookSqlRepository bookSqlRepository;
        /// <summary>
        /// Initializes a new instance of the StoreService class using the specified store SQL repository.
        /// </summary>
        /// <param name="storeSqlRepository">The repository used to access and manage store data in the underlying SQL database. Cannot be null.</param>
        public BookService(IBookSqlRepository bookSqlRepository)
        {
            this.bookSqlRepository = bookSqlRepository;
        }
        public async Task<CommonResultEntity> SaveBookAsync(Book model, CancellationToken cancellationToken = default)=> await bookSqlRepository.SaveBookAsync(model, cancellationToken);
        public async Task<CommonResultEntity> UpdateBookAsync(Book model, CancellationToken cancellationToken = default)=> await bookSqlRepository.UpdateBookAsync(model, cancellationToken);
        public async Task<PagedResult<BookDto>> GetAllBooksWithFilterAsync(BookRequestEntity model, CancellationToken cancellationToken = default)=> await bookSqlRepository.GetAllBooksWithFilterAsync(model, cancellationToken);
        public async Task<BookCatalogDto?> GetBookCatalogByIdAsync(BookRequestEntity model, CancellationToken cancellationToken = default)=> await bookSqlRepository.GetBookCatalogByIdAsync(model, cancellationToken);
        public async Task<CommonResultEntity> SaveBookAuthorMappingAsync(BookAuthorMapping model, CancellationToken cancellationToken = default)=> await bookSqlRepository.SaveBookAuthorMappingAsync(model, cancellationToken);
        public async Task<CommonResultEntity> DeleteBookAuthorMappingAsync(BookAuthorMapping model, CancellationToken cancellationToken = default)=> await bookSqlRepository.DeleteBookAuthorMappingAsync(model, cancellationToken);
    }
}
