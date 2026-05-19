using FCG.Payments.Application.Interface.Service;
using FCG.Payments.Application.UseCases.Behavirour;
using FCG.Payments.Application.UseCases.Feature.Payment.Consumers.MakePayment;
using FCG.Payments.Application.UseCases.Service;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace FCG.Payments.Application.UseCases.Registration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<ICacheService, CacheService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<MakePaymentConsumer>();

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(configuration["ServiceBus:ConnectionString"]);

                    cfg.ReceiveEndpoint("payment-create-queue", e =>
                    {
                        // não criar topology automática (evita topics)
                        e.ConfigureConsumeTopology = false;

                        // evita propriedades não suportadas
                        e.RemoveSubscriptions = true;

                        e.ConfigureConsumer<MakePaymentConsumer>(context);
                    });
                });
            });


            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configurationRedis = configuration["Redis:ConnectionString"];

                var options = ConfigurationOptions.Parse(configurationRedis);

                options.ConnectTimeout = 30000;
                options.SyncTimeout = 30000;
                options.AbortOnConnectFail = false;

                options.Ssl = true;
                options.ReconnectRetryPolicy = new ExponentialRetry(5000);

                return ConnectionMultiplexer.Connect(options);
            });


            return services;
        }
    }
}
