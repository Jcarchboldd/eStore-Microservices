namespace Coupon.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CouponContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            // Configure mediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(IHandlerApplication));
                cfg.RegisterServicesFromAssemblyContaining(typeof(IHandler));
            });

            services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Coupon API",
                    Description = "API for managing coupons"
                });

                options.OperationFilter<SwaggerDefaultValues>();
            });
            return services;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.OfType<OpenApiParameter>())
            {
                var description = apiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);

                if (description != null && parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                parameter.Required |= description?.IsRequired ?? false;
            }
        }
    }
}
