using GameManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Persistence.Configurations {
    public class FriendGameRequestConfiguration : IEntityTypeConfiguration<FriendGameRequest> {
        public void Configure(EntityTypeBuilder<FriendGameRequest> builder) {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.PlayerSender)
                .WithMany(x=>x.RecieverFriendGameRequests)
                .HasForeignKey(x=>x.PlayerSenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.PlayerReciever)
                .WithMany(x => x.SenderFriendGameRequests)
                .HasForeignKey(x=>x.PlayerRecieverId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
