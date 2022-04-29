using HC.UI.ViewModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HC.UI.Elements
{
    public class CallLogRowView : MonoBehaviour
    {
        [field: SerializeField]
        public TMP_Text Date { get; private set; }

        [field: SerializeField]
        public TMP_Text UserTo { get; private set; }

        [field: SerializeField]
        public TMP_Text CityFrom { get; private set; }

        [field: SerializeField]
        public TMP_Text CityTo { get; private set; }

        [field: SerializeField]
        public TMP_Text Cost { get; private set; }

        [field: SerializeField]
        public TMP_Text StatusText { get; private set; }

        [field: SerializeField]
        public Button StatusPayButton { get; private set; }

        private void Awake()
        {
            StatusPayButton.onClick.AddListener(OnPayButtonClick);
        }

        public void Present(CallLogViewModel viewModel)
        {
            Date.text = viewModel.Date.ToString();
            UserTo.text = viewModel.UserToNumber;
            CityFrom.text = viewModel.CityFrom;
            CityTo.text = viewModel.CityTo;
            Cost.text = viewModel.Cost.ToString();

            StatusText.gameObject.SetActive(viewModel.Status);
            StatusPayButton.gameObject.SetActive(!viewModel.Status);
        }

        private void OnPayButtonClick()
        {
        }
    }
}