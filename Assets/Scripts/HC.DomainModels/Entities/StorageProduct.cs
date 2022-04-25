namespace HC.DomainModels.Entities
{
    /// <summary>
    /// Запись о хранимом товаре
    /// </summary>
    public class StorageProduct : BaseDbEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public uint Count { get; set; }

        public double Price { get; set; }
    }
}