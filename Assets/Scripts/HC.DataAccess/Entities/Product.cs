using HC.DataAccess.Models;

namespace HC.Domain.Entities
{
    /// <summary>
    /// Сущность товара
    /// </summary>
    public class Product : BaseDbEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}