using BS.UI.Services;
using Core;
using Core.Services;
using Interfaces.Services;
using UI.Elements;
using UI.Elements.Popups;
using UnityEngine;
using Zenject;

namespace DiInstaller
{
    public class CoreSceneContextInstaller : MonoInstaller<CoreSceneContextInstaller>
    {
        [SerializeField]
        private UserCallLogsTable _userCallLogsTable;

        [SerializeField]
        private BusyIndicator _busyIndicator;

        [SerializeField]
        private PaymentPopup _paymentPopup;

        public override void InstallBindings()
        {
            BindServices();
            BindUI();

            DbInstaller.Install(Container);
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<UserCallLogsTable>().FromInstance(_userCallLogsTable).AsSingle();
            Container.BindInstance(_busyIndicator).AsSingle();
            Container.BindInstance(_paymentPopup).AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<ServiceInitializer>().AsSingle();
            Container.Bind<IPaymentService>().To<PaymentService>().AsSingle();
            Container.BindInterfacesTo<AppConfigProvider>().AsSingle();
        }
    }
}