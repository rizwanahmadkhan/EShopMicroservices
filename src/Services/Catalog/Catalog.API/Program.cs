var builder = WebApplication.CreateBuilder(args);

// DI - AddServices

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure Request pipeline - Use and Map methods

app.MapCarter();

app.Run();
