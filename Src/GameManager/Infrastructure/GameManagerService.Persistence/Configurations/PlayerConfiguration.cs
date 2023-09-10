using GameManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Persistence.Configurations {
    public class PlayerConfiguration : IEntityTypeConfiguration<Player> {
        public void Configure(EntityTypeBuilder<Player> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}
