namespace Coupon.Application.Commands.Handlers
{
    public class CreatePromoCodeCommandHandler : IRequestHandler<CreatePromoCodeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePromoCodeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreatePromoCodeCommand request, CancellationToken cancellationToken)
        {
            var promoCode = new PromoCode(request.Code, request.Discount, request.Quantity, request.ExpirationDate);
            await _unitOfWork.PromoCodeRepository.AddAsync(promoCode);
            return await _unitOfWork.SaveEntitiesAsync();
        }
    }
}