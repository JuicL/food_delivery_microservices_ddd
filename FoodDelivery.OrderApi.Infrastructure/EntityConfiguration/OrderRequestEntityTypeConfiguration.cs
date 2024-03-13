using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate;
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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "dbo");

            builder.Ignore(r => r.DomainEvents);
            builder.HasKey(r => r.Id);
            builder.Property(x => x.UserName);
           
        }
    }
}
