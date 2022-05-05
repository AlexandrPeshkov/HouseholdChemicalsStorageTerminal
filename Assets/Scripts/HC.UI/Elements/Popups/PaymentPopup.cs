using System;
using System.Threading.Tasks;
using BS.UI.Services;
using HC.Interfaces.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HC.UI.Elements.Popups
{
    public class PaymentPopup : MonoBehaviour
    {
        [Inject]
        private readonly IPaymentService _paymentService;

        [SerializeField]
        private Button _yes;

        [SerializeField]
        private Button _no;

        [SerializeField]
        private BusyIndicator _busyIndicator;

        private int _invoiceId;

        public event Action<int> PaymentCompleted;

        private void Awake()
        {
            _yes.onClick.AddListener(OnYesClick);
            _no.onClick.AddListener(OnNoClick);
        }

        private void OnYesClick()
        {
            _busyIndicator.Wait(PaymentTask(), "Проводится оплата...");
        }

        private async Task PaymentTask()
        {
            await Task.Delay(900);
            await _paymentService.Pay(_invoiceId);
            gameObject.SetActive(false);
            PaymentCompleted?.Invoke(_invoiceId);
        }

        private void OnNoClick()
        {
            gameObject.SetActive(false);
            _busyIndicator.StopOperation();
        }

        public void Present(int invoiceId)
        {
            _invoiceId = invoiceId;
            gameObject.SetActive(true);
        }
    }
}