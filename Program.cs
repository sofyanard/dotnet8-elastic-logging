using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;

// Set up configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Set up Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        IndexFormat = $"َapp-logs-{DateTimeOffset.Now.LocalDateTime:yyyy-MM}",
        AutoRegisterTemplate = true,
        OverwriteTemplate = true,
        TemplateName = "ESv7",
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
        TypeName = null,
        BatchAction = ElasticOpType.Create,
        ModifyConnectionSettings = conn =>
            conn.BasicAuthentication("elastic", "JynEP9RYl792*khVwnTi")
    })
    .CreateLogger();

try
{
    Log.Information("Application starting up");
    Console.WriteLine("Hello, World!");
    Log.Warning("Just a warning message");
    Log.Error("Just an error message");
    Log.Information("Application running");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

Console.WriteLine("Hello, World!");
