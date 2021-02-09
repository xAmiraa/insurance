using InsuranceClaims.Data.DbModels.LookupSchema;
using InsuranceClaims.Data.DbModels.SecuritySchema;
using InsuranceClaims.Data.ThirdPartyInfo;
using InsuranceClaims.Enums;
using InsuranceClaims.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace InsuranceClaims.Data.DataContext
{
    public class DataSeedingIntilization 
    {
        private static AppDbContext _appDbContext;
        private static UserManager<ApplicationUser> _userManager;
        private static IServiceProvider _serviceProvider;

        public static void Seed(AppDbContext appDbContext,IServiceProvider serviceProvider)
        {
            _appDbContext = appDbContext;
            //_appDbContext.Database.EnsureDeleted();
            _appDbContext.Database.Migrate();
            _serviceProvider = serviceProvider;

            var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            // call functions
            SeedApplicationRoles();
            SeedApplicationSuperAdmin();
            SeedCoverageTypes();
            SeedClaimStatuses();
            //SeedPolicyTypes();
            SeedCountries();
            SeedCurrencies();

            // save to the database
            _appDbContext.SaveChanges();
        }

        private static void SeedApplicationRoles()
        {
            var items = _appDbContext.Roles.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(ApplicationRolesEnum));
                ApplicationRolesEnum[] values = (ApplicationRolesEnum[])Enum.GetValues(typeof(ApplicationRolesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.Roles.Add(new ApplicationRole() 
                    { 
                        Id = (int)values[i], 
                        Name = values[i].GetDescription(),
                        NormalizedName = names[i].ToUpper()
                    });
                }
                _appDbContext.SaveChanges();
            }
        }
        private static void SeedApplicationSuperAdmin()
        {
            var superAdmin = _userManager.FindByNameAsync("admin@gmail.com");
            if (superAdmin.Result == null)
            {
                var applicationUser = new ApplicationUser() {
                    EmailConfirmed = true,
                    Status = UserStatusEnum.Active.ToString(),
                    FirstName = "Super",
                    LastName = "Admin",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    LockoutEnabled = false,
                    CreatedBy = null,
                    CreatedOn = DateTime.Now,
                    NextPasswordExpiryDate = DateTime.Now.AddDays(30)
                };

                var result = _userManager.CreateAsync(applicationUser, "Admin@2010");
                if (result.Result.Succeeded)
                {
                    superAdmin = _userManager.FindByEmailAsync("admin@gmail.com");
                    _appDbContext.UserRoles.Add(new ApplicationUserRole { RoleId = (int)ApplicationRolesEnum.SuperAdmin, UserId = superAdmin.Result.Id });
                }
            }

        }
        private static void SeedCoverageTypes()
        {
            var items = _appDbContext.CoverageTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(CoverageTypesEnum));
                CoverageTypesEnum[] values = (CoverageTypesEnum[])Enum.GetValues(typeof(CoverageTypesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.CoverageTypes.Add(new CoverageType()
                    {
                        Id = (int)values[i],
                        Name = values[i].GetDescription()
                    });
                }
                _appDbContext.SaveChanges();
            }
        }
        private static void SeedClaimStatuses()
        {
            var items = _appDbContext.ClaimStatuses.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(ClaimStatusesEnum));
                ClaimStatusesEnum[] values = (ClaimStatusesEnum[])Enum.GetValues(typeof(ClaimStatusesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.ClaimStatuses.Add(new ClaimStatus()
                    {
                        Id = (int)values[i],
                        Name = values[i].GetDescription()
                    });
                }
                _appDbContext.SaveChanges();
            }
        }
        private static void SeedPolicyTypes()
        {
            var items = _appDbContext.PolicyTypes.ToList();
            if (items == null || items.Count == 0)
            {
                string[] names = Enum.GetNames(typeof(PolicyTypesEnum));
                PolicyTypesEnum[] values = (PolicyTypesEnum[])Enum.GetValues(typeof(PolicyTypesEnum));

                for (int i = 0; i < names.Length; i++)
                {
                    _appDbContext.PolicyTypes.Add(new PolicyType()
                    {
                        Id = (int)values[i],
                        Name = values[i].GetDescription()
                    });
                }
                _appDbContext.SaveChanges();
            }
        }
        private static void SeedCountries()
        {
            List<Country> allcountry = _appDbContext.Countries.ToList();

            if (allcountry == null || allcountry.Count() == 0)
            {

                HttpClient http = new HttpClient();
                var data = http.GetAsync("https://restcountries.eu/rest/v2/all").Result.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<List<CountryInfoApi>>(data);
                var countries = model.ConvertAll(x =>
                {
                    return new Country
                    {
                        Name = x.Name,
                        Description = "the subregion of this country is " + x.Subregion + " and capital of this country is " + x.Capital,
                        IsDeleted = false,
                        Code = x.Alpha3Code,
                        Flag = x.Flag,
                        NativeName = x.NativeName,
                        CurrencyCode = x.Currencies.Count() > 0 ? x.Currencies[0].Code : null,
                        CallingCode = x.CallingCodes.Count() > 0 && !string.IsNullOrEmpty(x.CallingCodes[0]) ? $"+{x.CallingCodes[0]}" : null,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    };
                });

                _appDbContext.Countries.AddRange(countries);
            }

        }
        private static void SeedCurrencies()
        {
            List<Currency> allCurrencies = _appDbContext.Currencies.ToList();

            if (allCurrencies == null || allCurrencies.Count() == 0)
            {
                HttpClient http = new HttpClient();
                var data = http.GetAsync("https://openexchangerates.org/api/currencies.json").Result.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<Object>(data);

                var json = JsonConvert.SerializeObject(model);
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                var currencies = dictionary.ToList().ConvertAll(x =>
                {
                    return new Currency
                    {
                        Name = x.Value,
                        Code = x.Key,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    };
                });

                _appDbContext.Currencies.AddRange(currencies);
            }

        }
    }
}
