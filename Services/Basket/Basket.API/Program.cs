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

var app = builder.Build();

// Configure HTTP Request Pipeline 
app.MapCarter();

app.MapGet("/", () => "Hello World!");

app.Run();
