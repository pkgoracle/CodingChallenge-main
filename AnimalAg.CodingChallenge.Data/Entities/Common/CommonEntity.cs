using System.ComponentModel.DataAnnotations;

namespace AnimalAg.CodingChallenge.Data.Entities.Common
{
    /// <summary>
    /// This is common entity to define common properties for all entities, such as Id, IsActive, CreatedBy, CreatedDate, ModifiedBy, and ModifiedDate. The Id is a unique identifier for each entity, generated as a new Guid when a new entity is created. The IsActive property indicates whether the entity is active or not, with a default value of true. The CreatedBy and ModifiedBy properties Book the identifiers of the users who created and last modified the entity, respectively. The CreatedDate and ModifiedDate properties Book the date and time when the entity was created and last modified, respectively.
    /// </summary>
    public abstract class CommonEntity
    {
        /// <summary>
        /// Mapping ID primary key, generated as a new Guid
        /// </summary>
        [MaxLength(100)]
        public Guid Id { get; set; } = new Guid();
        /// <summary>
        /// Staus for the current record, default value is true, which means the record is active. If set to false, it indicates that the record is inactive and should not be considered in active operations.
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        [MaxLength(100)]
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Gets or sets the identifier of the user who last modified the entity.
        /// </summary>
        [MaxLength(100)]
        public string? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
