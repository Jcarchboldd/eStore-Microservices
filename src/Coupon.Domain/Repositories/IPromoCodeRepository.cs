namespace Coupon.Domain.Repositories
{
    public interface IPromoCodeRepository
    {
        Task<PromoCode?> GetByCodeAsync(string code);
        Task AddAsync(PromoCode promoCode);
        Task UpdateAsync(PromoCode promoCode);
    }
}