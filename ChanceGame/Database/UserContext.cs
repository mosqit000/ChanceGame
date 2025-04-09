using ChanceGame.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChanceGame.Database;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Balance)
            .HasDefaultValue(10000); 

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<User> Users { get; set; }
}