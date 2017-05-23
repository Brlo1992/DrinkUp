using DrinkUp.WebApi.Model.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrinkUp.WebApi.Context {
    public interface IAuthorizationContext {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
    }

    public class AuthorizationContext : IdentityDbContext<User>, IAuthorizationContext {
        public AuthorizationContext(DbContextOptions options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}