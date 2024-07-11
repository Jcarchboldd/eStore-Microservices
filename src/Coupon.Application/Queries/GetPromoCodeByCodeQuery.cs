namespace Coupon.Application.Queries
{
    public class GetPromoCodeByCodeQuery : IRequest<PromoCode?>
    {
        public string Code { get; set; }

        public GetPromoCodeByCodeQuery(string code)
        {
            Code = code;
        }
    }
}