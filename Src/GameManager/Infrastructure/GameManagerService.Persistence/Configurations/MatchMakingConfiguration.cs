using GameManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Persistence.Configurations {
    internal class MatchMakingConfiguration : IEntityTypeConfiguration<MatchMaking> {
        public void Configure(EntityTypeBuilder<MatchMaking> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}
