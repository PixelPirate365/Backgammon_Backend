using GameManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameManagerService.Persistence.Configurations {
    public class GameConfiguration : IEntityTypeConfiguration<Game> {
        public void Configure(EntityTypeBuilder<Game> builder) {
            builder.HasKey(x => x.Id);
            //builder.Property(x => x.StartTime).Metadata.GetColumnType() > DateTime.UtcNow;
        }
    }
}
