using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Core.Extensions;
using HC.Core.Services;
using HC.DataAccess;
using HC.DataAccess.Interfaces;
using HC.Interfaces.Services;
using UnityEngine;
using Random = System.Random;

namespace HC.Core
{
    public class DatabaseSeedService : IAsyncInitializable
    {
        private readonly IEntityRepository _entityRepository;

        private readonly IDbConfigProvider _configProvider;

        private Random Random => ThreadSafeRandom.Random;

        public int Order => 3;

        public bool IsReady { get; private set; }

        public DatabaseSeedService(IEntityRepository entityRepository, IDbConfigProvider configProvider)
        {
            _entityRepository = entityRepository;
            _configProvider = configProvider;
        }

        public async Task Initialize()
        {
            if (!_configProvider.AppConfig.Value.ClearAndSeedDb)
            {
                return;
            }

            try
            {
                await SeedCities();
                await SeedRates();
                await SeedUsers();
                await SeedCallLog();
                await SeedInvoices();
                await Task.Delay(1000);

                Debug.LogWarning("Сидирование завершено");
                IsReady = true;
            }
            catch (Exception e)
            {
#if DEBUG || UNITY_EDITOR
                Debug.LogError(e);
#endif
                throw;
            }
        }

        private async Task SeedCities()
        {
            var cities = new List<City>()
            {
                new City { Code = 8632, Name = "Ростов-на-Дону" },
                new City { Code = 86342, Name = "Азов" },
                new City { Code = 473, Name = "Воронеж" },
                new City { Code = 495, Name = "Москва" }
            };

            foreach (var city in cities)
            {
                await _entityRepository.Cities.Create(city);
            }
        }

        private async Task SeedRates()
        {
            var cities = await _entityRepository.Cities.All();

            foreach (var cityFrom in cities)
            {
                foreach (var cityTo in cities)
                {
                    await _entityRepository.Rates.Create(new Rate
                    {
                        CityIdFrom = cityFrom.Id,
                        CityIdTo = cityTo.Id,
                        CostPerMinute = Random.Range(3.2f, 12.7f)
                    });
                }
            }
        }

        private async Task SeedUsers()
        {
            var cities = await _entityRepository.Cities.All();

            var userNames = new List<string>
            {
                "Иванов",
                "Петров",
                "Сидоров",
                "Морозов",
                "Ткаченко",
                "Осипов"
            };

            foreach (var userName in userNames)
            {
                await _entityRepository.Users.Create(new User()
                {
                    Name = userName,
                    CityId = cities.GetRandom().Id,
                    Number = Random.PhoneNumber()
                });
            }
        }

        private async Task SeedCallLog()
        {
            var users = await _entityRepository.Users.All();

            var size = 20;

            for (var i = 0; i < size; i++)
            {
                var userFrom = users.GetRandom();
                var userTo = users.GetRandom();

                while (userFrom.Id == userTo.Id)
                {
                    userTo = users.GetRandom();
                }

                await _entityRepository.CallLogs.Create(new CallLog()
                {
                    UserIdFrom = userFrom.Id,
                    UserIdTo = userTo.Id,
                    Date = Random.NearDateTime(),
                    Duration = Random.Range(10, 400)
                });
            }
        }

        private async Task SeedInvoices()
        {
            var callLogs = await _entityRepository.CallLogs.All();

            foreach (var callLog in callLogs)
            {
                await _entityRepository.Invoices.Create(new Invoice()
                {
                    CallLogId = callLog.Id,
                    IsPaid = Random.FlipCoin()
                });
            }
        }
    }
}