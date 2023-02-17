using System.Collections.Generic;
using System.Threading.Tasks;
using UI.ViewModels;

namespace Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IReadOnlyCollection<InvoiceViewModel>> GetAllInvoices();

        Task Pay(int invoiceId);
    }
}