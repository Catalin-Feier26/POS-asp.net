using Microsoft.EntityFrameworkCore;
using RestaurantPOS.Models;

namespace RestaurantPOS.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options) { }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<MenuItem> MenuItems { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                Role = UserRoles.Admin,
                Email = "admin@email.com"
            }, 
            new User
            {
                Id = 2,
                Username = "server",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("server"),
                Role = UserRoles.Server,
                Email = "server@email.com"
            }
        );
    }
}