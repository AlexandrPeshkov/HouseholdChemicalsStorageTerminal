using System.Threading.Tasks;

namespace HC.Interfaces.Services
{
    public interface IServiceInitializer
    {
        Task InitializeAll();
    }
}