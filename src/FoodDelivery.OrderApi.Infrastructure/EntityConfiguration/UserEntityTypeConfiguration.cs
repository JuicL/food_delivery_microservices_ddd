using FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.OrderApi.Infrastructure.EntityConfiguration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "dbo");

            builder.Ignore(r => r.DomainEvents);
            builder.HasKey(r => r.Id);
            builder.Property(x => x.UserName);
            builder.OwnsOne(x => x.Phone, p => { p.Property(x => x.Number).HasColumnName("Phone"); });
           
        }
    }
}
