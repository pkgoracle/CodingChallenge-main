using AnimalAg.CodingChallenge.Data.Entities.Books;
using AnimalAg.CodingChallenge.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AnimalAg.CodingChallenge.Data.Entities.Authors
{
    /// <summary>
    /// Author Author data entity class, which represents the author information in the system. It contains properties such as Id, Name, IsActive, CreatedBy, CreatedDate, ModifiedBy, and ModifiedDate. The Id is a unique identifier for each author, generated as a new Guid when a new Author is created. The Name property is required and should not be null or empty, representing the name of the author. The IsActive property indicates whether the author is active or not, with a default value of true. The CreatedBy and ModifiedBy properties Author the identifiers of the users who created and last modified the author entity, respectively. The CreatedDate and ModifiedDate properties Author the date and time when the author entity was created and last modified, respectively.
    /// </summary>
    public class Author : CommonEntity
    {
        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }
        // Navigation
        public ICollection<BookAuthorMapping> BookAuthors { get; set; } = new List<BookAuthorMapping>();
    }
}
