var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomMiddleware();

// Seed the database.
app.SeedDatabase();

// Map the coupon API routes
app.MapCouponApiV1();

app.Run();