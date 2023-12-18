using Microsoft.EntityFrameworkCore;
using Productos.Models;

namespace Productos;

public class DBConnection : DbContext
{
    public DBConnection(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; } = null!;
}