using System.Collections.Generic;
using System.Threading.Tasks;
using HC.UI.ViewModels;

namespace HC.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IReadOnlyCollection<InvoiceViewModel>> GetAllInvoices();

        Task Pay(int invoiceId);
    }
}