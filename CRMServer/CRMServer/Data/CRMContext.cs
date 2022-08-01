using Microsoft.EntityFrameworkCore;
using CRMServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CRMServer.Models.CRM;

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
            //one to one AppUser <=> Contact
            base.OnModelCreating(builder);
            builder.Entity<AppUser>()
                .HasOne<Contact>(c => c.Contact)
                .WithOne(a => a.User)
                .HasForeignKey<Contact>(a => a.UserId);
            //one to many Account <=> Contacts
            builder.Entity<Contact>()
                .HasOne<Account>(a => a.Account)
                .WithMany(c => c.Contacts);
        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}
