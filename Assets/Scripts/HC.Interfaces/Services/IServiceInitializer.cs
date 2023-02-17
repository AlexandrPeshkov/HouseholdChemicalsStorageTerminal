using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IServiceInitializer
    {
        Task InitializeAll();
    }
}