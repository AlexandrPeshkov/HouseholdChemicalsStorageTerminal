using Data.Data;
using UniRx;

namespace DataAccess.Interfaces
{
    public interface IDbConfigProvider
    {
        public IReadOnlyReactiveProperty<DbConfig> AppConfig { get; }
    }
}