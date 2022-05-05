using HC.Data.HC.Data;
using UniRx;

namespace HC.DataAccess.Interfaces
{
    public interface IDbConfigProvider
    {
        public IReadOnlyReactiveProperty<DbConfig> AppConfig { get; }
    }
}