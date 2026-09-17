using Raven.Client.Documents;
using RavenDbDemo.Infrastructure;
using RavenDbDemo.Models;
using RavenDbDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// RavenDB
var store = RavenDbContext.CreateStore(
    builder.Configuration);


builder.Services.AddSingleton<IDocumentStore>(store);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseHttpsRedirection();

// Controllers
app.MapControllers();

// Health check
app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        message = "RavenDB Demo",
        status = "running"
    });
});

app.MapGet("/ready", async (IDocumentStore store) =>
{
    try
    {
        using var session = store.OpenAsyncSession();

        var count = await session
            .Query<Customer>()
            .CountAsync();

        return Results.Ok(new
        {
            RavenDB = "Connected",
            CustomerCount = count
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            title: "RavenDB connection failed",
            detail: ex.Message,
            statusCode: StatusCodes.Status503ServiceUnavailable);
    }
});

app.Run();