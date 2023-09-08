using Microsoft.EntityFrameworkCore;
using Shared.Constants;

namespace Stock.Infrastructure.Persistence;

public class StockDbContext : DbContext
{
    public StockDbContext()
    {
    }
    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
    {

    }

    public DbSet<Domain.Entities.Stock> Stocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionStrings.StockDb);
        base.OnConfiguring(optionsBuilder);
    }
}