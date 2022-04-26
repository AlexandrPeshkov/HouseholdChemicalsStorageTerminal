using HC.DataAccess.Interfaces;

namespace HC.DataAccess.Models
{
    public class BaseDbEntity : IDbEntity
    {
        public int Id { get; set; }
    }
}