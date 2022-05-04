using System;
using HC.DataAccess.Models;

namespace HC.DataAccess
{
    public class CallLog : BaseDbEntity
    {
        public int UserIdFrom { get; set; }

        public int UserIdTo { get; set; }

        public float Duration { get; set; }

        public DateTime Date { get; set; }
    }
}