using AnimalAg.CodingChallenge.Data.Entities.Common;

namespace AnimalAg.CodingChallenge.Data.Entities.Stores
{
    /// <summary>
    /// Entity Class For Book Store
    /// </summary>
    public class Store : CommonEntity
    {
        ///// <summary>
        ///// Store Name, required and should not be null or empty. It represents the name of the store.
        ///// </summary>
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Store Name.")]
        public string Name { get; set; }
        public string Location { get; set; }
        // Navigation
        public ICollection<StoreBookMapping> StoreBooks { get; set; } = new List<StoreBookMapping>();
    }
}
