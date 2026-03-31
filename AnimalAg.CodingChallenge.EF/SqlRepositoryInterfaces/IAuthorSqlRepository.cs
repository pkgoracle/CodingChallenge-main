using AnimalAg.CodingChallenge.Data.Entities.Authors;
using AnimalAg.CodingChallenge.Data.Entities.Common;

namespace AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces
{
    public interface IAuthorSqlRepository
    {
        public Task<CommonResultEntity> SaveAuthorAsync(Author model, CancellationToken cancellationToken = default);
        public Task<IEnumerable<AllAuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<AuthorDto>> GetAuthorCatalogByIdAsync(Author model, CancellationToken cancellationToken = default);
    }
}
