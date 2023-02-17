namespace DataAccess.Logic.DbSets
{
    public class DistrictDbSet : DbSet<District>
    {
        public const string Table = "District";

        public override string TableName => Table;
        
        public DistrictDbSet(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string CreateTableIfNotExistSql()
        {
            return $"CREATE TABLE IF NOT EXISTS \"{TableName}\" (" +
                $"\"{nameof(District.Id)}\"	INTEGER NOT NULL UNIQUE," +
                $"\"{nameof(District.Name)}\" TEXT NOT NULL," +
                $"\"{nameof(District.CityId)}\" INTEGER NOT NULL," +
                $"FOREIGN KEY({nameof(District.CityId)}) REFERENCES {CityDbSet.Table}({nameof(City.Id)})" +
                $"PRIMARY KEY(\"{nameof(District.Id)}\" AUTOINCREMENT)" +
                ");";
        }
    }
}