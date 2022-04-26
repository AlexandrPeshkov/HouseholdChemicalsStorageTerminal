using HC.DataAccess.Models;

namespace HC.Domain.Entities
{
    /// <summary>
    /// Запись о хранимом товаре
    /// </summary>
    public class StorageProduct : BaseDbEntity
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public uint Count { get; set; }

        public double Price { get; set; }
    }
}