namespace DataAccess.Logic.DbSets
{
    public class UserDbSet : DbSet<User>
    {
        public const string Table = "Users";

        public override string TableName => Table;

        public UserDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS {TableName} (" +
                $"{nameof(User.Id)}	INTEGER NOT NULL UNIQUE," +
                $"{nameof(User.Name)} TEXT NOT NULL," +
                $"{nameof(User.CityId)}	INTEGER NOT NULL," +
                $"{nameof(User.ProviderAccountId)}	INTEGER NOT NULL," +
                $"PRIMARY KEY({nameof(User.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(User.CityId)}) REFERENCES {CityDbSet.Table}({nameof(City.Id)})" +
                $"FOREIGN KEY({nameof(User.ProviderAccountId)}) REFERENCES {ProviderAccountDbSet.Table}({nameof(ProviderAccount.Id)})" +
                ");";
        }
    }
}