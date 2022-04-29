using DefaultNamespace;
using HC.DataAccess.Interfaces;

namespace HC.Interfaces.Services
{
    public interface IEntityRepository
    {
        public IDbSet<City> Cities { get; }

        public IDbSet<User> Users { get; }
    }
}