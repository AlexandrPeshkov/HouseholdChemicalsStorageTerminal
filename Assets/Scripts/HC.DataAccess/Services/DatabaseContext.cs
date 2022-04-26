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
        public const string DbFolder = "../../DB~";

        public const string DbName = "HC.db";

        private readonly string _connectionString;

        private readonly string _dbPath;

        private readonly string _dpFolderPath;

        public DatabaseContext()
        {
            //_dbPath = Path.Combine(Application.persistentDataPath, DbName);
            _dpFolderPath = Path.Combine(Application.dataPath, DbFolder);
            _dbPath = Path.Combine(_dpFolderPath, DbName);
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
                    File.Delete(_dbPath);
                    // var fs = new FileStream(_dbPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.DeleteOnClose);
                    //
                    // // FileStream fileStream = new FileStream(_dbPath, 
                    // //     FileMode.Open, 
                    // //     FileAccess.ReadWrite, 
                    // //     FileShare.Delete);
                    // // File.Delete(_dbPath);
                    // fs.Close();
                    // fs.Dispose();
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
                if (!Directory.Exists(_dpFolderPath))
                {
                    Directory.CreateDirectory(_dpFolderPath);
                }

                var fileStream = new FileStream(_dbPath,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.Delete);
                fileStream.Dispose();

                //File.Create(_dbPath);
            }
        }

        public Task ExecuteNonQuery(string sqlCommand)
        {
            //using (IDbConnection connection = new SqliteConnection(_connectionString))
            IDbConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sqlCommand;
            command.ExecuteReader();
            connection.Close();

            return Task.CompletedTask;

            try
            {
                // await Task.Run(() =>
                // {
                //     using (IDbConnection connection = new SqliteConnection(_connectionString))
                //     {
                //         connection.Open();
                //         var command = connection.CreateCommand();
                //         command.CommandText = sqlCommand;
                //         command.ExecuteReader();
                //         connection.Close();
                //     }
                // });
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