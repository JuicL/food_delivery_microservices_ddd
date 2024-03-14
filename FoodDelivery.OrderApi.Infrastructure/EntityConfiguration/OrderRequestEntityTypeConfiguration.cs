using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.OrderApi.Infrastructure.EntityConfiguration
{

    public class OrderRequestEntityTypeConfiguration : IEntityTypeConfiguration<OrderRequest>
    {
        public void Configure(EntityTypeBuilder<OrderRequest> builder)
        {
            builder.ToTable("OrderRequest","dbo");

            builder.Property(o => o.Id)
                .UseHiLo("orderseq");

            builder.Ignore(r => r.DomainEvents);
            builder.Property(x => x.UserName);
            builder.Property(x => x.UserId);
            builder.Property(x => x.BranchId);
            builder.Property(x => x.RestaurantName);
            builder.Property(x => x.Description);

            builder.Property(x => x.OrderTime);
            builder.OwnsOne(e => e.DeliveryAddress, b =>
            {
                b.Property(e => e.City);
                b.Property(e => e.Country);
                b.Property(e => e.Street);
                b.Property(e => e.Home);
            });
            
            builder.OwnsOne(e => e.RestaurantAddress, b =>
            {
                b.Property(e => e.City);
                b.Property(e => e.Country);
                b.Property(e => e.Street);
                b.Property(e => e.Home);
            });
            builder.OwnsOne(e => e.PaymentMethod, b => { 
                b.Property(b => b.Id).HasColumnName("PaymentMethodId");
                b.Property(b => b.Name).HasColumnName("PaymentMethod");
            });
            
            builder.OwnsOne(e => e.OrderStatus, b => {
                b.Property(b => b.Id).HasColumnName("OrderStatusId");
                b.Property(b => b.Name).HasColumnName("OrderStatus");
            });
            builder.HasMany(x => x.Dishes).WithOne();

            
        }
    }
}
