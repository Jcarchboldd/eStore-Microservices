using Microsoft.EntityFrameworkCore;

namespace Coupon.Infrastructure
{
    public class CouponContext : DbContext
    {
        public CouponContext(DbContextOptions<CouponContext> options) : base(options)
        {
        }

        public DbSet<PromoCode> Coupons { get; set; }
    }
}

