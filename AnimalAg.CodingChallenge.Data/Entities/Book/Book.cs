using AnimalAg.CodingChallenge.Data.Entities.Common;
using AnimalAg.CodingChallenge.Data.Entities.Stores;
using System.ComponentModel.DataAnnotations;

namespace AnimalAg.CodingChallenge.Data.Entities.Books
{
    /// <summary>
    /// Book data entity class, which represents the book information in the system. It contains properties such as Id, Name, IsActive, CreatedBy, CreatedDate, ModifiedBy, and ModifiedDate. The Id is a unique identifier for each book, generated as a new Guid when a new Book is created. The Name property is required and should not be null or empty, representing the name of the book. The IsActive property indicates whether the book is active or not, with a default value of true. The CreatedBy and ModifiedBy properties Book the identifiers of the users who created and last modified the book entity, respectively. The CreatedDate and ModifiedDate properties Book the date and time when the book entity was created and last modified, respectively.
    /// </summary>
    public class Book : CommonEntity
    {
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        // Navigation
        public ICollection<BookAuthorMapping> BookAuthors { get; set; } = new List<BookAuthorMapping>();
        public ICollection<StoreBookMapping> StoreBooks { get; set; } = new List<StoreBookMapping>();
    }
}
