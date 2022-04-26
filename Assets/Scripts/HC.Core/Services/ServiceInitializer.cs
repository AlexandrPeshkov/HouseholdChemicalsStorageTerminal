using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HC.Interfaces.Services;
using Zenject;

namespace HC.Core
{
    public class ServiceInitializer : IServiceInitializer, IInitializable
    {
        private readonly ICollection<IAsyncInitializable> _services;

        public ServiceInitializer(IEnumerable<IAsyncInitializable> services)
        {
            _services = services.ToList();
        }

        public async Task InitializeAll()
        {
            foreach (var service in _services
                         .Where(x => !x.IsReady)
                         .OrderBy(x => x.Order))
            {
                await service.Initialize();
            }
        }

        public void Initialize()
        {
            InitializeAll();
        }
    }
}