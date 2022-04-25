namespace HC.DomainModels.Entities
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