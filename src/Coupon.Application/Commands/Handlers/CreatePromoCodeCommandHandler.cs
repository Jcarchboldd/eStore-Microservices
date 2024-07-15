namespace Coupon.Application.Commands.Handlers
{
    public class CreatePromoCodeCommandHandler : IRequestHandler<CreatePromoCodeCommand, bool>, Abstractions.IHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePromoCodeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreatePromoCodeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var promoCode = new PromoCode(request.Code, request.Discount, request.Quantity, request.ExpirationDate);
                await _unitOfWork.PromoCodeRepository.AddAsync(promoCode);
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