using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add Services in DI
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure HTTP Request Pipeline 
app.MapCarter();

app.MapGet("/", () => "Hello World!");

app.Run();
