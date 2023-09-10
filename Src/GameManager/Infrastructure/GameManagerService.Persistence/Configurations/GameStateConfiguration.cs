using GameManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Persistence.Configurations {
    public class GameStateConfiguration : IEntityTypeConfiguration<GameState> {
        public void Configure(EntityTypeBuilder<GameState> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}
