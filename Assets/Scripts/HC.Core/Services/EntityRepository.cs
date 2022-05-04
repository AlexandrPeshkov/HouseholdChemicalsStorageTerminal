using System.Threading.Tasks;
using HC.DataAccess;
using HC.DataAccess.Interfaces;
using HC.Interfaces.Services;

namespace HC.Core.Logic
{
    public class EntityRepository : IEntityRepository, IAsyncInitializable
    {
        public IDbSet<City> Cities { get; }

        public IDbSet<User> Users { get; }

        public IDbSet<Rate> Rates { get; }

        public IDbSet<CallLog> CallLogs { get; }

        public IDbSet<Invoice> Invoices { get; }

        public EntityRepository(IDbSet<City> cities,
            IDbSet<User> users,
            IDbSet<Rate> rates,
            IDbSet<CallLog> callLogs,
            IDbSet<Invoice> invoices)
        {
            Cities = cities;
            Users = users;
            Rates = rates;
            CallLogs = callLogs;
            Invoices = invoices;
        }

        public int Order => 0;

        public bool IsReady { get; private set; }

        public async Task Initialize()
        {
            await Cities.EnsureCreated();
            await Users.EnsureCreated();
            await Rates.EnsureCreated();
            await CallLogs.EnsureCreated();
            await Invoices.EnsureCreated();

            IsReady = true;
        }
    }
}