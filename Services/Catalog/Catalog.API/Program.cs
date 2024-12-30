

var builder = WebApplication.CreateBuilder(args);

// Add Services in DI
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

// Configure HTTP Request Pipeline 
app.MapCarter();

app.MapGet("/", () => "Hello World!");

app.Run();
