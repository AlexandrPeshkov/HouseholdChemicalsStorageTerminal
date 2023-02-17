using DataAccess.Models;

namespace DataAccess
{
    public class City : BaseDbEntity
    {
        public int Code { get; set; }

        public string Name { get; set; }
        
        public int CountryId { get; set; }
    }
}