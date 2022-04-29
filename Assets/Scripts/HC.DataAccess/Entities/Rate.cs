using HC.DataAccess.Models;

namespace DefaultNamespace
{
    public class Rate : BaseDbEntity
    {
        public string CityCodeFrom { get; set; }

        public string CityCodeTo { get; set; }

        public float CostPerMinute { get; set; }
    }
}