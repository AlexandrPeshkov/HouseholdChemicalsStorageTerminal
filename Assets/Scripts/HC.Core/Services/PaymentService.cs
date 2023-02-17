using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Logic;
using DataAccess;
using Interfaces.Services;
using UI.ViewModels;
using UnityEngine;

namespace Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly EntityRepository _entityRepository;

        public PaymentService(EntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task Pay(int invoiceId)
        {
            var invoiceModel = await _entityRepository.Invoices.Get(invoiceId);
            invoiceModel.IsPaid = true;
            await _entityRepository.Invoices.Update(invoiceModel);
        }

        public async Task<IReadOnlyCollection<InvoiceViewModel>> GetAllInvoices()
        {
            IReadOnlyCollection<Invoice> invoices = await _entityRepository.Invoices.All();
            IReadOnlyCollection<User> users = await _entityRepository.Users.All();

            var invoiceViewModels = new List<InvoiceViewModel>(invoices.Count);

            foreach (Invoice invoice in invoices)
            {
                CallLog callLog = await _entityRepository.CallLogs.Get(invoice.CallLogId);

                ProviderAccount userFromAccount = await _entityRepository.ProviderAccounts.Get(callLog.ProviderAccountIdFrom);
                ProviderAccount userToAccount = await _entityRepository.ProviderAccounts.Get(callLog.ProviderAccountIdTo);

                Provider providerFrom = await _entityRepository.Providers.Get(userFromAccount.ProviderId);
                
                District district = await _entityRepository.Districts.Get(callLog.DistrictId);

                User userFrom = users.FirstOrDefault(x => x.ProviderAccountId == userFromAccount.ProviderId);
                User userTo = users.FirstOrDefault(x => x.ProviderAccountId == userToAccount.ProviderId);
                
                float cost = await CalcCost(callLog.Duration, userFromAccount, userToAccount);

                var invoiceView = new InvoiceViewModel()
                {
                    InvoiceId = invoice.Id,
                    UserFrom = userFrom.Name,
                    UserFromNumber = userFromAccount.Number,
                    UserTo = userTo.Name,
                    UserToNumber = userToAccount.Number,
                    Date = callLog.Date,
                    DistrictName = district.Name,
                    Status = invoice.IsPaid,
                    Cost = cost,
                    ProviderFromName = providerFrom.Name
                };
                invoiceViewModels.Add(invoiceView);
            }

            return invoiceViewModels;
        }

        private async Task<float> CalcCost(float seconds, ProviderAccount from, ProviderAccount to)
        {
            var rates = await _entityRepository.ProviderRates.All();

            var rateEntity = rates
                .FirstOrDefault(x => 
                    x.ProviderId == from.ProviderId 
                    && x.CallingAccountTypeId == to.AccountTypeId);

            var rate = rateEntity?.Rate ?? 0f;

            return rate;
        }
    }
}