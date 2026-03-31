namespace AnimalAg.CodingChallenge.Data.Entities.Books
{
    public class BookCatalogDto
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public List<BookAuthorDto> Authors { get; set; }
        public List<BookStoreDto> Stores { get; set; }
    }

    public class BookAuthorDto
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class BookStoreDto
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreLocation { get; set; }
        public decimal StoreBookPrice { get; set; }
        public int Inventory { get; set; }
    }
}
