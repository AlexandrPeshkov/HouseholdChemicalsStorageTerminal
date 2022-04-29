using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;

namespace HC.DataAccess.Logic.DbSets
{
    public class CityDbSet : DbSet<City>
    {
        public const string Table = "Cities";

        public override string TableName => Table;

        public CityDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override ICollection<City> ReadAll()
        {
            throw new NotImplementedException();
        }

        public override City Get(int key)
        {
            throw new NotImplementedException();
        }

        public override void WriteOrUpdate(City entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS \"{TableName}\" (" +
                $"\"{nameof(City.Id)}\"	INTEGER NOT NULL UNIQUE," +
                $"\"{nameof(City.Name)}\" TEXT NOT NULL," +
                $"PRIMARY KEY(\"{nameof(City.Id)}\" AUTOINCREMENT)" +
                ");";
        }
    }
}