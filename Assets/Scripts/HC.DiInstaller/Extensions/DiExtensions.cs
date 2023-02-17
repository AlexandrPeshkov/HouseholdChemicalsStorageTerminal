using DataAccess.Interfaces;
using DataAccess.Logic;
using Zenject;

namespace DiInstaller.DiInstaller.Extensions
{
    public static class DiExtensions
    {
        public static void BindDbSet<TEntity>(this DiContainer container) where TEntity : class, IDbEntity, new()
        {
            container.Bind<IDbSet<TEntity>>().To<DbSet<TEntity>>().AsSingle();
        }
    }
}