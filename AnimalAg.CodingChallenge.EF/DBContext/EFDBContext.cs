using AnimalAg.CodingChallenge.Data.Entities.Authors;
using AnimalAg.CodingChallenge.Data.Entities.Books;
using AnimalAg.CodingChallenge.Data.Entities.Stores;
using Microsoft.EntityFrameworkCore;

namespace AnimalAg.CodingChallenge.EF.DBContext
{
    /// <summary>
    /// Represents the Entity Framework database context for accessing and managing store data.
    /// </summary>
    /// <remarks>This context is configured for use with Entity Framework Core and provides access to the
    /// Stores entity set. It should be registered and managed according to the application's dependency injection and
    /// lifetime requirements.</remarks>
    public class EFDBContext : DbContext
    {
        /// <summary>
        /// Gets or sets the collection of stores in the database context.
        /// </summary>
        public DbSet<Store> Stores { get; set; }
        /// <summary>
        /// Gets or sets the collection of books in the database context.
        /// </summary>
        public DbSet<Book> Books { get; set; }
        /// <summary>
        /// Gets or sets the collection of Authors in the database context.
        /// </summary>
        public DbSet<Author> Authors { get; set; }
        /// <summary>
        /// Gets or sets the collection of StoreBookMapping in the database context.
        /// </summary>
        public DbSet<StoreBookMapping> StoreBookMappings { get; set; }
        /// <summary>
        /// Gets or sets the collection of BookAuthorMapping in the database context.
        /// </summary>
        public DbSet<BookAuthorMapping> BookAuthorMappings { get; set; }
        /// <summary>
        /// Initializes a new instance of the DBContext class using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext. Must not be null.</param>
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options)
        {
        }
    }
}
