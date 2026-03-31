namespace AnimalAg.CodingChallenge.Data.Entities.Stores
{
    public class StoreRequestEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
    }
}
