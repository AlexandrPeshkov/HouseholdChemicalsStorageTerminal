using DataAccess.Models;

namespace DataAccess
{
    public class User : BaseDbEntity
    {
        public string Name { get; set; }

        public int CityId { get; set; }
        
        public int ProviderAccountId { get; set; }
    }
}