using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.EntityConfiguration
{
    public class RestaurantEntityTypeConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurants", "dbo");

            builder.Ignore(r => r.DomainEvents);
            builder.HasKey(r => r.Id);

            builder.Property(e => e.Name);

            builder.HasMany(e => e.Branches)
                .WithOne(b=> b.Restaurant)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}
