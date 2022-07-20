using Microsoft.EntityFrameworkCore;
using CRMServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRMServer.Data
{
    public class CRMContext : IdentityDbContext<AppUser>
    {
        public CRMContext (DbContextOptions<CRMContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<AppUser> AppUser { get; set; }
    }
}
