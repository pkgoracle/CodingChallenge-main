using AnimalAg.CodingChallenge.Data.Entities.Books;
using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;

namespace AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces
{
    public interface IBookSqlRepository
    {
        public Task<CommonResultEntity> SaveBookAsync(Book model, CancellationToken cancellationToken = default);
        public Task<CommonResultEntity> UpdateBookAsync(Book model, CancellationToken cancellationToken = default);
        public Task<PagedResult<BookDto>> GetAllBooksWithFilterAsync(BookRequestEntity model, CancellationToken cancellationToken = default);
        public Task<BookCatalogDto?> GetBookCatalogByIdAsync(BookRequestEntity model, CancellationToken cancellationToken = default);
        public Task<CommonResultEntity> SaveBookAuthorMappingAsync(BookAuthorMapping model, CancellationToken cancellationToken = default);
        public Task<CommonResultEntity> DeleteBookAuthorMappingAsync(BookAuthorMapping model, CancellationToken cancellationToken = default);
    }
}
