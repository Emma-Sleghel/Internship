using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserOperation.Data.Entities;

namespace UserOperation.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.Entity<Leave>().HasOne(l => l.PrimaryReason).WithMany().OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Leave>().HasOne(l => l.SecondaryReason).WithMany().OnDelete(DeleteBehavior.Restrict);
    }

    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Criticality> Criticalities { get; set; }
    public DbSet<StabilityLevel> StabilityLevels { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Reason> Reasons { get; set; }
    public DbSet<Stability> Stabilities { get; set; }
}

public class ApplicationUserEntityConfiguration:IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure (EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}
