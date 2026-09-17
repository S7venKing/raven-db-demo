using Raven.Client.Documents;

namespace RavenDbDemo.Infrastructure;

public static class RavenDbContext
{
    public static IDocumentStore CreateStore(
        IConfiguration configuration)
    {
        var urls = configuration
            .GetSection("RavenDB:Urls")
            .Get<string[]>();

        var database = configuration["RavenDB:Database"];

        if (urls == null || urls.Length == 0)
        {
            throw new InvalidOperationException(
                "RavenDB URLs chưa được cấu hình.");
        }

        if (string.IsNullOrWhiteSpace(database))
        {
            throw new InvalidOperationException(
                "RavenDB Database chưa được cấu hình.");
        }

        var store = new DocumentStore
        {
            Urls = urls,
            Database = database.Trim()
        };

        store.Initialize();

        return store;
    }
}