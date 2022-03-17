using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToolbox.DataAccessLayer.Configurations.Common;
using MyToolbox.DataAccessLayer.Entities;

namespace MyToolbox.DataAccessLayer.Configurations;

public class OrderConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.ToTable("Orders");

        builder.Property(x => x.CreationDate).HasColumnName("ORD_DT");
        builder.Property(x => x.Status).HasConversion<string>();
    }
}
