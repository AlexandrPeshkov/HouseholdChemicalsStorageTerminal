namespace DataAccess.Logic.DbSets
{
    public class ProviderDbSet : DbSet<Provider>
    {
        public ProviderDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        
        public const string Table = "Provider";

        public override string TableName => Table;
        
        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS \"{TableName}\" (" +
                $"\"{nameof(Provider.Id)}\"	INTEGER NOT NULL UNIQUE," +
                $"\"{nameof(Provider.Name)}\" TEXT NOT NULL," +
                $"PRIMARY KEY({nameof(AccountType.Id)} AUTOINCREMENT)" +
                ");";
        }
    }
}