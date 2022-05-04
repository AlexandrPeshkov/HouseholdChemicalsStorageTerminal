namespace HC.DataAccess.Logic.DbSets
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
                $"{nameof(CallLog.UserIdFrom)} INTEGER NOT NULL," +
                $"{nameof(CallLog.UserIdTo)} INTEGER NOT NULL," +
                $"{nameof(CallLog.Date)} TEXT NOT NULL," +
                $"PRIMARY KEY({nameof(CallLog.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(CallLog.UserIdFrom)}) REFERENCES {UserDbSet.Table}({nameof(User.Id)})" +
                $"FOREIGN KEY({nameof(CallLog.UserIdTo)}) REFERENCES {UserDbSet.Table}({nameof(User.Id)})" +
                ");";
        }

        protected override string SelectWhereSql()
        {
            throw new System.NotImplementedException();
        }
    }
}