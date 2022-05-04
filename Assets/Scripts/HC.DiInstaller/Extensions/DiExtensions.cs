using HC.DataAccess.Interfaces;
using HC.DataAccess.Logic;
using Zenject;

namespace HC.DiInstaller.HC.DiInstaller.Extensions
{
    public static class DiExtensions
    {
        public static void BindDbSet<TEntity>(this DiContainer container) where TEntity : class, IDbEntity, new()
        {
            container.Bind<IDbSet<TEntity>>().To<DbSet<TEntity>>().AsSingle();
        }
    }
}