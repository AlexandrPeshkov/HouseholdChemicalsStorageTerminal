using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IAsyncInitializable
    {
        int Order { get; }

        bool IsReady { get; }

        Task Initialize();
    }
}