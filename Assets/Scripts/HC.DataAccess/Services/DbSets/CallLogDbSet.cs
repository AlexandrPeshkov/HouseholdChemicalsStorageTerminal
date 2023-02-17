namespace DataAccess.Logic.DbSets
{
    public class CallLogDbSet : DbSet<CallLog>
    {
        public const string Table = "CallLogs";

        public override string TableName => Table;

        public CallLogDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS {TableName} (" +
                $"{nameof(CallLog.Id)}	INTEGER NOT NULL UNIQUE," +
                $"{nameof(CallLog.Duration)} REAL NOT NULL," +
                $"{nameof(CallLog.ProviderAccountIdFrom)} INTEGER NOT NULL," +
                $"{nameof(CallLog.ProviderAccountIdTo)} INTEGER NOT NULL," +
                $"{nameof(CallLog.DistrictId)} INTEGER NOT NULL," +
                $"{nameof(CallLog.Date)} TEXT NOT NULL," +
                $"PRIMARY KEY({nameof(CallLog.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(CallLog.ProviderAccountIdFrom)}) REFERENCES {ProviderAccountDbSet.Table}({nameof(ProviderAccount.Id)})," +
                $"FOREIGN KEY({nameof(CallLog.ProviderAccountIdTo)}) REFERENCES {ProviderAccountDbSet.Table}({nameof(ProviderAccount.Id)})," +
                $"FOREIGN KEY({nameof(CallLog.DistrictId)}) REFERENCES {DistrictDbSet.Table}({nameof(District.Id)})" +
                ");";
        }
    }
}