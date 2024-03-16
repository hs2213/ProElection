using Microsoft.EntityFrameworkCore;
using ProElection.Entities;

namespace ProElection.Persistence;

public class ProElectionDbContext : DbContext
{
    public ProElectionDbContext(DbContextOptions<ProElectionDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=ProElection.db");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Election> Elections { get; set; }
    public DbSet<ElectionCode> ElectionCodes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vote> Votes { get; set; }
}