using Microsoft.EntityFrameworkCore;
using Api.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public DbSet<Cashier> Cashier { get; set; }
    public DbSet<Inventory> Inventory { get; set; }
}
