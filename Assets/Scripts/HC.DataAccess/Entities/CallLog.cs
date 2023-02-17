using System;
using DataAccess.Models;

namespace DataAccess
{
    public class CallLog : BaseDbEntity
    {
        public int ProviderAccountIdFrom { get; set; }

        public int ProviderAccountIdTo { get; set; }

        public float Duration { get; set; }

        public DateTime Date { get; set; }

        public int DistrictId { get; set; }
    }
}