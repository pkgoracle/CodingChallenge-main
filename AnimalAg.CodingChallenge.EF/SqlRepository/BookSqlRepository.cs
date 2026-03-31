using AnimalAg.CodingChallenge.Data.Entities.Authors;
using AnimalAg.CodingChallenge.Data.Entities.Books;
using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;
using AnimalAg.CodingChallenge.EF.DBContext;
using AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalAg.CodingChallenge.EF.SqlRepository
{
    public class BookSqlRepository : IBookSqlRepository
    {
        /// <summary>
        /// EFDBContext instance used for accessing the database. This field is typically initialized through dependency injection and should be used to perform database operations related to books.
        /// </summary>
        private readonly EFDBContext dbContext;
        /// <summary>
        /// Initializes a new instance of the bookSqlRepository class using the specified database context.
        /// </summary>
        /// <param name="_dbContext">The EFDBContext instance to be used for database operations. Cannot be null.</param>
        public BookSqlRepository(EFDBContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<CommonResultEntity> SaveBookAsync(Book model, CancellationToken cancellationToken = default)
        {
            var validateResult = ValidateBookDetail(model);
            if (!validateResult.IsSuccess)
                return validateResult;

            // Use AnyAsync for existence matters
            bool bookExists = await dbContext.Books
                .AnyAsync(s => s.Name == model.Name, cancellationToken);

            if (bookExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Book_006",
                    ErrorMessage = "Book with the same name already exists."
                };
            }

            // Use AnyAsync for existence matters
            bookExists = await dbContext.Books
                .AnyAsync(s => s.ISBN == model.ISBN, cancellationToken);

            if (bookExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Book_006",
                    ErrorMessage = "Book with the ISBN name already exists."
                };
            }

            await dbContext.Books.AddAsync(model, cancellationToken);

            //async save
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CommonResultEntity
            {
                IsSuccess = true,
                ErrorCode = "Error_Book_100",
                ErrorMessage = "Record saved successfully",
                Result = model.Id
            };
        }

        public async Task<CommonResultEntity> UpdateBookAsync(Book model, CancellationToken cancellationToken = default)
        {
            var validateResult = ValidateBookDetail(model);
            if (!validateResult.IsSuccess)
                return validateResult;

            // Use AnyAsync for existence matters
            bool bookExists = await dbContext.Books
                .AnyAsync(s => s.Name == model.Name && s.Id != model.Id, cancellationToken);

            if (bookExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Book_006",
                    ErrorMessage = "Book with the same name already exists."
                };
            }

            // Use AnyAsync for existence matters
            bookExists = await dbContext.Books
                .AnyAsync(s => s.ISBN == model.ISBN && s.Id != model.Id, cancellationToken);

            if (bookExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Book_007",
                    ErrorMessage = "Book with the ISBN name already exists."
                };
            }

            await dbContext.Books
                 .Where(b => b.Id == model.Id) // filter the book you want to update
                 .ExecuteUpdateAsync(
                     setters => setters
                         .SetProperty(b => b.Name, model.Name)
                         .SetProperty(b => b.ISBN, model.ISBN)
                         .SetProperty(b => b.Price, model.Price),
                     cancellationToken);

            //async save
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CommonResultEntity
            {
                IsSuccess = true,
                ErrorCode = "Error_Book_100",
                ErrorMessage = "Record saved successfully",
                Result = model.Id
            };
        }

        /// <summary>
        /// Valid Book detail common method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CommonResultEntity ValidateBookDetail(Book model)
        {
            if (model == null)
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_001", ErrorMessage = "Please provide Book detail" };

            if (string.IsNullOrEmpty(Convert.ToString(model.Id)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_002", ErrorMessage = "Please provide Book Id" };

            if (string.IsNullOrEmpty(model.Name))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_003", ErrorMessage = "Please provide Book name" };

            if (string.IsNullOrEmpty(model.ISBN))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_004", ErrorMessage = "Please provide Book ISBN" };

            if (model.Price <= 0 || model.Price > 10000)
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_005", ErrorMessage = "Please provide Book ISBN" };

            return new CommonResultEntity() { IsSuccess = true }; //all data is fine
        }

        public async Task<PagedResult<BookDto>> GetAllBooksWithFilterAsync(BookRequestEntity model, CancellationToken cancellationToken = default)
        {
            var query = dbContext.Books
                .Where(b => b.IsActive);

            // Filter by title
            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(b => b.Name.Contains(model.Title));
            }

            // Filter by author
            if (!string.IsNullOrEmpty(model.Author))
            {
                query = query.Where(b => b.BookAuthors
                    .Any(bam => (bam.Author.FirstName + " " + bam.Author.LastName)
                        .Contains(model.Author)));
            }

            // Sorting (safe defaults)
            var sortBy = string.IsNullOrEmpty(model.SortBy) ? "Id" : model.SortBy;
            var sortDirection = string.IsNullOrEmpty(model.SortDirection) ? "ASC" : model.SortDirection.ToUpper();

            query = sortBy switch
            {
                "Title" => sortDirection == "DESC"
                    ? query.OrderByDescending(b => b.Name)
                    : query.OrderBy(b => b.Name),

                "Price" => sortDirection == "DESC"
                    ? query.OrderByDescending(b => b.Price)
                    : query.OrderBy(b => b.Price),

                _ => sortDirection == "DESC"
                    ? query.OrderByDescending(b => b.Id)
                    : query.OrderBy(b => b.Id)
            };

            // Total count for paging
            var totalCount = await query.CountAsync(cancellationToken);

            // Paging + projection
            var books = await query
                .Skip((model.PageNumber - 1) * model.PageSize)
                .Take(model.PageSize)
                .Select(b => new BookDto
                {
                    BookId = b.Id,
                    BookName = b.Name,
                    ISBN = b.ISBN,
                    Price = b.Price,
                    Authors = b.BookAuthors
                        .Select(bam => new AuthorDto() { FirstName = bam.Author.FirstName, LastName = bam.Author.LastName })
                        .ToList()
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<BookDto>
            {
                Items = books,
                TotalCount = totalCount,
                PageNumber = model.PageNumber,
                PageSize = model.PageSize
            };
        }

        public async Task<BookCatalogDto?> GetBookCatalogByIdAsync(BookRequestEntity model, CancellationToken cancellationToken = default)
        {
            return await dbContext.Books
                        .Where(b => b.IsActive && b.Id == model.Id)
                        .Include(b => b.BookAuthors)
                            .ThenInclude(ba => ba.Author)
                        .Include(b => b.StoreBooks)
                            .ThenInclude(sb => sb.Store)
                        .Select(b => new BookCatalogDto
                        {
                            BookId = b.Id,
                            BookName = b.Name,
                            ISBN = b.ISBN,
                            Price = b.Price,

                            Authors = b.BookAuthors
                                .Where(ba => ba.IsActive && ba.BookId == b.Id && ba.Author.IsActive && ba.Author.Id == ba.AuthorId)
                                .Select(ba => new BookAuthorDto
                                {
                                    AuthorId = ba.Author.Id,
                                    FirstName = ba.Author.FirstName,
                                    LastName = ba.Author.LastName
                                })
                                .ToList(),

                            Stores = b.StoreBooks
                                .Where(sb => sb.Store.IsActive)
                                .Select(sb => new BookStoreDto
                                {
                                    StoreId = sb.Store.Id,
                                    StoreName = sb.Store.Name,
                                    StoreLocation = sb.Store.Location,
                                    StoreBookPrice = sb.StoreBookPrice,
                                    Inventory = sb.Inventory
                                })
                                .ToList()
                        })
                        .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Validate Book Author Detail Common Method
        /// </summary>
        /// <param name="model"></param>
        /// <param name="IsSave"></param>
        /// <returns></returns>
        private CommonResultEntity ValidateBookAuthorDetail(BookAuthorMapping model, bool IsSave = false)
        {
            if (model == null)
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_008", ErrorMessage = "Please provide book author detail" };

            if (IsSave && string.IsNullOrEmpty(Convert.ToString(model.Id)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_009", ErrorMessage = "Please provide book author Id" };

            if (string.IsNullOrEmpty(Convert.ToString(model.AuthorId)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_010", ErrorMessage = "Please provide author Id" };

            if (string.IsNullOrEmpty(Convert.ToString(model.BookId)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_011", ErrorMessage = "Please provide book Id" };

            return new CommonResultEntity() { IsSuccess = true }; //all data is fine
        }

        /// <summary>
        /// Save Book Author Mapping
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonResultEntity> SaveBookAuthorMappingAsync(BookAuthorMapping model, CancellationToken cancellationToken = default)
        {
            var validateResult = ValidateBookAuthorDetail(model, true);
            if (!validateResult.IsSuccess)
                return validateResult;

            // Check if author exists and is active
            bool storeExists = await dbContext.Authors
                .AnyAsync(s => s.IsActive && s.Id == model.AuthorId, cancellationToken);

            if (!storeExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Book_012",
                    ErrorMessage = "Please provide a valid author Id"
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
                    ErrorCode = "Error_Book_013",
                    ErrorMessage = "Please provide a valid book Id"
                };
            }

            await dbContext.BookAuthorMappings.AddAsync(model, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CommonResultEntity
            {
                IsSuccess = true,
                ErrorCode = "Error_Book_100",
                ErrorMessage = "Record saved successfully",
                Result = model.Id
            };
        }


        /// <summary>
        /// Delete Book Author Mapping by updating IsActive flag
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommonResultEntity> DeleteBookAuthorMappingAsync(BookAuthorMapping model, CancellationToken cancellationToken = default)
        {
            var validateResult = ValidateBookAuthorDetail(model);
            if (!validateResult.IsSuccess)
                return validateResult;

            // Find the mapping
            var storeBookMapping = await dbContext.BookAuthorMappings
                .FirstOrDefaultAsync(s => s.IsActive && s.AuthorId == model.AuthorId && s.BookId == model.BookId, cancellationToken);

            if (storeBookMapping == null)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Store_012",
                    ErrorMessage = "Book Author mapping doesn't exist"
                };
            }

            await dbContext.BookAuthorMappings
                 .Where(b => b.Id == storeBookMapping.Id) // filter the store you want to update
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
                ErrorCode = "Error_Book_101",
                ErrorMessage = "Record deleted successfully",
                Result = model.Id
            };
        }
    }
}
