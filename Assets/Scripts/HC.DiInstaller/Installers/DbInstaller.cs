using HC.Core;
using HC.Core.Logic;
using HC.DataAccess.Logic;
using HC.DataAccess.Logic.DbSets;
using Zenject;

namespace HC.DiInstaller
{
    public class DbInstaller : Installer<DbInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DatabaseContext>().AsSingle();
            Container.BindInterfacesTo<EntityRepository>().AsSingle();
            Container.BindInterfacesTo<DatabaseSeedService>().AsSingle();

            BindDbSets();
        }

        private void BindDbSets()
        {
            Container.BindInterfacesTo<CityDbSet>().AsSingle();
            Container.BindInterfacesTo<UserDbSet>().AsSingle();
            Container.BindInterfacesTo<RateDbSet>().AsSingle();
            Container.BindInterfacesTo<CallLogDbSet>().AsSingle();
            Container.BindInterfacesTo<InvoiceDbSet>().AsSingle();
        }
    }
}