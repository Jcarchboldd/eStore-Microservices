namespace Coupon.API.Endpoints
{
    public static class CouponApi
    {
        public static IEndpointRouteBuilder MapCouponApiV1(this IEndpointRouteBuilder endpoints)
        {
            var apiVersionSet = endpoints.NewApiVersionSet()
                                         .HasApiVersion(1.0)
                                         .Build();

            var couponsGroup = endpoints.MapGroup("/api/v1/coupons")
                                        .WithApiVersionSet(apiVersionSet)
                                        .HasApiVersion(1.0);

            couponsGroup.MapPost("/", CreateCouponAsync);
            couponsGroup.MapPut("/use/{code}", UseCouponAsync);
            couponsGroup.MapGet("/{code}", GetCouponByCodeAsync);

            return endpoints;
        }

        private static async Task<IResult> CreateCouponAsync(
            CreatePromoCodeCommand command,
            IMediator mediator)
        {
            var result = await mediator.Send(command);
            return result ? Results.Ok() : Results.BadRequest("Failed to create coupon.");
        }

        private static async Task<IResult> UseCouponAsync(
            string code,
            UsePromoCodeCommand command,
            IMediator mediator)
        {
            command.Code = code;
            var result = await mediator.Send(command);
            return result ? Results.Ok() : Results.Problem(detail: "Failed to use coupon.", statusCode: 500);
        }

        private static async Task<IResult> GetCouponByCodeAsync(
            string code, 
            IMediator mediator)
        {
            var coupon = await mediator.Send(new GetPromoCodeByCodeQuery(code));
            return coupon != null ? Results.Ok(coupon) : Results.NotFound();
        }
    }
}