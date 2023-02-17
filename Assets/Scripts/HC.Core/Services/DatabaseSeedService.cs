using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Logic;
using Core.Services;
using DataAccess;
using DataAccess.Interfaces;
using Interfaces.Services;
using UnityEngine;
using Random = System.Random;

namespace Core
{
    public class DatabaseSeedService : IAsyncInitializable
    {
        private readonly EntityRepository _entityRepository;

        private readonly IDbConfigProvider _configProvider;

        private Random Random => ThreadSafeRandom.Random;

        public int Order => 3;

        public bool IsReady { get; private set; }

        public DatabaseSeedService(EntityRepository entityRepository, IDbConfigProvider configProvider)
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
                await SeedAccountTypes();
                await SeedProviders();
                await SeedCountries();
                await SeedCities();
                await SeedRostovDistricts();
                await SeedProviderAccounts();
                
                await SeedProviderRates();
              
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

        private async Task SeedAccountTypes()
        {
            var callTypes = new List<AccountType>()
            {
                new AccountType() { Name = "Mobile" },
                new AccountType() { Name = "Home" },
                new AccountType() { Name = "Service" },
            };

            foreach (AccountType accountType in callTypes)
            {
                await _entityRepository.AccountTypes.Create(accountType);
            }
        }

        private async Task SeedProviders()
        {
            var providers = new List<Provider>()
            {
                new Provider() { Name = "Local" },
                new Provider() { Name = "MTC" },
                new Provider() { Name = "TELE2" },
                new Provider() { Name = "Beeline" },
            };
            
            foreach (Provider provider in providers)
            {
                await _entityRepository.Providers.Create(provider);
            }
        }

        private async Task SeedProviderAccounts()
        {
            IReadOnlyCollection<AccountType> accountTypes = await _entityRepository.AccountTypes.All();
            IReadOnlyCollection<Provider> providers = await _entityRepository.Providers.All();

            var ru = (await _entityRepository.Countries.All()).FirstOrDefault(x => x.Name == RuName);

            var providerAccounts = new List<ProviderAccount>();

            foreach (Provider provider in providers)
            {
                foreach (AccountType accountType in accountTypes)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        var providerAccount = new ProviderAccount()
                        {
                            CountryId = ru.Id,
                            AccountTypeId = accountType.Id,
                            Number = Random.RandomNumberFor(accountType),
                            ProviderId = provider.Id
                        };
                    
                        providerAccounts.Add(providerAccount);
                    }
                }
            }

            foreach (ProviderAccount providerAccount in providerAccounts)
            {
                await _entityRepository.ProviderAccounts.Create(providerAccount);
            }
        }


        public const string RuName = "RU";
        public const int RostovCode = 8632;

        private async Task SeedCountries()
        {
            await _entityRepository.Countries.Create(new Country()
            {
                Code = "+7",
                Name = RuName
            });
        }

        private async Task SeedCities()
        {
            var countries = await _entityRepository.Countries.All();
            var RF = countries.FirstOrDefault(x => x.Name == RuName);
            
            var cities = new List<City>()
            {
                new City { Code = RostovCode, Name = "Ростов-на-Дону", CountryId = RF.Id},
                new City { Code = 86342, Name = "Азов", CountryId = RF.Id},
                new City { Code = 473, Name = "Воронеж", CountryId = RF.Id},
                new City { Code = 495, Name = "Москва", CountryId = RF.Id}
            };

            foreach (var city in cities)
            {
                await _entityRepository.Cities.Create(city);
            }
        }

        private async Task SeedRostovDistricts()
        {
            var cities = await _entityRepository.Cities.All();
            var rostov = cities.FirstOrDefault(x => x.Code == 8632);

            var districts = new List<District>()
            {
                new District() { CityId = rostov.Id, Name = "Ворошиловский" },
                new District() { CityId = rostov.Id, Name = "Западный" },
                new District() { CityId = rostov.Id, Name = "Северный" },
                new District() { CityId = rostov.Id, Name = "Железнодорожный" },
                new District() { CityId = rostov.Id, Name = "Советский" },
                new District() { CityId = rostov.Id, Name = "Кировский" },
            };

            foreach (District district in districts)
            {
                await _entityRepository.Districts.Create(district);
            }
        }

        private async Task SeedProviderRates()
        {
            List<ProviderRate> providerRates = new List<ProviderRate>();

            var providers = await _entityRepository.Providers.All();
            var accounts = await _entityRepository.AccountTypes.All();

            foreach (Provider provider in providers)
            {
                foreach (AccountType accountType in accounts)
                {
                    var rate = 0f;

                    switch ((AccountTypeEnum)accountType)
                    {
                        case AccountTypeEnum.Service:
                            rate = 0f;
                            break;
                        case AccountTypeEnum.Mobile:
                            rate = Random.Range(1f, 5f);
                            break;
                        case AccountTypeEnum.Home:
                            rate = Random.Range(9f, 20f);
                            break;
                    }
                    

                    var providerRate = new ProviderRate()
                    {
                        CallingAccountTypeId = accountType.Id,
                        ProviderId = provider.Id,
                        Rate = rate
                    };
                    
                    providerRates.Add(providerRate);
                }
              
            }

            foreach (ProviderRate providerRate in providerRates)
            {
                await _entityRepository.ProviderRates.Create(providerRate);
            }
        }

        private async Task SeedUsers()
        {
            var cities = await _entityRepository.Cities.All();
            var providerAccounts = await _entityRepository.ProviderAccounts.All();
            
            foreach (ProviderAccount providerAccount in providerAccounts)
            {
                await _entityRepository.Users.Create(new User()
                {
                    Name = await GetName(providerAccount),
                    CityId = cities.GetRandom().Id,
                    ProviderAccountId = providerAccount.Id
                });
            }
        }

        private readonly List<string> userNames = new List<string>
        {
            "Иванов",
            "Петров",
            "Сидоров",
            "Морозов",
            "Ткаченко",
            "Осипов",
            "Михайлов",
            "Трутов",
            "Москов",
            "Филатов",
            "Марченко",
            "Пешков",
            "Елин",
            "Фомин",
            "Пушкин"
        };

        private async Task<string> GetName(ProviderAccount providerAccount)
        {
            AccountType accountType = await _entityRepository.AccountTypes.Get(providerAccount.AccountTypeId);

            switch ((AccountTypeEnum)accountType)
            {
                case AccountTypeEnum.Service:
                    switch (providerAccount.Number)
                    {
                        case "01": return "Пожарная";
                        case "02": return "Полиция";
                        case "03": return "Скорая";
                        case "04": return "Газ";
                        default: return $"Служба {providerAccount.Number}";
                    }

                    break;
                case AccountTypeEnum.Home:
                case AccountTypeEnum.Mobile:
                    return userNames.GetRandom();
                default: return string.Empty;
            }
        }

        private async Task SeedCallLog()
        {
            IReadOnlyCollection<User> users = await _entityRepository.Users.All();
            IReadOnlyCollection<District> districts = await _entityRepository.Districts.All();
            IReadOnlyCollection<ProviderAccount> accounts = await _entityRepository.ProviderAccounts.All();

            var size = 40;

            for (var i = 0; i < size; i++)
            {
                User userFrom = users.GetRandom();
                User userTo = users.GetRandom();
                District district = districts.GetRandom();

                while (userFrom.Id == userTo.Id)
                {
                    userTo = users.GetRandom();
                }

                var caller = accounts.GetRandom();
                var accepter = accounts.GetRandom();
                
                var accountType = await _entityRepository.AccountTypes.Get(caller.AccountTypeId);

                if ((AccountTypeEnum)accountType == AccountTypeEnum.Service || accepter.Id == caller.Id)
                {
                    i = -1;
                    continue;
                }
                

                await _entityRepository.CallLogs.Create(new CallLog()
                {
                    Date = Random.NearDateTime(),
                    Duration = Random.Range(10, 400),
                    DistrictId = district.Id,
                    ProviderAccountIdFrom = caller.Id,
                    ProviderAccountIdTo = accepter.Id
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