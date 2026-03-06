using FCG.Payments.Application.UseCases.Behavirour;
using FCG.Payments.Application.UseCases.Feature.Payment.Consumers.MakePayment;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FCG.Payments.Application.UseCases.Registration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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

            return services;
        }
    }
}
