using HC.DataAccess.Models;

namespace DefaultNamespace
{
    public class City : BaseDbEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}