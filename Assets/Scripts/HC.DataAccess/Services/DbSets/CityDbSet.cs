namespace DataAccess.Logic.DbSets
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
                $"\"{nameof(City.CountryId)}\"	INTEGER NOT NULL," +
                $"\"{nameof(City.Code)}\"TEXT NOT NULL," +
                $"\"{nameof(City.Name)}\" TEXT NOT NULL," +
                $"FOREIGN KEY({nameof(City.CountryId)}) REFERENCES {CountryDbSet.Table}({nameof(Country.Id)})" +
                $"PRIMARY KEY(\"{nameof(City.Id)}\" AUTOINCREMENT)" +
                ");";
        }
    }
}