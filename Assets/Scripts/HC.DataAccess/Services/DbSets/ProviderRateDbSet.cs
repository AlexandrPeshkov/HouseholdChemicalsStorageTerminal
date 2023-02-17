namespace DataAccess.Logic.DbSets
{
    public class ProviderRateDbSet : DbSet<ProviderRate>
    {
        public ProviderRateDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override string TableName => Table;
        
        public const string Table = "ProviderRates";

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS {TableName} (" +
                $"{nameof(ProviderRate.Id)}	INTEGER NOT NULL UNIQUE," +
                $"{nameof(ProviderRate.Rate)} REAL NOT NULL," +
                $"{nameof(ProviderRate.ProviderId)} INTEGER NOT NULL," +
                $"{nameof(ProviderRate.CallingAccountTypeId)} INTEGER NOT NULL," +
                $"PRIMARY KEY({nameof(ProviderRate.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(ProviderRate.ProviderId)}) REFERENCES {ProviderDbSet.Table}({nameof(Provider.Id)})," +
                $"FOREIGN KEY({nameof(ProviderRate.CallingAccountTypeId)}) REFERENCES {AccountTypeDbSet.Table}({nameof(AccountType.Id)})" +
                ");";
        }
    }
}