using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.DataAccess.Extensions;
using DataAccess.Interfaces;
using UnityEngine;

namespace DataAccess.Logic
{
    public abstract class DbSet<TEntity> : IDbSet<TEntity>
        where TEntity : class, IDbEntity, new()
    {
        protected readonly DatabaseContext _dbContext;

        public abstract string TableName { get; }

        protected DbSet(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var predicateStr = SqlReaderExtensions.ParsePredicate(predicate);

            var sql = $"SELECT * FROM {TableName}" +
                $"\nWHERE {predicateStr}" +
                $"\nLIMIT 1";

            var table = await _dbContext.ExecuteResultQuery<TEntity>(sql);
            var entity = table.FirstOrDefault();
            return entity;
        }

        public Task<IEnumerable<TEntity>> Where(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Create(TEntity newEntity)
        {
            try
            {
                await _dbContext.ExecuteNonQuery(CreateSql(newEntity));
                var id = await LastInsertedId();
                var entity = await Get(id);
                return entity;
            }
            catch (Exception e)
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError(e);
#endif
                throw;
            }
        }
        
        private string CreateSql(TEntity entity)
        {
            var queryProperties = SqlReaderExtensions.ParseToQueryArgs(entity);

            var names = string.Join(", ", queryProperties.Keys);
            var values = string.Join(", ", queryProperties.Values);

            var sql = $"INSERT INTO {TableName}({names})" +
                $"\n VALUES ({values})";

            return sql;
        }

        public async Task<TEntity> Get(int id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {nameof(IDbEntity.Id)} = {id} LIMIT 1";
            var table = await _dbContext.ExecuteResultQuery<TEntity>(sql);
            var entity = table.FirstOrDefault();
            return entity;
        }

        public async Task<IReadOnlyCollection<TEntity>> All()
        {
            var sql = $"SELECT * FROM {TableName}";
            var table = await _dbContext.ExecuteResultQuery<TEntity>(sql);
            return table;
        }

        public async Task Update(TEntity entity)
        {
            var queryProperties = SqlReaderExtensions.ParseToQueryArgs(entity);

            var setExpression = string.Join(",\n", queryProperties.Select(pair => $"{pair.Key} = {pair.Value}"));

            var sql = $"UPDATE {TableName} \n" +
                $"SET {setExpression} \n" +
                $"WHERE {nameof(IDbEntity.Id)} = {entity.Id}";

            await _dbContext.ExecuteNonQuery(sql);
        }

        public async Task<int> LastInsertedId()
        {
            var sql = $"SELECT {nameof(IDbEntity.Id)} FROM {TableName} ORDER BY Id DESC LIMIT 1";
            var id = await _dbContext.ExecuteScalar<long>(sql);
            return (int)id;
        }

        public async Task EnsureCreated()
        {
            await _dbContext.ExecuteNonQuery(CreateTableIfNotExistSql());
        }

        protected abstract string CreateTableIfNotExistSql();
    }
}