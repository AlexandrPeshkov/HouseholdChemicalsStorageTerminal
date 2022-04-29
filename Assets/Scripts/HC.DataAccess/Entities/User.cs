using HC.DataAccess.Models;

namespace DefaultNamespace
{
    public class User : BaseDbEntity
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public int CityId { get; set; }
    }
}