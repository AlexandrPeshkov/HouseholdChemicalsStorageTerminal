using System.Threading.Tasks;

namespace HC.Interfaces.Services
{
    public interface IAsyncInitializable
    {
        public int Order { get; }

        public bool IsReady { get; }

        Task Initialize();
    }
}