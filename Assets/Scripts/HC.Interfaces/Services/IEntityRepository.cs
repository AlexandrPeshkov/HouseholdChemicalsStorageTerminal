using HC.DataAccess;
using HC.DataAccess.Interfaces;

namespace HC.Interfaces.Services
{
    public interface IEntityRepository
    {
        public IDbSet<City> Cities { get; }

        public IDbSet<User> Users { get; }

        public IDbSet<Rate> Rates { get; }

        public IDbSet<CallLog> CallLogs { get; }

        public IDbSet<Invoice> Invoices { get; }
    }
}