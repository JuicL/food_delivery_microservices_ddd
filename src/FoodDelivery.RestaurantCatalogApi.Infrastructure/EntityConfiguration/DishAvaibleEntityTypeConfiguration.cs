using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.EntityConfiguration
{
    public class DishAvaibleEntityTypeConfiguration : IEntityTypeConfiguration<DishAvaible>
    {
        public void Configure(EntityTypeBuilder<DishAvaible> builder)
        {
            builder.ToTable("DishAvaibles", "dbo");
            builder.Ignore(r => r.DomainEvents);

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id);
            
            //builder.HasOne(e => e.Status).WithMany().HasForeignKey("DishAvaibleStatusId");
            
            builder.HasOne(e=> e.Dish).WithMany().HasForeignKey("DishId");

            builder.HasOne(e=> e.Branch).WithMany();
            builder.HasIndex(e=> new {e.DishId,e.BranchId}).IsUnique(true);
            //builder.OwnsOne(e => e.Status, b =>
            //{
            //    b.Property(e => e.Id).HasColumnName("StatusId");
            //    b.Property(e => e.Name).HasColumnName("Status");
            //});

        }
    }
}
