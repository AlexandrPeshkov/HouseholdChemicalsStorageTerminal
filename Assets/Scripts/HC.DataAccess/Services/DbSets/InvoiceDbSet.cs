namespace HC.DataAccess.Logic.DbSets
{
    public class InvoiceDbSet : DbSet<Invoice>
    {
        public InvoiceDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override string TableName => "Invoices";

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS {TableName} (" +
                $"{nameof(Invoice.Id)}	INTEGER NOT NULL UNIQUE," +
                //$"{nameof(Invoice.Cost)} REAL NOT NULL," +
                $"{nameof(Invoice.IsPaid)} INTEGER NOT NULL," +
                $"{nameof(Invoice.CallLogId)} INTEGER NOT NULL," +
                $"PRIMARY KEY({nameof(Invoice.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(Invoice.CallLogId)}) REFERENCES {CallLogDbSet.Table}({nameof(CallLog.Id)})" +
                ");";
        }

        protected override string SelectWhereSql()
        {
            throw new System.NotImplementedException();
        }
    }
}