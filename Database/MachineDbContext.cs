using machines.Jobs;
using Microsoft.EntityFrameworkCore;

namespace machines.DataBase;

public class MachineDbContext : DbContext
{
    public MachineDbContext(DbContextOptions<MachineDbContext> options) : base(options)
    {
    }

    public DbSet<Machine> Machines { get; set; }

    public DbSet<Job> Jobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("MachineDb");
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCollation("CaseInsensitive", "en-u-ks-primary", "icu", deterministic: false);
        modelBuilder.UseDefaultColumnCollation("CaseInsensitive");

        modelBuilder.Entity<Machine>().Property<int>("Id");
        modelBuilder.Entity<Machine>().HasKey("Id");
        modelBuilder.Entity<Machine>().OwnsMany(machine => machine.Jobs, jobBuilder =>
        {
            jobBuilder.WithOwner().HasForeignKey("FK_MachineId");
            jobBuilder.Property<int>("Id");
            jobBuilder.HasKey("Id");
        });
    }
}