using AnimalAg.CodingChallenge.Data.Entities.Authors;
using AnimalAg.CodingChallenge.Data.Entities.Common;

namespace AnimalAg.CodingChallenge.Data.Entities.Books
{
    /// <summary>
    /// Book Author Mapping many -to-many relationship between Book and Author. It contains properties such as Id, BookId, AuthorId, IsActive, CreatedBy, CreatedDate, ModifiedBy, and ModifiedDate. The Id is a unique identifier for each mapping, generated as a new Guid when a new BookAuthorMapping is created. The BookId and AuthorId properties are foreign keys to the Book and Author entities, respectively, representing the identifiers of the book and author associated with the mapping. The IsActive property indicates whether the mapping is active or not, with a default value of true. The CreatedBy and ModifiedBy properties Book the identifiers of the users who created and last modified the mapping entity, respectively. The CreatedDate and ModifiedDate properties Book the date and time when the mapping entity was created and last modified, respectively.
    /// </summary>
    public class BookAuthorMapping : CommonEntity
    {        
        /// <summary>
        /// Book ID, foreign key to the Book entity. It represents the identifier of the Book associated with the Author in the mapping.
        /// </summary>
        public Guid BookId { get; set; }
        /// <summary>
        /// Author ID, foreign key to the Author entity. It represents the identifier of the Author associated with the Book in the mapping.
        /// </summary>
        public Guid AuthorId { get; set; }
        // Navigation
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
