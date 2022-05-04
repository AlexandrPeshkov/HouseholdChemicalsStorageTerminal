using HC.DataAccess.Models;

namespace HC.DataAccess
{
    public class User : BaseDbEntity
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public int CityId { get; set; }
    }
}