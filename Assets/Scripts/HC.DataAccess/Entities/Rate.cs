using HC.DataAccess.Models;

namespace HC.DataAccess
{
    public class Rate : BaseDbEntity
    {
        public int CityIdFrom { get; set; }

        public int CityIdTo { get; set; }

        public float CostPerMinute { get; set; }
    }
}