using System;
using System.Globalization;
using UI.Elements.Popups;
using UI.ViewModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Elements
{
    public class CallLogRowView : MonoBehaviour
    {
        [Inject]
        private readonly PaymentPopup _paymentPopup;

        [field: SerializeField]
        public TMP_Text Date { get; private set; }

        [field: SerializeField]
        public TMP_Text UserTo { get; private set; }
        
        [field: SerializeField]
        public TMP_Text UserFrom{ get; private set; }

        [field: SerializeField]
        public TMP_Text District { get; private set; }
        
        [field: SerializeField]
        public TMP_Text Provider { get; private set; }

        [field: SerializeField]
        public TMP_Text CityTo { get; private set; }

        [field: SerializeField]
        public TMP_Text Cost { get; private set; }

        [field: SerializeField]
        public TMP_Text StatusText { get; private set; }

        [field: SerializeField]
        public Button StatusPayButton { get; private set; }

        private CultureInfo _ruCulture;

        private InvoiceViewModel _viewModel;

        public event Action<InvoiceViewModel> Paid;

        private void Awake()
        {
            StatusPayButton.onClick.AddListener(OnPayButtonClick);
            _ruCulture = CultureInfo.GetCultureInfo("ru-RU");
        }

        public void Present(InvoiceViewModel viewModel)
        {
            _viewModel = viewModel;

            Date.text = viewModel.Date.ToString();

            UserTo.text = $"{viewModel.UserToNumber} \n {viewModel.UserTo}";
            UserFrom.text = $"{viewModel.UserFromNumber} \n {viewModel.UserFrom}";

            District.text = viewModel.DistrictName;
            Provider.text = viewModel.ProviderFromName;

            Cost.text = viewModel.Cost.ToString("N2", _ruCulture);

            StatusText.gameObject.SetActive(viewModel.Status);
            StatusPayButton.gameObject.SetActive(!viewModel.Status);
        }

        private void OnPayButtonClick()
        {
            _paymentPopup.Present(_viewModel.InvoiceId);
            _paymentPopup.PaymentCompleted += OnPaymentCompleted;
        }

        private void OnPaymentCompleted(int invoiceId)
        {
            _paymentPopup.PaymentCompleted -= OnPaymentCompleted;
            Paid?.Invoke(_viewModel);
        }

        private void OnDestroy()
        {
            Paid = null;
        }
    }
}