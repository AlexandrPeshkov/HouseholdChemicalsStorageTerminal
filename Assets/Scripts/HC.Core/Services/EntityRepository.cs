using System.Threading.Tasks;
using HC.DataAccess.Interfaces;
using HC.Domain.Entities;
using HC.Interfaces.Services;

namespace HC.Core.Logic
{
    public class EntityRepository : IEntityRepository, IAsyncInitializable
    {
        public IDbSet<Product> Products { get; private set; }

        public EntityRepository(IDbSet<Product> products)
        {
            Products = products;
        }

        public int Order => 0;

        public bool IsReady { get; private set; }

        public async Task Initialize()
        {
            await Products.EnsureCreated();
            IsReady = true;
        }
    }
}