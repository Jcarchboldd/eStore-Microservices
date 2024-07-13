namespace Coupon.API.EndPoints
{
    public static class CouponApi
    {
        public static async Task<IResult> CreateCouponAsync(
            CreatePromoCodeCommand command,
            [AsParameters] CouponServices services)
        {
            var result = await services.Mediator.Send(command);
            return result ? Results.Ok() : Results.BadRequest("Failed to create coupon.");
        }

        public static async Task<IResult> UseCouponAsync(
            string code,
            UsePromoCodeCommand command,
            [AsParameters] CouponServices services)
        {
            var result = await services.Mediator.Send(command);
            return result ? Results.Ok() : Results.Problem(detail: "Failed to use coupon.", statusCode: 500);
        }

        public static async Task<IResult> GetCouponByCodeAsync(
            string code, 
            [AsParameters] CouponServices services)
        {
            var coupon = await services.Mediator.Send(new GetPromoCodeByCodeQuery(code));
            return coupon != null ? Results.Ok(coupon) : Results.NotFound();
        }
    }

    public class CouponServices
    {
        public IMediator Mediator { get; }
        public CouponServices(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
