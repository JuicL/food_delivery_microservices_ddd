using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Delivering.Infrastructure.EntityConfiguration
{
    public class AssignDeliveryEntityTypeConfiguration :
       IEntityTypeConfiguration<AssignDelivery>
    {
        public void Configure(EntityTypeBuilder<AssignDelivery> builder)
        {
            builder.ToTable("AssignDeliveries", "dbo");
            builder.Property(x=> x.Id).UseHiLo("deliveryseq");
            builder.Ignore(x => x.DomainEvents);
            builder.Property(x=> x.AssignDateTime);
            builder.OwnsOne(x => x.Status, p => { 
                p.Property(x => x.Id).HasColumnName("StatusId"); 
                p.Property(x => x.Name).HasColumnName("Status"); });
            builder.HasOne(x=> x.Courier).WithMany().HasForeignKey(x => x.CourierId);
            builder.HasOne(x=> x.Delivery).WithMany().HasForeignKey(x => x.DeliveryId);
        }
    }
}
