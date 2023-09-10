using GameManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Persistence.Configurations {
    public class MoveConfiguration : IEntityTypeConfiguration<Move> {
        public void Configure(EntityTypeBuilder<Move> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}
