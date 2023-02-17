using DataAccess.Models;

namespace DataAccess
{
    public class District : BaseDbEntity
    {
        public string Name { get; set; }
        
        public int CityId { get; set; }
    }
}