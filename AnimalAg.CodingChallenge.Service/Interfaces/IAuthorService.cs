using AnimalAg.CodingChallenge.Data.Entities.Authors;
using AnimalAg.CodingChallenge.Data.Entities.Common;

namespace AnimalAg.CodingChallenge.Service.Interfaces
{
    /// <summary>
    /// Defines the contract for services that provide operations related to authors.
    /// </summary>
    public interface IAuthorService
    {
        public Task<CommonResultEntity> SaveAuthorAsync(Author model, CancellationToken cancellationToken = default);
        public Task<IEnumerable<AllAuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<AuthorDto>> GetAuthorCatalogByIdAsync(Author model, CancellationToken cancellationToken = default);
    }
}
