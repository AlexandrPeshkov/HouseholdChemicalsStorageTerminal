using System.Collections.Generic;
using System.Threading.Tasks;
using HC.DataAccess.Interfaces;

namespace HC.DataAccess.Logic
{
    public abstract class DbSet<TEntity> : IDbSet<TEntity>
        where TEntity : class, IDbEntity
    {
        protected readonly DatabaseContext _dbContext;

        public abstract string TableName { get; }

        protected DbSet(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public abstract ICollection<TEntity> ReadAll();

        public abstract TEntity Get(int key);

        public abstract void WriteOrUpdate(TEntity entity);

        public abstract void Remove(int id);

        public abstract Task EnsureCreated();
    }
}