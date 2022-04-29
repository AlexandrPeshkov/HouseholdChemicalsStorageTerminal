using System;
using HC.DataAccess.Models;

namespace DefaultNamespace
{
    public class CallLog : BaseDbEntity
    {
        public int UserIdFrom { get; set; }

        public int UserIdTo { get; set; }

        public decimal Duration { get; set; }

        public DateTime Date { get; set; }
    }
}