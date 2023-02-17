namespace DataAccess.Logic.DbSets
{
    public class ProviderAccountDbSet : DbSet<ProviderAccount>
    {
        public ProviderAccountDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override string TableName => Table;
        
        public const string Table = "ProviderAccounts";

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS {TableName} (" +
                $"{nameof(ProviderAccount.Id)}	INTEGER NOT NULL UNIQUE," +
                $"{nameof(ProviderAccount.Number)} TEXT NOT NULL," +
                $"{nameof(ProviderAccount.ProviderId)} INTEGER NOT NULL," +
                $"{nameof(ProviderAccount.CountryId)} INTEGER NOT NULL," +
                $"{nameof(ProviderAccount.AccountTypeId)} INTEGER NOT NULL," +
                $"PRIMARY KEY({nameof(ProviderAccount.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(ProviderAccount.ProviderId)}) REFERENCES {ProviderDbSet.Table}({nameof(Provider.Id)})," +
                $"FOREIGN KEY({nameof(ProviderAccount.CountryId)}) REFERENCES {CountryDbSet.Table}({nameof(Country.Id)})," +
                $"FOREIGN KEY({nameof(ProviderAccount.AccountTypeId)}) REFERENCES {AccountTypeDbSet.Table}({nameof(AccountType.Id)})" +
                ");";
        }
    }
}