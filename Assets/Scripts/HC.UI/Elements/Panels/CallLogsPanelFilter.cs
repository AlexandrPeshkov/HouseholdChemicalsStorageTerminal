using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HC.UI.Elements
{
    public class CallLogsPanelFilter : MonoBehaviour
    {
        [SerializeField]
        private Toggle _toggle;

        [SerializeField]
        private TMP_InputField _searchInput;

        private readonly ReactiveProperty<FilterConfig> _config = new ReactiveProperty<FilterConfig>(
            new FilterConfig()
            {
                OnlyNonPaid = false,
                OrderByDate = true
            });

        public IReadOnlyReactiveProperty<FilterConfig> Config => _config;

        private void Awake()
        {
            _toggle.isOn = _config.Value.OnlyNonPaid;
            _toggle.onValueChanged.AddListener(OnPaidFilterChanged);
            _searchInput.onValueChanged.AddListener(OnSearchInputChanged);
        }

        private void OnPaidFilterChanged(bool onlyNotPaid)
        {
            _config.Value.OnlyNonPaid = onlyNotPaid;
            _config.SetValueAndForceNotify(_config.Value);
        }

        private void OnSearchInputChanged(string filter)
        {
            _config.Value.UerNameMask = filter;
            _config.SetValueAndForceNotify(_config.Value);
        }
    }

    public class FilterConfig
    {
        public bool OnlyNonPaid { get; set; }

        public bool OrderByDate { get; set; }

        public string UerNameMask { get; set; }
    }
}