using GameManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Persistence.Configurations {
    public class GamePlayersConfiguration : IEntityTypeConfiguration<GamePlayers> {
        public void Configure(EntityTypeBuilder<GamePlayers> builder) {
            builder.HasKey(x => x.Id);
        }
    }
}
