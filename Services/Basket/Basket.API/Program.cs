var builder = WebApplication.CreateBuilder(args);

// Add Services in DI
var app = builder.Build();

// Configure HTTP Request Pipeline 
app.MapGet("/", () => "Hello World!");

app.Run();
