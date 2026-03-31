namespace AnimalAg.CodingChallenge.Data.Entities.Books
{
    public class BookRequestEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Title";
        public string SortDirection { get; set; } = "ASC";
    }
}
