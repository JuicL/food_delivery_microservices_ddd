using Microsoft.Extensions.Hosting;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Serilog.Sinks.PostgreSQL;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Serilog.Events;


namespace FoodDelivery.ServiceDefaults
{
    public static class LoggingExtention
    {
        public static IHostApplicationBuilder AddSerilog(this IHostApplicationBuilder builder)
        {
            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                { "id", new IdAutoIncrementColumnWriter() },
                { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
                { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
            };
            var connectionString = builder.Configuration.GetSection("SerilogLogging:ConnectionString").Value;
            if (connectionString is not null && !connectionString.Contains(';'))
            {
                connectionString = builder.Configuration.GetConnectionString(connectionString);
            }
            if (connectionString is null)
            {
                throw new Exception("Connection string not exist");
            }
            var tableName = builder.Configuration.GetSection("SerilogLogging:TableName").Value;
            var schemaName = builder.Configuration.GetSection("SerilogLogging:SchemaName").Value;
            var restrictedToMinimumLevel = builder.Configuration.GetSection("SerilogLogging:RestrictedToMinimumLevel").Value;
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.PostgreSQL(connectionString: connectionString,
                tableName: tableName ?? "Logs",
                schemaName: schemaName ?? "",
                columnOptions: columnWriters,
                restrictedToMinimumLevel: GetLogEventLevel(restrictedToMinimumLevel),
                needAutoCreateTable: true)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            return builder;

        }
        private static LogEventLevel GetLogEventLevel(string? logeventLevel)
        {
            if (logeventLevel is null)
                return LogEventLevel.Verbose;
            return logeventLevel switch
            {
                nameof(LogEventLevel.Information) => LogEventLevel.Information,
                nameof(LogEventLevel.Debug) => LogEventLevel.Debug,
                nameof(LogEventLevel.Verbose) => LogEventLevel.Verbose,
                nameof(LogEventLevel.Error) => LogEventLevel.Error,
                nameof(LogEventLevel.Warning) => LogEventLevel.Warning,
                nameof(LogEventLevel.Fatal) => LogEventLevel.Fatal,
                _ => LogEventLevel.Verbose,
            };
        }
    }
}
