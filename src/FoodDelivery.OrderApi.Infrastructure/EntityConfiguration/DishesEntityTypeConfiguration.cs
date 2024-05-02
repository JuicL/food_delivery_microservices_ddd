using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.OrderApi.Infrastructure.EntityConfiguration
{
    public class DishesEntityTypeConfiguration : IEntityTypeConfiguration<Dishes>
    {
        public void Configure(EntityTypeBuilder<Dishes> builder)
        {
            builder.ToTable("Dishes", "dbo");
            
            builder.Ignore(r => r.DomainEvents);
            builder.HasKey(r => r.Id);
            builder.Property(x => x.DishId);
            builder.Property(x => x.Name);
            builder.OwnsOne(e => e.Weight, b => { b.Property(x => x.Grams); });
            builder.OwnsOne(e => e.Price, b => { b.Property(x => x.Amount); });
            builder.Property(x => x.Units);
        }
    }
}
