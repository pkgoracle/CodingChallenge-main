using AnimalAg.CodingChallenge.Data.Entities.Books;
using AnimalAg.CodingChallenge.Data.Entities.Common;

namespace AnimalAg.CodingChallenge.Data.Entities.Stores
{
    /// <summary>
    /// Entity class Book Store Mapping many -to-many relationship between Store and Book. It contains properties such as Id, StoreId, BookId, IsActive, CreatedBy, CreatedDate, ModifiedBy, and ModifiedDate. The Id is a unique identifier for each mapping, generated as a new Guid when a new StoreBookMapping is created. The StoreId and BookId properties are foreign keys to the Store and Book entities, respectively, representing the identifiers of the store and book associated with the mapping. The IsActive property indicates whether the mapping is active or not, with a default value of true. The CreatedBy and ModifiedBy properties Book the identifiers of the users who created and last modified the mapping entity, respectively. The CreatedDate and ModifiedDate properties Book the date and time when the mapping entity was created and last modified, respectively.
    /// </summary>
    public class StoreBookMapping : CommonEntity
    {
        /// <summary>
        /// Store ID, foreign key to the Store entity. It represents the identifier of the store associated with the book in the mapping.
        /// </summary>
        public Guid StoreId { get; set; }
        /// <summary>
        /// Book ID, foreign key to the Book entity. It represents the identifier of the book associated with the store in the mapping.
        /// </summary>
        public Guid BookId { get; set; }

        public decimal StoreBookPrice { get; set; }

        public int Inventory { get; set; }
        // Navigation
        public Store Store { get; set; }
        public Book Book { get; set; }
    }
}
