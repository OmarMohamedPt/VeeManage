using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VMTS.Core.Entities.Identity;
using VMTS.Core.Entities.Identity.ActivityLog;

namespace VMTS.Repository.Identity;

public class IdentityDbContext : IdentityDbContext<AppUser>
{
    
    public DbSet<ActivityLog> ActivityLogs { get; set; }

    public IdentityDbContext (DbContextOptions<IdentityDbContext> options)
        :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Address>().ToTable("Addresses");
        builder.Entity<AppUser>()
            .HasIndex(u => u.UserName)
            .IsUnique(false);

    }
}