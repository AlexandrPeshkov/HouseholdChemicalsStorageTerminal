using HC.DataAccess.Interfaces;
using HC.Domain.Entities;

namespace HC.Interfaces.Services
{
    public interface IEntityRepository
    {
        public IDbSet<Product> Products { get; }
    }
}