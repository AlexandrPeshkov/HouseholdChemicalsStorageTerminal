using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.DataAccess.Interfaces
{
    public interface IDbSet<TEntity> where TEntity : class, IDbEntity
    {
        ICollection<TEntity> ReadAll();

        TEntity Get(int key);

        void WriteOrUpdate(TEntity entity);

        void Remove(int id);

        Task EnsureCreated();
    }
}