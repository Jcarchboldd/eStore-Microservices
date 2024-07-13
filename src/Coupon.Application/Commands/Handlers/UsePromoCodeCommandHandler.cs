namespace Coupon.Application.Commands.Handlers
{
    public class UsePromoCodeCommandHandler : IRequestHandler<UsePromoCodeCommand, bool>, IHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public UsePromoCodeCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<bool> Handle(UsePromoCodeCommand request, CancellationToken cancellationToken)
        {
            var promoCode = await _unitOfWork.PromoCodeRepository.GetByCodeAsync(request.Code);
            if (promoCode == null || !promoCode.Active)
            {
                return false;
            }

            promoCode.UsePromoCode();
            await _unitOfWork.PromoCodeRepository.UpdateAsync(promoCode);
            var result = await _unitOfWork.SaveEntitiesAsync();

            if (result)
            {
                await _mediator.Publish(new PromoCodeUsedEvent(promoCode), cancellationToken);
            }

            return result;
        }
    }
}
