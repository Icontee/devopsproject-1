var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Ok(new
{
    message = "Welcome to my CI/CD Demo!",
    version = "1.0.0",
    status = "running"
}))
.WithName("GetHome")
.WithOpenApi();

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    timestamp = DateTime.UtcNow
}))
.WithName("GetHealth")
.WithOpenApi();

app.MapGet("/api/info", () => Results.Ok(new
{
    app = "DevOps Interview Demo",
    environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
    features = new[]
    {
        "Automated CI/CD",
        "Docker containerization",
        "Health checks",
        "GitHub Actions integration",
        "Swagger/OpenAPI documentation"
    }
}))
.WithName("GetInfo")
.WithOpenApi();

app.Run();
