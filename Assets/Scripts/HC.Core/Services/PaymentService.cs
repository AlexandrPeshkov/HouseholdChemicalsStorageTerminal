using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HC.DataAccess;
using HC.Interfaces.Services;
using HC.UI.ViewModels;
using UnityEngine;

namespace HC.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IEntityRepository _entityRepository;

        public PaymentService(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task Pay(int invoiceId)
        {
            //TODO: Pay
        }

        public async Task<IReadOnlyCollection<InvoiceViewModel>> GetAllInvoices()
        {
            var invoices = await _entityRepository.Invoices.All();

            var invoiceViewModels = new List<InvoiceViewModel>(invoices.Count);

            foreach (var invoice in invoices)
            {
                var callLog = await _entityRepository.CallLogs.Get(invoice.CallLogId);
                var userFrom = await _entityRepository.Users.Get(callLog.UserIdFrom);
                var userTo = await _entityRepository.Users.Get(callLog.UserIdTo);
                var cityFrom = await _entityRepository.Cities.Get(userFrom.CityId);
                var cityTo = await _entityRepository.Cities.Get(userTo.CityId);

                var cost = await CalcCost(callLog.Duration, cityFrom.Id, cityTo.Id);

                var invoiceView = new InvoiceViewModel()
                {
                    InvoiceId = invoice.Id,
                    UserFrom = userFrom.Name,
                    UserFromNumber = userFrom.Number,
                    UserTo = userTo.Name,
                    UserToNumber = userTo.Number,
                    Date = callLog.Date,
                    CityFrom = cityFrom.Name,
                    CityTo = cityTo.Name,
                    Status = invoice.IsPaid,
                    Cost = cost
                };
                invoiceViewModels.Add(invoiceView);
            }

            return invoiceViewModels;
        }

        private async Task<float> CalcCost(float seconds, int cityFrom, int cityTo)
        {
            var argParam = Expression.Parameter(typeof(Rate), "s");
            Expression fromProp = Expression.Property(argParam, nameof(Rate.CityIdFrom));
            Expression toProp = Expression.Property(argParam, nameof(Rate.CityIdTo));

            var fromVal = Expression.Constant(cityFrom);
            var toVal = Expression.Constant(cityTo);

            Expression e1 = Expression.Equal(fromProp, fromVal);
            Expression e2 = Expression.Equal(toProp, toVal);
            var andExp = Expression.AndAlso(e1, e2);

            var lambda = Expression.Lambda<Func<Rate, bool>>(andExp, argParam);

            var rate = await _entityRepository.Rates.FirstOrDefault(lambda);

            if (rate == null)
            {
                Debug.LogError($"Null rate {cityFrom} {cityTo}");
                return 0f;
            }

            var cost = rate.CostPerMinute * (seconds / 60f);

            return cost;
        }
    }
}