namespace DataAccess.Logic.DbSets
{
    public class AccountTypeDbSet : DbSet<AccountType>
    {
        public override string TableName => Table;
        
        public const string Table = "AccountTypes";
        
        public AccountTypeDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
        
        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS \"{TableName}\" (" +
                $"\"{nameof(AccountType.Id)}\"	INTEGER NOT NULL UNIQUE," +
                $"\"{nameof(AccountType.Name)}\" TEXT NOT NULL," +
                $"PRIMARY KEY({nameof(AccountType.Id)} AUTOINCREMENT)" +
                ");";
        }
    }
}