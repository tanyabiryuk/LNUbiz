using LNUbiz.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LNUbiz.DAL
{
    public class LNUbizDBContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public LNUbizDBContext(DbContextOptions<LNUbizDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BusinessTripRequest> BusinessTripRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(x => x.BusinessTripRequests)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BusinessTripRequest>()
                .HasOne(a => a.User)
                .WithMany(u => u.BusinessTripRequests)
                .HasForeignKey(a => a.UserId);
        }
    }
}
