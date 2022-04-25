using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HC.Data
{
    public class Storage : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}