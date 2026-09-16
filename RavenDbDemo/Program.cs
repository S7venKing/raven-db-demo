using Raven.Client.Documents;
using RavenDbDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var store = RavenDbContext.CreateStore(
    builder.Configuration);

builder.Services.AddSingleton<IDocumentStore>(store);

var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        message = "RavenDB Demo",
        status = "running"
    });
});

app.MapGet("/test", async (IDocumentStore store) =>
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
});

app.MapPost("/customers", async (
    Customer customer,
    IDocumentStore store) =>
{
    using var session = store.OpenAsyncSession();

    await session.StoreAsync(customer);

    await session.SaveChangesAsync();

    return Results.Ok(customer);
});

app.Run();


public class Customer
{
    public string? Id { get; set; }

    public string Name { get; set; } = "";

    public string Email { get; set; } = "";
}