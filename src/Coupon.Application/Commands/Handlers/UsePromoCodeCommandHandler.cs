namespace Coupon.Application.Commands.Handlers
{
    public class UsePromoCodeCommandHandler : IRequestHandler<UsePromoCodeCommand, bool>, Abstractions.IHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsePromoCodeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UsePromoCodeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var promoCode = await _unitOfWork.PromoCodeRepository.GetByCodeAsync(request.Code);
                if (promoCode == null) return false;

                promoCode.UsePromoCode();
                await _unitOfWork.PromoCodeRepository.UpdateAsync(promoCode);
                await _unitOfWork.SaveEntitiesAsync();
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
    }
}
