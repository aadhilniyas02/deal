using dealSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace dealSystem.Data;

public class DealContext : DbContext // Db Context works with Database
{
    public DealContext(DbContextOptions<DealContext> options) : base(options)
    {

    }

    public DbSet<Deal> DealManagementSystem { get; set; } 
                        //Database name
}