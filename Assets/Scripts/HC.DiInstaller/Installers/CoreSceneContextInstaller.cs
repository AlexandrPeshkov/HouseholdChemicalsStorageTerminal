using HC.Core;
using HC.UI.Elements;
using UnityEngine;
using Zenject;

namespace HC.DiInstaller
{
    public class CoreSceneContextInstaller : MonoInstaller<CoreSceneContextInstaller>
    {
        [SerializeField]
        private UserCallLogsTable _userCallLogsTable;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ServiceInitializer>().AsSingle();
            BindUI();
            
            DbInstaller.Install(Container);
        }

        private void BindUI()
        {
            Container.BindInstance(_userCallLogsTable).AsSingle();
        }
    }
}