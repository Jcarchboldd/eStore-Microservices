namespace Coupon.Infrastructure.Data.EntityConfigurations
{
    public class PromoCodeEntityTypeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.ToTable("PromoCodes");
            
            builder.HasKey(pc => pc.Code);

            builder.Property(pc => pc.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pc => pc.Discount)
                .IsRequired();

            builder.Property(pc => pc.Quantity)
                .IsRequired();

            builder.Property(pc => pc.ExpirationDate)
                .IsRequired();

            builder.Property(pc => pc.Active)
                .IsRequired();
        }
    }
}
