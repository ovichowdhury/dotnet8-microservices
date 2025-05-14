using Discount.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add Services in DI
var assembly = typeof(Program).Assembly;


// Add MediatR for CQRS
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Add Fluent Validation for Data Validation
builder.Services.AddValidatorsFromAssembly(assembly);

// Add Carter for Minimal API Module
builder.Services.AddCarter();

// Add Marten for Database Access
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();

// register repository
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

// Register Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Register Grpc
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountGrpc"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});

// register exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Configure JSON Serialization Options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Add health check
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);


var app = builder.Build();

// Configure HTTP Request Pipeline 
app.MapCarter();

// Use Exception Handler
app.UseExceptionHandler(options => { });

// Use Health Check
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
