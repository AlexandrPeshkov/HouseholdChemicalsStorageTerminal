using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.UI.Services;
using Cysharp.Threading.Tasks;
using Interfaces.Services;
using UI.ViewModels;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Elements
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
        private CallLogsPanelFilter _filter;

        private List<CallLogRowView> _currentRows;

        private bool _inProcess;

        public int Order => 10;

        public bool IsReady { get; private set; }

        public async Task Initialize()
        {
            IsReady = true;
            RunPresentation("Заполняем счета");
            _filter.Config.Subscribe(OnConfigChanged).AddTo(this);
        }

        private void Awake()
        {
            _currentRows = new List<CallLogRowView>();
        }

        private void OnConfigChanged(FilterConfig filterConfig)
        {
            RunPresentation("Обновляем данные");
        }

        private void RunPresentation(string taskName)
        {
            _busyIndicator.Wait(Present(), taskName);
        }

        private async UniTask Present()
        {
            if (_inProcess)
            {
                return;
            }

            _inProcess = true;

            try
            {
                Clear();

                var collection = await GetLogsByFilter(_filter.Config.Value);

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
            finally
            {
                _inProcess = false;
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

            if (!string.IsNullOrWhiteSpace(filterConfig.UerNameMask))
            {
                var mask = filterConfig.UerNameMask;

                result = result.Where(x =>
                    x.UserFrom.Contains(mask)
                    || x.UserTo.Contains(mask)
                    || x.UserFromNumber.Contains(mask)
                    || x.UserToNumber.Contains(mask)
                    || x.DistrictName.Contains(mask)
                    || x.ProviderFromName.Contains(mask));
            }

            return result;
        }

        private void OnPaid(InvoiceViewModel invoiceViewModel)
        {
            RunPresentation("Обновляем данные");
        }

        private void Clear()
        {
            foreach (var row in _currentRows)
            {
                Destroy(row.gameObject);
            }

            _currentRows.Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}