using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Mapster global configuration
TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection(); // Enable gRPC reflection
builder.Services.AddDbContext<DiscountContext>(opts => { 
    opts.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

// Configure DB Migration
app.UseMigration();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MapGrpcReflectionService(); // Enable gRPC reflection endpoint

app.Run();
