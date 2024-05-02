using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.EntityConfiguration
{
    public class DishTypeEntityTypeConfiguration : IEntityTypeConfiguration<DishType>
    {
        public void Configure(EntityTypeBuilder<DishType> builder)
        {
            builder.ToTable("DishType", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValue(1).ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
