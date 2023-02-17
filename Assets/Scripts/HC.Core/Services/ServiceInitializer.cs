using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BS.UI.Services;
using Interfaces.Services;
using Zenject;

namespace Core
{
    public class ServiceInitializer : IServiceInitializer, IInitializable
    {
        private readonly ICollection<IAsyncInitializable> _services;

        private readonly BusyIndicator _busyIndicator;

        public ServiceInitializer(IEnumerable<IAsyncInitializable> services, BusyIndicator busyIndicator)
        {
            _services = services.ToList();
            _busyIndicator = busyIndicator;
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
            _busyIndicator.Wait( InitializeAll(), "Инициализация БД");
        }
    }
}