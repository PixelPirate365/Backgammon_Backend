using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Entities {
    public class UserContext : IdentityDbContext<User> {
        public UserContext(DbContextOptions<UserContext> options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);


        }
    }
}
