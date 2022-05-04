using System;

namespace HC.DataAccess.Logic.DbSets
{
    public class CityDbSet : DbSet<City>
    {
        public const string Table = "Cities";

        public override string TableName => Table;

        public CityDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS \"{TableName}\" (" +
                $"\"{nameof(City.Id)}\"	INTEGER NOT NULL UNIQUE," +
                $"\"{nameof(City.Code)}\"INTEGER NOT NULL," +
                $"\"{nameof(City.Name)}\" TEXT NOT NULL," +
                $"PRIMARY KEY(\"{nameof(City.Id)}\" AUTOINCREMENT)" +
                ");";
        }

        protected override string SelectWhereSql()
        {
            throw new NotImplementedException();
        }
    }
}