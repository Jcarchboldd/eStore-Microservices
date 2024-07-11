namespace Coupon.Domain.interfaces
{
    public interface IUnitOfWork
    {
        IPromoCodeRepository PromoCodeRepository { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<bool> SaveEntitiesAsync();
    }
}