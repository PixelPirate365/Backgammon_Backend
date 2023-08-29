using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Persistence.Configurations {
    public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken> {
        public void Configure(EntityTypeBuilder<RefreshToken> builder) {
            builder.HasKey(x => x.Token);
        }
    }
}
