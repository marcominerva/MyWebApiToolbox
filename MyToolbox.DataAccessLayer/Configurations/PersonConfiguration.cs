using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToolbox.DataAccessLayer.Configurations.Common;
using MyToolbox.DataAccessLayer.Entities;

namespace MyToolbox.DataAccessLayer.Configurations;

public class PersonConfiguration : BaseEntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);

        builder.ToTable("People");

        builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
        builder.Property(x => x.City).HasMaxLength(50);
    }
}
