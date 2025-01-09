using ClassLib.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add Services in DI
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

// Configure HTTP Request Pipeline 
app.MapCarter();

app.MapGet("/", () => "Hello World!");

app.Run();
