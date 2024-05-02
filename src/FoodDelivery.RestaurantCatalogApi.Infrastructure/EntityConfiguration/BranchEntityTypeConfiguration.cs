using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.EntityConfiguration
{
    public class BranchEntityTypeConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches", "dbo");

            builder.Ignore(r => r.DomainEvents);
            builder.HasKey(r => r.Id);

            builder.Property(e => e.IsAvailable);

            builder.OwnsOne( e=> e.Address, b =>
            {
                b.Property(e => e.City);
                b.Property(e => e.Country);
                b.Property(e => e.Street);
                b.Property(e => e.Home);
            });

            builder.OwnsOne(e => e.WorkingHours, b =>
            {
                b.Property(e => e.Start).HasColumnType("time").HasColumnName("opening_time");
                b.Property(e => e.End).HasColumnType("time").HasColumnName("closing_time");
            });
            builder.Ignore(e => e.DomainEvents);

            builder.HasMany(e=> e.Dishes).WithOne(b=> b.Branch);

            //builder.HasOne(e => e.Restaurant).WithMany();
        }
    }
}
