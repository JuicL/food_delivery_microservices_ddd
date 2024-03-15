using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.Delivering.Infrastructure.EntityConfiguration
{
    public class CourierEntityTypeConfiguration :
        IEntityTypeConfiguration<Courier>
    {
        public void Configure(EntityTypeBuilder<Courier> builder)
        {
            builder.ToTable("Couriers", "dbo");
            builder.Property(o => o.Id)
               .UseHiLo("orderseq");
            builder.Ignore(r => r.DomainEvents);
            builder.Property(x => x.UserName);
            builder.OwnsOne(x => x.PhoneNumber, p => { p.Property(x => x.Number).HasColumnName("UserPhone"); });
            
            builder.OwnsOne(x => x.WorkAddress, p => {
                p.Property(x => x.Country);
                p.Property(x => x.City);
                p.Property(x => x.District);
            });

            builder.OwnsOne(e => e.WorkStatus, b => {
                b.Property(b => b.Id).HasColumnName("WorkStatusId");
                b.Property(b => b.Name).HasColumnName("WorkStatus");
            });
        }
    }
}
