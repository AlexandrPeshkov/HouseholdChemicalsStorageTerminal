using System.Collections.Generic;
using HC.UI.ViewModels;
using UnityEngine;
using Zenject;

namespace HC.UI.Elements
{
    public class UserCallLogsTable : MonoBehaviour
    {
        [Inject]
        private readonly IInstantiator _diContainer;

        [SerializeField]
        private CallLogRowView _logPrefab;

        [SerializeField]
        private Transform _rowsParent;

        private List<CallLogRowView> _currentRows;

        private void Awake()
        {
            _currentRows = new List<CallLogRowView>();
        }

        public void Present(IEnumerable<CallLogViewModel> table)
        {
            Clear();
            
            foreach (var rowModel in table)
            {
                var row = _diContainer.InstantiatePrefabForComponent<CallLogRowView>(_logPrefab, _rowsParent);
                _currentRows.Add(row);
                row.Present(rowModel);
            }
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