using FCG.Payments.Application.UseCases.Feature.Payment.Commands.AddPayment;
using FCG.Payments.Domain.Enums;
using MassTransit;
using MediatR;

namespace FCG.Payments.Application.UseCases.Feature.Payment.Consumers.MakePayment
{
    public class MakePaymentConsumer : IConsumer<FCG.Shared.Contracts.OrderPlacedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MakePaymentConsumer> _logger;

        public MakePaymentConsumer(IMediator mediator, ILogger<MakePaymentConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public Task Consume(ConsumeContext<FCG.Shared.Contracts.OrderPlacedEvent> context)
        {
            MethodPaymentEnum typePayment = (MethodPaymentEnum)char.Parse(context.Message.PaymentMethod);

            return _mediator.Send(new AddPaymentCommand
            {
                GameId = context.Message.GameId,
                UserId = context.Message.UserId,
                Price  = context.Message.Price!.Value,
                MethodPayment = typePayment,
                StatusPayment = StatusPaymentEnum.Approved,
                Game = context.Message.Game,    
                Name = context.Message.Name,
                Email = context.Message.Email
            });
        }
    }
}
