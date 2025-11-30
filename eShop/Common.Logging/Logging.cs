using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Common.Logging;

public static class Logging
{
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (context, loggerConfiguration) =>
    {
        var env = context.HostingEnvironment;
        loggerConfiguration.MinimumLevel.Information()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApplicationName", env.ApplicationName)
        .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
        .Enrich.WithExceptionDetails()
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
        .WriteTo.Console();

        if (context.HostingEnvironment.IsDevelopment())
        {
            loggerConfiguration.MinimumLevel.Override("Catalog.API", LogEventLevel.Debug);
            loggerConfiguration.MinimumLevel.Override("Basket.API", LogEventLevel.Debug);
            loggerConfiguration.MinimumLevel.Override("Discount.API", LogEventLevel.Debug);
            loggerConfiguration.MinimumLevel.Override("Ordering.API", LogEventLevel.Debug);
            loggerConfiguration.MinimumLevel.Override("Payment.API", LogEventLevel.Debug);
            loggerConfiguration.MinimumLevel.Override("Identity.API", LogEventLevel.Debug);
        }

        //Elastic Search
        var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
        if (!string.IsNullOrEmpty(elasticUrl))
        {
            loggerConfiguration.WriteTo.Elasticsearch( new ElasticsearchSinkOptions(new Uri(elasticUrl))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                    IndexFormat = "eShop-Logs-{0:yyyy.MM.dd}",
                    MinimumLogEventLevel = LogEventLevel.Debug
                }
            );
        }
    };
}
