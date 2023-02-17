using DataAccess.Interfaces;

namespace DataAccess.Models
{
    public class BaseDbEntity : IDbEntity
    {
        public int Id { get; set; }
    }
}