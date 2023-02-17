using System.Threading.Tasks;
using DataAccess.Logic.DbSets;
using Interfaces.Services;

namespace Core.Logic
{
    public class EntityRepository : IAsyncInitializable
    {
        public CityDbSet Cities { get; }
        
        public CountryDbSet Countries { get; }
        
        public DistrictDbSet Districts { get; }

        public UserDbSet Users { get; }

        public CallLogDbSet CallLogs { get; }

        public InvoiceDbSet Invoices { get; }
        
        public AccountTypeDbSet AccountTypes { get; }
        
        public ProviderDbSet Providers { get; }
        
        public ProviderAccountDbSet ProviderAccounts { get; }
        
        public ProviderRateDbSet ProviderRates { get; }

        public EntityRepository(
            CityDbSet cities,
            UserDbSet users,
            AccountTypeDbSet accountTypes,
            CallLogDbSet callLogs,
            InvoiceDbSet invoices,
            ProviderDbSet providers,
            ProviderAccountDbSet providerAccounts,
            CountryDbSet countries,
            DistrictDbSet districts,
            ProviderRateDbSet providerRates
            )
        {
            Cities = cities;
            Users = users;
            AccountTypes = accountTypes;
            CallLogs = callLogs;
            Invoices = invoices;
            Providers = providers;
            ProviderAccounts = providerAccounts;
            Countries = countries;
            Districts = districts;
            ProviderRates = providerRates;
        }

        public int Order => 2;

        public bool IsReady { get; private set; }

        public async Task Initialize()
        {
            await Countries.EnsureCreated();
            await Cities.EnsureCreated();
            await Districts.EnsureCreated();
            
            await AccountTypes.EnsureCreated();
            await Providers.EnsureCreated();
            await ProviderRates.EnsureCreated();
            
            await ProviderAccounts.EnsureCreated();
            
            await CallLogs.EnsureCreated();
            await Invoices.EnsureCreated();
            
            await Users.EnsureCreated();
            IsReady = true;
        }
    }
}