using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FoodDelivery.Delivering.Infrastructure.EntityConfiguration
{
    public class DeliveryEntityTypeConfiguration :
        IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("Deliveries", "dbo");
            builder.Property(o => o.Id)
               .UseHiLo("orderseq");
            builder.Ignore(r => r.DomainEvents);

            builder.Property(x => x.OrderId);
            builder.Property(x => x.RecipientId);
            builder.Property(x => x.RecipientName);
            builder.OwnsOne(x => x.UserPhoneNumber, p => { p.Property(x => x.Number).HasColumnName("UserPhone"); });
            builder.OwnsOne(e => e.TotalWeight, b => { b.Property(x => x.Grams); });
            builder.OwnsOne(e => e.TotalPrice, b => { b.Property(x => x.Amount); });
            builder.OwnsOne(e => e.PaymentMethod, b => {
                b.Property(b => b.Name).HasColumnName("PaymentMethod");
            });
            builder.Property(x => x.SenderName);
            builder.OwnsOne(e => e.SenderAddress, b =>
            {
                b.Property(e => e.GetFullAddress()).HasColumnName("SenderAddress");
            });
            
            builder.OwnsOne(e => e.RecipientAddress, b =>
            {
                b.Property(e => e.GetFullAddress()).HasColumnName("RecipientAddress");
            });
            builder.Property(x => x.CourierId);
            builder.Property(x => x.StartDeliveryDateTime);
            builder.Property(x => x.DeliveredAt);
            builder.Property(x => x.Lateness);
            builder.Property(x => x.Description);

            builder.OwnsOne(e => e.DeliveryStatus, b => {
                b.Property(b => b.Id).HasColumnName("DeliveryStatusId");
                b.Property(b => b.Name).HasColumnName("DeliveryStatus");
            });

        }
    }
}
