using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Mono.Data.Sqlite;
using UnityEngine;

namespace HC.DataAccess.Logic
{
    public class DatabaseContext
    {
        //public const string DbFolder = "../../DB~";

        public const string DbName = "HC.db";

        private readonly string _connectionString;

        private readonly string _dbPath;

        //private readonly string _dpFolderPath;

        public DatabaseContext()
        {
            _dbPath = Path.Combine(Application.persistentDataPath, DbName);
            //_dpFolderPath = Path.Combine(Application.dataPath, DbFolder);
            //_dbPath = Path.Combine(_dpFolderPath, DbName);
            //_dbPath = $"{Application.dataPath}/../../{DbName}";
            _connectionString = $"URI=file:{_dbPath}";

            EnsureDeleted();
            EnsureCreated();
        }

        private void EnsureDeleted()
        {
            if (File.Exists(_dbPath))
            {
                try
                {
                    var sql = string.Concat(new[]
                    {
                        "PRAGMA writable_schema = 1;",
                        "delete from sqlite_master where type in ('table', 'index', 'trigger');",
                        "PRAGMA writable_schema = 0;",
                        "VACUUM;"
                    });
                    ExecuteNonQuery(sql).GetAwaiter();
                }
                catch (IOException e)
                {
#if DEBUG || UNITY_EDITOR
                    Debug.LogError(e);
#endif
                }
            }
        }

        private void EnsureCreated()
        {
            if (!File.Exists(_dbPath))
            {
                File.Create(_dbPath);
            }
        }

        public async Task ExecuteNonQuery(string sqlCommand)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (IDbConnection connection = new SqliteConnection(_connectionString))
                    {
                        connection.Open();
                        var command = connection.CreateCommand();
                        command.CommandText = sqlCommand;
                        command.ExecuteReader();
                        //connection.Close();
                    }
                });
            }
            catch (Exception e)
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError(e);
#endif
                throw;
            }
        }
    }
}