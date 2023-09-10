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

            builder.HasOne(x => x.PlayerSender)
              .WithMany(x => x.RandomMatchMakingRequests)
              .HasForeignKey(x => x.PlayerSenderId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RandomMatchMaker)
                .WithMany(x => x.SenderMatchMakingRequests)
                .HasForeignKey(x => x.RandomMatchMakerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
