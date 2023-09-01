using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Persistence.Configurations {
    public class AccountProfileConfiguration : IEntityTypeConfiguration<AccountProfile> {
        public void Configure(EntityTypeBuilder<AccountProfile> builder) {
            builder.HasKey(x=>x.Id);
        }
    }
}
