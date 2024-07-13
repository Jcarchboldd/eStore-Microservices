namespace Coupon.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapCouponApiV1(this IEndpointRouteBuilder endpoints)
        {
            var apiVersionSet = endpoints.NewApiVersionSet()
                                         .HasApiVersion(1.0)
                                         .Build();

            var couponsGroup = endpoints.MapGroup("/api/v1/coupons")
                                        .WithApiVersionSet(apiVersionSet)
                                        .HasApiVersion(1.0);

            couponsGroup.MapPost("/", async (CreatePromoCodeCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result ? Results.Ok() : Results.BadRequest("Failed to create coupon.");
            });

            couponsGroup.MapPut("/use/{code}", async (string code, UsePromoCodeCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result ? Results.Ok() : Results.Problem(detail: "Failed to use coupon.", statusCode: 500);
            });

            couponsGroup.MapGet("/{code}", async (string code, IMediator mediator) =>
            {
                var coupon = await mediator.Send(new GetPromoCodeByCodeQuery(code));
                return coupon != null ? Results.Ok(coupon) : Results.NotFound();
            });

            return endpoints;
        }
    }
}
