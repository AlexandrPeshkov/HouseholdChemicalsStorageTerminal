using System.Collections.Generic;
using DefaultNamespace;

namespace HC.DataAccess.Logic.DbSets
{
    public class InvoiceDbSet : DbSet<Invoice>
    {
        public InvoiceDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override string TableName => "Invoices";

        public override ICollection<Invoice> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public override Invoice Get(int key)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteOrUpdate(Invoice entity)
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
                $"{nameof(Invoice.Id)}	INTEGER NOT NULL UNIQUE," +
                $"{nameof(Invoice.Cost)} REAL NOT NULL," +
                $"{nameof(Invoice.IsPaid)} INTEGER NOT NULL," +
                $"{nameof(Invoice.CallLogId)}	INTEGER NOT NULL," +
                $"PRIMARY KEY({nameof(Invoice.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(Invoice.CallLogId)}) REFERENCES {CallLogDbSet.Table}({nameof(CallLog.Id)})" +
                ");";
        }
    }
}