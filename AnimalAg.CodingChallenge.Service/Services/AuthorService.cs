using AnimalAg.CodingChallenge.Data.Entities.Authors;
using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.EF.SqlRepositoryInterfaces;
using AnimalAg.CodingChallenge.Service.Interfaces;

namespace AnimalAg.CodingChallenge.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorSqlRepository authorSqlRepository;
        public AuthorService(IAuthorSqlRepository authorSqlRepository)
        {
            this.authorSqlRepository = authorSqlRepository;
        }
        public async Task<CommonResultEntity> SaveAuthorAsync(Author model, CancellationToken cancellationToken = default)=> await authorSqlRepository.SaveAuthorAsync(model, cancellationToken);
        public async Task<IEnumerable<AllAuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken = default)=> await authorSqlRepository.GetAllAuthorsAsync(cancellationToken);
        public async Task<IEnumerable<AuthorDto>> GetAuthorCatalogByIdAsync(Author model, CancellationToken cancellationToken = default)=> await authorSqlRepository.GetAuthorCatalogByIdAsync(model, cancellationToken);
    }
}
