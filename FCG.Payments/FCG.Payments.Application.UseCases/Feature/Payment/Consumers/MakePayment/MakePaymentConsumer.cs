using FCG.Payments.Application.UseCases.Feature.Payment.Commands.AddPayment;
using FCG.Payments.Domain.Enums;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FCG.Payments.Application.UseCases.Feature.Payment.Consumers.MakePayment
{
    public class MakePaymentConsumer : IConsumer<FCG.Shared.Contracts.OrderPlacedEvent>
    {
        private readonly IMediator _mediator;
        public MakePaymentConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Consume(ConsumeContext<FCG.Shared.Contracts.OrderPlacedEvent> context)
        {

            FCG.Payments.Domain.Enums.MethodPaymentEnum typePayment = (MethodPaymentEnum)char.Parse(context.Message.PaymentMethod);

            return _mediator.Send(new AddPaymentCommand
            {
                GameId = context.Message.GameId,
                UserId = context.Message.UserId,
                Price = context.Message.Price.Value,
                MethodPayment = typePayment,
                StatusPayment = FCG.Payments.Domain.Enums.StatusPaymentEnum.Approved,
                Game = context.Message.Game,    
                Name = context.Message.Name,
                Email = context.Message.Email
            });
        }
    }
}
