using HC.DataAccess.Models;

namespace HC.Domain.Entities
{
    /// <summary>
    /// Поставщик товара
    /// </summary>
    public class Supplier : BaseDbEntity
    {
        public string Name { get; set; }

        public string City { get; set; }
    }
}