//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using SHO_Task.Domain.Common;
//using SHO_Task.Domain.Items;
//using SHO_Task.Domain.ShippingOrders;

//namespace SHO_Task.Infrastructure.Configurations;

//internal sealed class ItemConfiguration : IEntityTypeConfiguration<ShippingOrderItem>
//{
//    public void Configure(EntityTypeBuilder<ShippingOrderItem> builder)
//    {
//        ConfigureItem(builder);
//    }

//    private static void ConfigureItem(EntityTypeBuilder<ShippingOrderItem> builder)
//    {
//        builder.ToTable("ShippingOrderItems");

//        builder.HasKey(i => i.Id);

//        builder.Property(i => i.Id)
//            .ValueGeneratedNever()
//            .HasConversion(
//                id => id.Value,
//                value => ItemId.Create(value));

//        // Remove Unique Index on ShippingOrderId to allow multiple items per PO
//        builder.HasIndex(l => l.ShippingOrderId);

//        // Soft delete query filter
//        builder.HasQueryFilter(q => !q.DeletedAt.HasValue);

//        // Relationship to ShippingOrder
//        builder.HasOne<ShippingOrder>()
//            .WithMany(po => po.ShippingOrderItems)
//            .HasForeignKey(i => i.ShippingOrderId);

//        // Map properties
//        builder.Property(l => l.GoodCode).IsRequired().HasMaxLength(50);
//        builder.Property(l => l.Quantity).HasColumnType("decimal(10,2)");
//        builder.Property(l => l.SerialNumber);
//        builder.Property(l => l.DeletedAt);

//        // Value object mapping for Price
//        builder.OwnsOne(
//            s => s.Price,
//            price =>
//            {
//                price.Property(m => m.Currency)
//                     .HasConversion(
//                         currency => currency.Code,
//                         code => Currency.FromCode(code))
//                     .HasMaxLength(3)
//                     .IsRequired();

//                price.Property(m => m.Amount)
//                     .HasColumnType("decimal(18,2)")
//                     .IsRequired();
//            });
//    }

//}
