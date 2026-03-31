using AnimalAg.CodingChallenge.Data.Entities.Authors;

namespace AnimalAg.CodingChallenge.Data.Entities.Stores
{
    public class StoreDto
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreLocation { get; set; }
    }

    public class StoreCatalogDto
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreLocation { get; set; }

        public List<BookDto> Books { get; set; } = new();
    }

    public class BookDto
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public decimal StoreBookPrice { get; set; }
        public int Inventory { get; set; }
        public List<AuthorDto> Authors { get; set; }
    }
}
