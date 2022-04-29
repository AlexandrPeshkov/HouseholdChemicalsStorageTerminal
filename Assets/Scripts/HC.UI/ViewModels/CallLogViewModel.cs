using System;

namespace HC.UI.ViewModels
{
    public class CallLogViewModel
    {
        public DateTime Date { get; set; }

        public string UserToNumber { get; set; }

        public string CityFrom { get; set; }

        public string CityTo { get; set; }

        public float Cost { get; set; }

        public bool Status { get; set; }
    }
}