using HC.DataAccess.Models;

namespace HC.DataAccess
{
    public class City : BaseDbEntity
    {
        public int Code { get; set; }

        public string Name { get; set; }
    }
}