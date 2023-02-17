using DataAccess.Models;

namespace DataAccess
{
    public class Country : BaseDbEntity
    {
        public string Code { get; set; }
        
        public string Name { get; set; }
    }
}