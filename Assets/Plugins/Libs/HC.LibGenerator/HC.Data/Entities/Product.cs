using System.ComponentModel.DataAnnotations;

namespace HC.Data
{
    public class Product : BaseEntity
    {
        public int StorageId { get; set; }

        public Storage Storage { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}