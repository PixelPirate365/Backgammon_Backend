using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Persistence.Configurations {
    public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest> {
        public void Configure(EntityTypeBuilder<FriendRequest> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(s => s.SenderProfile)
                   .WithMany(l => l.RecieveFriendRequests)
                   .HasForeignKey(s => s.SenderProfileId)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(s => s.RecieverProfile)
                   .WithMany(l => l.SendFriendRequests)
                   .HasForeignKey(s => s.RecieverProfileId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
    
