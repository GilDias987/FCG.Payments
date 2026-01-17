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

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["Rabbitmq:Url"], "/", h =>
                    {
                        h.Username(configuration["Rabbitmq:Username"]);
                        h.Password(configuration["Rabbitmq:Password"]);
                    });

                    cfg.ReceiveEndpoint("payment-create-queue", e =>
                    {
                        e.ConfigureConsumer<MakePaymentConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
