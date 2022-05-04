namespace HC.DataAccess.Logic.DbSets
{
    public class RateDbSet : DbSet<Rate>
    {
        public RateDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override string TableName => "Rates";

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS {TableName} (" +
                $"{nameof(Rate.Id)}	INTEGER NOT NULL UNIQUE," +
                $"{nameof(Rate.CostPerMinute)} REAL NOT NULL," +
                $"{nameof(Rate.CityIdFrom)} INTEGER NOT NULL," +
                $"{nameof(Rate.CityIdTo)} INTEGER NOT NULL," +
                $"PRIMARY KEY({nameof(Rate.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(Rate.CityIdTo)}) REFERENCES {CityDbSet.Table}({nameof(City.Id)})" +
                $"FOREIGN KEY({nameof(Rate.CityIdFrom)}) REFERENCES {CityDbSet.Table}({nameof(City.Id)})" +
                ");";
        }

        protected override string SelectWhereSql()
        {
            throw new System.NotImplementedException();
        }
    }
}