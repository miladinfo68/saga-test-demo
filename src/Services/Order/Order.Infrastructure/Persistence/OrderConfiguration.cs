using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastructure.Persistence;

public class OrderConfiguration : IEntityTypeConfiguration<Shared.Entities.Order>
{
    public void Configure(EntityTypeBuilder<Shared.Entities.Order> builder)
    {
        builder.OwnsOne(e => e.Address );
    }
}
