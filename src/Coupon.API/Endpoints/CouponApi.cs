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
            if (result)
            {
                return Results.Ok(new ApiResponse<bool>(true, "Coupon created successfully", result));
            }
            return Results.BadRequest(new ApiResponse<bool>(false, "Failed to create coupon", result, ["Creation failed due to invalid data"]));
        }

        private static async Task<IResult> UseCouponAsync(
            string code,
            UsePromoCodeCommand command,
            IMediator mediator)
        {
            command.Code = code;
            var result = await mediator.Send(command);
            if (result)
            {
                return Results.Ok(new ApiResponse<bool>(true, "Coupon used successfully", result));
            }
            var errorResponse = new ApiResponse<bool>(false, "Failed to use coupon", result, ["Usage failed due to invalid code"]);
            return Results.Problem(detail: JsonSerializer.Serialize(errorResponse), statusCode: 500);
        }

        private static async Task<IResult> GetCouponByCodeAsync(
            string code, 
            IMediator mediator)
        {
            var coupon = await mediator.Send(new GetPromoCodeByCodeQuery(code));
            if (coupon != null)
            {
                var couponDto = new PromoCodeDTO
                {
                    Code = coupon.Code,
                    Discount = coupon.Discount,
                    Quantity = coupon.Quantity,
                    ExpirationDate = coupon.ExpirationDate,
                    Active = coupon.Active
                };
                return Results.Ok(new ApiResponse<PromoCodeDTO>(true, "Coupon retrieved successfully", couponDto));
            }
            var notFoundResponse = new ApiResponse<PromoCodeDTO>(false, "Coupon not found", null, ["No coupon found with the given code"]);
            return Results.NotFound(JsonSerializer.Serialize(notFoundResponse));
        }
    }
}