namespace DataAccess.Logic.DbSets
{
    public class CountryDbSet : DbSet<Country>
    {
        public const string Table = "Countries";
        
        public override string TableName => Table;
        
        public CountryDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS \"{TableName}\" (" +
                $"\"{nameof(Country.Id)}\"	INTEGER NOT NULL UNIQUE," +
                $"\"{nameof(Country.Code)}\"INTEGER NOT NULL," +
                $"\"{nameof(Country.Name)}\" TEXT NOT NULL," +
                $"PRIMARY KEY(\"{nameof(Country.Id)}\" AUTOINCREMENT)" +
                ");";
        }
    }
}