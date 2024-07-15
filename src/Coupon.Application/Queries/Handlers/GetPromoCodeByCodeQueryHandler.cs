namespace Coupon.Application.Queries.Handlers
{
    public class GetPromoCodeByCodeQueryHandler : IRequestHandler<GetPromoCodeByCodeQuery, PromoCode?>, Abstractions.IHandler
    {
        private readonly IPromoCodeRepository _promoCodeRepository;

        public GetPromoCodeByCodeQueryHandler(IPromoCodeRepository promoCodeRepository)
        {
            _promoCodeRepository = promoCodeRepository;
        }

        public async Task<PromoCode?> Handle(GetPromoCodeByCodeQuery request, CancellationToken cancellationToken)
        {
            return await _promoCodeRepository.GetByCodeAsync(request.Code);
        }
    }
}
