namespace Coupon.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new CouponContext(
                serviceProvider.GetRequiredService<DbContextOptions<CouponContext>>());

            if (!context.PromoCodes.Any())
            {
                context.PromoCodes.AddRange(
                    new PromoCode("DISCOUNT10", 10, 100, DateTime.UtcNow.AddMonths(1)),
                    new PromoCode("DISCOUNT20", 20, 50, DateTime.UtcNow.AddMonths(1)),
                    new PromoCode("DISCOUNT30", 30, 25, DateTime.UtcNow.AddMonths(1))
                );

                context.SaveChanges();
            }
        }
    }
}

