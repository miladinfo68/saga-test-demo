using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.SeedData;

namespace Order.Infrastructure.Persistence;

public class ProductConfiguration : IEntityTypeConfiguration<Shared.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Shared.Entities.Product> builder)
    {
        builder.HasData(FakeDate.Products());
    }
}