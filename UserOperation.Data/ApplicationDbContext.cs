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
        new DbInitializer(builder).Seed();
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

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}

public class DbInitializer
{
    private readonly ModelBuilder _builder;
    public DbInitializer(ModelBuilder builder)
    {
        _builder = builder;
    }
    public void Seed()
    {
        _builder.Entity<Criticality>().HasData(
             new Criticality()
             {
                 CriticalityID = 1,
                 CriticalityName = "Critical"
             },
             new Criticality()
             {
                 CriticalityID = 2,
                 CriticalityName = "Highcritical"
             },
             new Criticality()
             {
                 CriticalityID = 3,
                 CriticalityName = "Noncritical"
             }
            );
        _builder.Entity<StabilityLevel>().HasData(
            new StabilityLevel()
            {
                StabilityLevelID = 1,
                StabilityLevelName = "Certain"
            },
            new StabilityLevel()
            {
                StabilityLevelID = 2,
                StabilityLevelName = "Moderately certain"
            },
            new StabilityLevel()
            {
                StabilityLevelID = 3,
                StabilityLevelName = "Uncertain"
            }
            );
        _builder.Entity<Level>().HasData(
            new Level()
            {
                LevelId = 1,
                LevelName = "L0"
            },
            new Level()
            {
                LevelId = 2,
                LevelName = "L1"
            },
            new Level()
            {
                LevelId = 3,
                LevelName = "L2"
            }
            );
        _builder.Entity<Position>().HasData(
            new Position()
            {
                PositionId = 1,
                PositionName = "Software Developer"
            },
            new Position()
            {
                PositionId = 2,
                PositionName = "Senior Software Developer"
            },
            new Position()
            {
                PositionId = 3,
                PositionName = "QA Manual"
            },
            new Position()
            {
                PositionId = 4,
                PositionName = "Junior UX Designer"
            },
            new Position()
            {
                PositionId = 5,
                PositionName = "Junior Software Developer"
            }
                    );
        _builder.Entity<Project>().HasData(
             new Project()
             {
                 ProjectId = 1,
                 ProjectName = "Flowbird"
             },
             new Project()
             {
                 ProjectId = 2,
                 ProjectName = "Porsche"
             },
             new Project()
             {
                 ProjectId = 3,
                 ProjectName = "Westfield"
             },
             new Project()
             {
                 ProjectId = 4,
                 ProjectName = "Benenden"
             },
             new Project()
             {
                 ProjectId = 5,
                 ProjectName = "Tivo"
             }

            );
        _builder.Entity<Reason>().HasData(
             new Reason()
             {
                 ReasonId = 1,
                 ReasonName = "Compensation"
             },
             new Reason()
             {
                 ReasonId = 2,
                 ReasonName = "Project related"
             },
             new Reason()
             {
                 ReasonId = 3,
                 ReasonName = "Relocation"
             },
             new Reason()
             {
                 ReasonId = 4,
                 ReasonName = "Personal issues"
             }
            );
    }
}