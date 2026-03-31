using AnimalAg.CodingChallenge.Data.Entities.Authors;
using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.EF.DBContext;
using AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalAg.CodingChallenge.EF.SqlRepository
{
    public class AuthorSqlRepository : IAuthorSqlRepository
    {
        /// <summary>
        /// EFDBContext instance used for accessing the database. This field is typically initialized through dependency injection and should be used to perform database operations related to authors.
        /// </summary>
        private readonly EFDBContext dbContext;
        /// <summary>
        /// Initializes a new instance of the AuthorSqlRepository class using the specified database context.
        /// </summary>
        /// <param name="_dbContext">The EFDBContext instance to be used for database operations. Cannot be null.</param>
        public AuthorSqlRepository(EFDBContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<CommonResultEntity> SaveAuthorAsync(Author model, CancellationToken cancellationToken = default)
        {
            var validateResult = ValidateAuthorDetail(model);
            if (!validateResult.IsSuccess)
                return validateResult;

            bool authorExists = await dbContext.Authors
                .AnyAsync(s => s.FirstName == model.FirstName && s.LastName == model.LastName, cancellationToken);

            if (authorExists)
            {
                return new CommonResultEntity
                {
                    IsSuccess = false,
                    ErrorCode = "Error_Book_006",
                    ErrorMessage = "Author with the same name already exists."
                };
            }

            await dbContext.Authors.AddAsync(model, cancellationToken);

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
        /// Valid Author detail common method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CommonResultEntity ValidateAuthorDetail(Author model)
        {
            if (model == null)
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_001", ErrorMessage = "Please provide Book detail" };

            if (string.IsNullOrEmpty(Convert.ToString(model.Id)))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_002", ErrorMessage = "Please provide Book Id" };

            if (string.IsNullOrEmpty(model.FirstName))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_003", ErrorMessage = "Please provide Book first name" };

            if (string.IsNullOrEmpty(model.LastName))
                return new CommonResultEntity() { IsSuccess = false, ErrorCode = "Error_Book_004", ErrorMessage = "Please provide Book last name" };

            return new CommonResultEntity() { IsSuccess = true }; //all data is fine
        }

        public async Task<IEnumerable<AllAuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Authors
                        .Where(s => s.IsActive) // filter active authors
                        .Select(s => new AllAuthorDto()
                        {
                            AuthorId = s.Id,
                            FirstName = s.FirstName,
                            LastName = s.FirstName
                        })
                        .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthorCatalogByIdAsync(Author model,CancellationToken cancellationToken = default)
        {
            return await dbContext.Authors
                        .Where(a => a.IsActive) // only active authors
                        .Include(a => a.BookAuthors) // include mapping
                            .ThenInclude(bam => bam.Book)   // include book details
                        .Select(a => new AuthorDto
                        {
                            AuthorId = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Books = a.BookAuthors
                                .Where(bam => bam.IsActive && bam.AuthorId == a.Id && bam.Book.IsActive && bam.Book.Id == bam.BookId)
                                .Select(bam => new AuthorBookDto
                                {
                                    BookId = bam.Book.Id,
                                    BookName = bam.Book.Name,
                                    ISBN = bam.Book.ISBN,
                                    Price = bam.Book.Price
                                })
                                .ToList()
                        })
                        .ToListAsync(cancellationToken);
        }
    }
}
