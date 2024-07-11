namespace Coupon.Infrastructure
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly CouponContext _context;

        public PromoCodeRepository(CouponContext context)
        {
            _context = context;
        }

        public async Task<PromoCode?> GetByCodeAsync(string code)
        {
            return await _context.Coupons.FirstOrDefaultAsync(pc => pc.Code == code);
        }

        public async Task AddAsync(PromoCode promoCode)
        {
            await _context.Coupons.AddAsync(promoCode);
        }

        public async Task UpdateAsync(PromoCode promoCode)
        {
            _context.Coupons.Update(promoCode);
            await Task.CompletedTask;
        }
    }
}
