namespace HC.DomainModels.Entities
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