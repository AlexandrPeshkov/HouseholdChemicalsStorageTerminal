using System.Collections.Generic;
using DefaultNamespace;

namespace HC.DataAccess.Logic.DbSets
{
    public class CallLogDbSet : DbSet<CallLog>
    {
        public const string Table = "CallLogs";

        public override string TableName => Table;

        public CallLogDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override ICollection<CallLog> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public override CallLog Get(int key)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteOrUpdate(CallLog entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Remove(int id)
        {
            throw new System.NotImplementedException();
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
    }
}