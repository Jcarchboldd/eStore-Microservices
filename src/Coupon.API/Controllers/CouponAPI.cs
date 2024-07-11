namespace Coupon.API.Controllers
{
    public static class CouponApi
{
    public static void MapCouponApiV1(this IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.NewApiVersionSet()
                               .HasApiVersion(1.0)
                               .Build();

        var apiGroup = app.MapGroup("api/v{version:apiVersion}/coupons")
                          .WithApiVersionSet(apiVersionSet)
                          .HasApiVersion(1.0);

        apiGroup.MapPost("/", CreateCouponAsync);
        apiGroup.MapPut("/use/{code}", UseCouponAsync);
        apiGroup.MapGet("/{code}", GetCouponByCodeAsync);
    }

    public static async Task<IResult> CreateCouponAsync(
        [FromBody] CreatePromoCodeCommand command,
        [AsParameters] CouponServices services)
    {
        var result = await services.Mediator.Send(command);
        return result ? Results.Ok() : Results.BadRequest("Failed to create coupon.");
    }

    public static async Task<IResult> UseCouponAsync(
        string code,
        [FromBody] UsePromoCodeCommand command,
        [AsParameters] CouponServices services)
    {
        var result = await services.Mediator.Send(command);
        return result ? Results.Ok() : Results.Problem(detail: "Failed to use coupon.", statusCode: 500);
    }

    public static async Task<IResult> GetCouponByCodeAsync(string code, [AsParameters] CouponServices services)
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

