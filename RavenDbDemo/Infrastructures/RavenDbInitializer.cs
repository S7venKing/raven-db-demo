using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace RavenDbDemo.Infrastructure;

public static class RavenDbInitializer
{
    public static void EnsureDatabaseExists(
        IDocumentStore store)
    {
        var database = store.Database;

        if (string.IsNullOrWhiteSpace(database))
            throw new InvalidOperationException(
                "RavenDB Database chưa được cấu hình.");

        var databaseRecord = store.Maintenance.Server
            .Send(new GetDatabaseRecordOperation(database));

        if (databaseRecord != null)
            return;

        store.Maintenance.Server.Send(
            new CreateDatabaseOperation(
                new DatabaseRecord(database)));

        Console.WriteLine(
            $"[RavenDB] Created database: {database}");
    }
}