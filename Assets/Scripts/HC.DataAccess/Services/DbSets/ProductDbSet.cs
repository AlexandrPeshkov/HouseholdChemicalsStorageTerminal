using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Domain.Entities;

namespace HC.DataAccess.Logic.DbSets
{
    public class ProductDbSet : DbSet<Product>
    {
        public override string TableName { get; } = "Products";

        public ProductDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override ICollection<Product> ReadAll()
        {
            throw new NotImplementedException();
        }

        public override Product Get(int key)
        {
            throw new NotImplementedException();
        }

        public override void WriteOrUpdate(Product entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }

        private string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS \"{TableName}\" (" +
                $"\"{nameof(Product.Id)}\"	INTEGER NOT NULL UNIQUE," +
                $"\"{nameof(Product.Name)}\"	TEXT NOT NULL," +
                $"\"{nameof(Product.Description)}\"	TEXT," +
                $"PRIMARY KEY(\"{nameof(Product.Id)}\" AUTOINCREMENT)" +
                ");";
        }

        public override async Task EnsureCreated()
        {
            await _dbContext.ExecuteNonQuery(CreateTableIfNotExistSql());
        }
    }
}