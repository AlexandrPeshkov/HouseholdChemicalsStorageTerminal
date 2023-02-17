using Core;
using Core.Logic;
using DataAccess.Logic;
using DataAccess.Logic.DbSets;
using Zenject;

namespace DiInstaller
{
    public class DbInstaller : Installer<DbInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DatabaseContext>().AsSingle();
            Container.BindInterfacesAndSelfTo<EntityRepository>().AsSingle();
            Container.BindInterfacesTo<DatabaseSeedService>().AsSingle();

            BindDbSets();
        }

        private void BindDbSets()
        {
            Container.Bind<CityDbSet>().AsSingle();
            Container.Bind<UserDbSet>().AsSingle();
            Container.Bind<CallLogDbSet>().AsSingle();
            Container.Bind<InvoiceDbSet>().AsSingle();
            
            Container.Bind<AccountTypeDbSet>().AsSingle();
            Container.Bind<CountryDbSet>().AsSingle();
            Container.Bind<DistrictDbSet>().AsSingle();
            Container.Bind<ProviderAccountDbSet>().AsSingle();
            Container.Bind<ProviderDbSet>().AsSingle();
            Container.Bind<ProviderRateDbSet>().AsSingle();
        }
    }
}