using API_with_DOCKER.DataBase;
using API_with_DOCKER.interfaces;
using API_with_DOCKER.services;
using Microsoft.EntityFrameworkCore;
using API_with_DOCKER.DesignsPatterns.Pricing;
var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProduct, ProductServices>();
// Pricing Strategies
builder.Services.AddScoped<NormalPricingStrategy>();
builder.Services.AddScoped<VipPricingStrategy>();
builder.Services.AddScoped<BlackFridayPricingStrategy>();

// Strategy Resolver
builder.Services.AddScoped<
    IPricingStrategyResolver,
    PricingStrategyResolver>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/health");
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    await db.Database.MigrateAsync();
}

// HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();