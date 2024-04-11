# Tax assistant

## Migration

Run `dotnet ef migrations add <MigrationName>` in Blazor server application project directory, with `--project` parameter indicating project with database context and migrations

Eg:

```powershell
cd .\TaxAssistant.JPK\Server\
dotnet ef migrations add InitialDatabase --project ..\..\TaxAssistant.JPK.Database\TaxAssistant.JPK.Database.csproj --context DatabaseContext
```