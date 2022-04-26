using HC.Core;
using Zenject;

namespace HC.DiInstaller
{
    public class CoreSceneContextInstaller : MonoInstaller<CoreSceneContextInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ServiceInitializer>().AsSingle();
            //Container.Bind<DbTestRunner>().AsSingle().NonLazy();
            DbInstaller.Install(Container);
        }
    }
}