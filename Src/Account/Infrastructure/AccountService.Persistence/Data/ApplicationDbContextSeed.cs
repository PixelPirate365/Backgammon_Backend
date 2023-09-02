using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Persistence.Data {
    public static class ApplicationDbContextSeed {
        public static async Task SeedAccountData(ApplicationDbContext dbContext) {
           await SeedCurrencies(dbContext);
        }
        private static async Task SeedCurrencies(ApplicationDbContext dbContext) {
            var newCurrencies = new List<Currency> {
                new Currency {
                    Id= Guid.NewGuid(),
                    Name="Coin"
                }
            };
            var currencies = await dbContext.Currency.ToListAsync();
            var result = newCurrencies.Where(x => !currencies.Select(
                z => z.Id).Contains(x.Id)).ToList();
            await dbContext.Currency.AddRangeAsync(result);
            await dbContext.SaveChangesAsync();
        }
    }
}
