using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.EntityConfiguration
{
    public class DishEntityTypeConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dish", "dbo");
            builder.Ignore(r => r.DomainEvents);

            builder.HasKey(r => r.Id);
            builder.Property(e=> e.Name);
            builder.Property(e=> e.Ingredients);
            
            builder.OwnsOne(e => e.Weight, b => { b.Property(e => e.Gram).HasColumnName("Weight"); });
            builder.OwnsOne(e => e.Price, b => { b.Property(e => e.Amount).HasColumnName("Price"); });
            builder.HasOne(e => e.DishType).WithMany().HasForeignKey(x=> x.DishTypeId);
        }
    }
}
