using System.Collections.Generic;
using DefaultNamespace;

namespace HC.DataAccess.Logic.DbSets
{
    public class UserDbSet : DbSet<User>
    {
        public const string Table = "Users";
        public override string TableName => Table;

        public UserDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override ICollection<User> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public override User Get(int key)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteOrUpdate(User entity)
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
                $"{nameof(User.Id)}	INTEGER NOT NULL UNIQUE," +
                $"{nameof(User.Name)} TEXT NOT NULL," +
                $"{nameof(User.Number)} TEXT NOT NULL," +
                $"{nameof(User.CityId)}	INTEGER NOT NULL," +
                $"PRIMARY KEY({nameof(User.Id)} AUTOINCREMENT)," +
                $"FOREIGN KEY({nameof(User.CityId)}) REFERENCES {CityDbSet.Table}({nameof(City.Id)})" +
                ");";
        }
    }
}