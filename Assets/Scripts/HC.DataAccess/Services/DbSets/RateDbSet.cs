using System.Collections.Generic;
using DefaultNamespace;

namespace HC.DataAccess.Logic.DbSets
{
    public class RateDbSet : DbSet<Rate>
    {
        public RateDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override string TableName => "Rates";

        public override ICollection<Rate> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public override Rate Get(int key)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteOrUpdate(Rate entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS {TableName} (" +
                $"{nameof(Rate.Id)}	INTEGER NOT NULL UNIQUE," +
                $"{nameof(Rate.CostPerMinute)} REAL NOT NULL," +
                $"{nameof(Rate.CityCodeFrom)} INTEGER NOT NULL," +
                $"{nameof(Rate.CityCodeTo)} INTEGER NOT NULL," +
                $"PRIMARY KEY({nameof(Rate.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(Rate.CityCodeTo)}) REFERENCES {CityDbSet.Table}({nameof(City.Id)})" +
                $"FOREIGN KEY({nameof(Rate.CityCodeFrom)}) REFERENCES {CityDbSet.Table}({nameof(City.Id)})" +
                ");";
        }
    }
}