using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.UI.Services;
using Cysharp.Threading.Tasks;
using HC.Interfaces.Services;
using HC.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HC.UI.Elements
{
    public class UserCallLogsTable : MonoBehaviour, IAsyncInitializable
    {
        [Inject]
        private readonly IInstantiator _diContainer;

        [Inject]
        private readonly BusyIndicator _busyIndicator;

        [Inject]
        private readonly IPaymentService _paymentService;

        [SerializeField]
        private CallLogRowView _logPrefab;

        [SerializeField]
        private Transform _rowsParent;

        [SerializeField]
        private Toggle _toggle;

        private List<CallLogRowView> _currentRows;

        public int Order => 3;

        public bool IsReady { get; private set; }

        private readonly FilterConfig _filter = new FilterConfig()
        {
            OnlyNonPaid = false,
            OrderByDate = true
        };

        public class FilterConfig
        {
            public bool OnlyNonPaid { get; set; }

            public bool OrderByDate { get; set; }
        }

        public async Task Initialize()
        {
            IsReady = true;
            RunPresentation("Заполняем счета");
        }

        private void Awake()
        {
            _currentRows = new List<CallLogRowView>();
            _toggle.isOn = _filter.OnlyNonPaid;
            _toggle.onValueChanged.AddListener(OnPaidFilterChanged);
        }

        private void RunPresentation(string taskName)
        {
            _busyIndicator.Wait(Present(), taskName);
        }

        private async UniTask Present()
        {
            try
            {
                Clear();

                var collection = await GetLogsByFilter(_filter);

                foreach (var invoiceViewModel in collection)
                {
                    var logView = _diContainer.InstantiatePrefabForComponent<CallLogRowView>(_logPrefab, _rowsParent);
                    logView.Paid += OnPaid;
                    _currentRows.Add(logView);
                    logView.Present(invoiceViewModel);
                    await UniTask.Yield();
                }
            }
            catch (Exception e)
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError(e);
#endif
                throw;
            }
        }

        private async Task<IEnumerable<InvoiceViewModel>> GetLogsByFilter(FilterConfig filterConfig)
        {
            IEnumerable<InvoiceViewModel> result = await _paymentService.GetAllInvoices();

            if (filterConfig.OrderByDate)
            {
                result = result.OrderByDescending(x => x.Date);
            }

            if (filterConfig.OnlyNonPaid)
            {
                result = result.Where(x => !x.Status);
            }

            return result;
        }

        private void OnPaid(InvoiceViewModel invoiceViewModel)
        {
            RunPresentation("Обновляем данные");
        }

        private void OnPaidFilterChanged(bool onlyNotPaid)
        {
            _filter.OnlyNonPaid = onlyNotPaid;
            RunPresentation("Обновляем данные");
        }

        public void Clear()
        {
            foreach (var row in _currentRows)
            {
                Destroy(row.gameObject);
            }

            _currentRows.Clear();
        }
    }
}