using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BS.UI.Services
{
    /// <summary>
    /// Индикатор загрузки
    /// </summary>
    public class BusyIndicator : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField]
        private float _rotationStep = 45.0f;

        [SerializeField]
        private float _rotationDelayBetweenSteps = 0.5f;

        [SerializeField]
        private bool _runOnEnable;

        [SerializeField]
        private bool _clockwise;

        [SerializeField]
        private GameObject _busyIndicatorContent;

        [SerializeField]
        private Transform _imageTransform;

        [SerializeField]
        private TMP_Text _operationLabel;

        #endregion SerializeFields

        #region Fields

        private bool _isRunning;

        private Coroutine _currentOperation;

        private Coroutine _animation;

        private float _actualRotationStep;

        private WaitForSeconds _waitForRotationDelay;

        private Quaternion _cachedRotation;

        private Vector3 _cachedRotationEulerAngles;

        #endregion Fields

        #region IViewFrontAwaiter implementation

        public void Wait(Task task, string label = null)
        {
            if (_currentOperation != null)
            {
                StopCoroutine(_currentOperation);
            }

            _operationLabel.text = label ?? "Выполняется операция";
            Show();
            _currentOperation = StartCoroutine(Waiter(task));
        }

        public void Wait(UniTask task, string label = null)
        {
            if (_currentOperation != null)
            {
                StopCoroutine(_currentOperation);
            }

            _operationLabel.text = label ?? "Выполняется операция";
            Show();
            _currentOperation = StartCoroutine(Waiter(task));
        }

        public void StopOperation()
        {
            if (_currentOperation != null)
            {
                StopCoroutine(_currentOperation);
            }

            if (_animation != null)
            {
                StopCoroutine(_animation);
            }

            _operationLabel.text = string.Empty;
            _currentOperation = null;
            _isRunning = false;
            Hide();
        }

        #endregion IViewFrontAwaiter implementation

        #region Private Methods

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            if (_runOnEnable)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Initialize()
        {
            _actualRotationStep = _clockwise ? -_rotationStep : _rotationStep;
            _waitForRotationDelay = new WaitForSeconds(_rotationDelayBetweenSteps);
            _cachedRotation = _imageTransform.rotation;
            _cachedRotationEulerAngles = _cachedRotation.eulerAngles;
        }

        private void Show()
        {
            if (_busyIndicatorContent != null)
            {
                _busyIndicatorContent.SetActive(true);
            }

            _isRunning = true;
            RunAnimation();
        }

        private IEnumerator Animate()
        {
            while (_isRunning)
            {
                _cachedRotationEulerAngles.z += _actualRotationStep;
                _cachedRotation.eulerAngles = _cachedRotationEulerAngles;
                _imageTransform.rotation = _cachedRotation;

                yield return _waitForRotationDelay;
            }
        }

        private IEnumerator Waiter(Task handle)
        {
            yield return new WaitUntil(() => handle.IsCompleted);

            StopOperation();
        }

        private IEnumerator Waiter(UniTask handle)
        {
            yield return new WaitUntil(() => handle.Status != UniTaskStatus.Pending);

            StopOperation();
        }

        private void Hide()
        {
            _isRunning = false;

            if (_busyIndicatorContent != null)
            {
                _busyIndicatorContent.SetActive(false);
            }
        }

        private void RunAnimation()
        {
            if (_animation != null)
            {
                StopCoroutine(_animation);
            }

            _animation = StartCoroutine(Animate());
        }

        #endregion Private Methods
    }
}