namespace AnimalAg.CodingChallenge.Data.Entities.Authors
{

    public class AllAuthorDto
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class AuthorDto
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AuthorBookDto> Books { get; set; }
    }

    public class AuthorBookDto
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
    }
}
