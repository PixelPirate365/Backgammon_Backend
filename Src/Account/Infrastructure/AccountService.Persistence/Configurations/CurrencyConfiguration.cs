using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Persistence.Configurations {
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency> {
        public void Configure(EntityTypeBuilder<Currency> builder) {
            builder.Property(x=>x.Id).ValueGeneratedNever();
        }
    }
}
