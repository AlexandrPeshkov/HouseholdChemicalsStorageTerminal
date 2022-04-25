using HC.DomainModels.Interfaces;

namespace HC.DomainModels.Entities
{
    public class BaseDbEntity : IDbEntity
    {
        public int Id { get; set; }
    }
}