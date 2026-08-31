using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class LogiTrackContext : IdentityDbContext<IdentityUser>
{
    public LogiTrackContext()
    {
    }

    public LogiTrackContext(DbContextOptions<LogiTrackContext> options)
        : base(options)
    {
    }

    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlite("Data Source=logitrack.db");
        }
    }
}