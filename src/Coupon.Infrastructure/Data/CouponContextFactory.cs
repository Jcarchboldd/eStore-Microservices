namespace Coupon.Infrastructure.Data
{
    public class CouponContextFactory : IDesignTimeDbContextFactory<CouponContext>
    {
        public CouponContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CouponContext>();

            // Get environment setting to determine which configuration file to use
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var appsettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "../Coupon.API");

            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(appsettingsPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);

            return new CouponContext(optionsBuilder.Options);
        }
    }
}
