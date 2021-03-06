using System;

namespace HC.UI.ViewModels
{
    public class InvoiceViewModel
    {
        public DateTime Date { get; set; }

        public string UserToNumber { get; set; }

        public string UserFromNumber { get; set; }

        public string UserTo { get; set; }

        public string UserFrom { get; set; }

        public string CityFrom { get; set; }

        public string CityTo { get; set; }

        public float Cost { get; set; }

        public bool Status { get; set; }
        
        public int InvoiceId { get; set; }
    }
}