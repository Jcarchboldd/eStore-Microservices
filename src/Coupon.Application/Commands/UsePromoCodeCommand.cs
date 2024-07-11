namespace Coupon.Application.Commands
{
    public class UsePromoCodeCommand : IRequest<bool>
    {
        public string Code { get; set; }

        public UsePromoCodeCommand(string code)
        {
            Code = code;
        }
    }
}