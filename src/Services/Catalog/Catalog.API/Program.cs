var builder = WebApplication.CreateBuilder(args);

// DI - AddServices

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(options => 
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure Request pipeline - Use and Map methods

app.MapCarter();

app.Run();
