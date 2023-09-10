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

            builder.HasOne(x => x.PlayerOne)
             .WithMany(x => x.PlayerTwoGamePlays)
             .HasForeignKey(x => x.PlayerOneId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PlayerTwo)
                .WithMany(x => x.PlayerOneGamePlays)
                .HasForeignKey(x => x.PlayerTwoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
