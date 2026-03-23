namespace FCG.Payments.WebAPI.Configurations
{
    public static class LoggingConfig
    {
        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationInsightsTelemetry();
        }
    }
}
