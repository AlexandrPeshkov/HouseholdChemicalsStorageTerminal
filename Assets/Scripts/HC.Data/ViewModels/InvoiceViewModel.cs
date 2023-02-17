using System;

namespace UI.ViewModels
{
    public class InvoiceViewModel
    {
        public DateTime Date { get; set; }

        public string UserToNumber { get; set; }

        public string UserFromNumber { get; set; }

        public string UserTo { get; set; }

        public string UserFrom { get; set; }
        
        public float Cost { get; set; }

        public bool Status { get; set; }
        
        public int InvoiceId { get; set; }
        
        public string DistrictName { get; set; }
        
        public string ProviderFromName { get; set; }
    }
}