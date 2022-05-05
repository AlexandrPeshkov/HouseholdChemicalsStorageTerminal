using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using HC.DataAccess.HC.DataAccess.Extensions;
using HC.DataAccess.Interfaces;
using HC.Interfaces.Services;
using Mono.Data.Sqlite;
using UnityEngine;

namespace HC.DataAccess.Logic
{
    public class DatabaseContext : IAsyncInitializable
    {
        private const string DbName = "CallsDB.db";

        private readonly string _connectionString;

        private readonly string _dbPath;

        private readonly IDbConfigProvider _configProvider;

        public int Order => 1;

        public bool IsReady { get; private set; }

        public DatabaseContext(IDbConfigProvider configProvider)
        {
            _dbPath = Path.Combine(Application.persistentDataPath, DbName);
            _connectionString = $"URI=file:{_dbPath}";
            _configProvider = configProvider;
        }

        public async Task Initialize()
        {
            if (_configProvider.AppConfig.Value.ClearAndSeedDb)
            {
                await EnsureDeleted();
            }

            await EnsureCreated();
            IsReady = true;
        }

        private async Task EnsureDeleted()
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
                    await ExecuteNonQuery(sql);
                }
                catch (IOException e)
                {
#if DEBUG || UNITY_EDITOR
                    Debug.LogError(e);
#endif
                }
            }
        }

        private async Task EnsureCreated()
        {
            if (!File.Exists(_dbPath))
            {
                using (var fs = new FileStream(_dbPath, FileMode.Create))
                {
                    fs.Flush();
                    fs.Close();
                    await Task.Delay(10);
                }
                //File.Create(_dbPath);
            }
        }

        public async Task ExecuteNonQuery(string sqlCommand)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = sqlCommand;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError(e);
                Debug.LogError($"SQL: \n {sqlCommand}");
#endif
                throw;
            }
        }

        public async Task<IReadOnlyCollection<TEntity>> ExecuteResultQuery<TEntity>(string sqlCommand)
            where TEntity : class, IDbEntity, new()
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = sqlCommand;

                    using (var reader = command.ExecuteReader())
                    {
                        var entities = Map<TEntity>(reader);
                        return entities;
                    }
                }
            }
            catch (Exception e)
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError(e);
                Debug.LogError($"SQL: \n {sqlCommand}");
#endif
                throw;
            }
        }

        public async Task<T> ExecuteScalar<T>(string sqlCommand)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = sqlCommand;
                    var answer = command.ExecuteScalar();
                    var result = (T)answer;
                    return result;
                }
            }
            catch (Exception e)
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError(e);
                Debug.LogError($"SQL: \n {sqlCommand}");
#endif
                throw;
            }
        }

        private IReadOnlyCollection<TEntity> Map<TEntity>(SqliteDataReader reader)
            where TEntity : class, IDbEntity, new()
        {
            var properties = typeof(TEntity).GetProperties(BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty |
                BindingFlags.SetProperty);

            var entities = new List<TEntity>();

            using (reader)
            {
                while (reader.Read())
                {
                    var entity = new TEntity();

                    foreach (var propertyInfo in properties)
                    {
                        var value = reader.ReadValue(propertyInfo.Name, propertyInfo.PropertyType);
                        propertyInfo.SetValue(entity, value);
                    }

                    entities.Add(entity);
                }
            }

            return entities;
        }
    }
}