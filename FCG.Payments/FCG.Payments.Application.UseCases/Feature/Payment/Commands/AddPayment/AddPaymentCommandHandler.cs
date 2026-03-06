using FCG.Payments.Application.Interface.Repository.Base;
using FCG.Payments.Domain.Extensions;
using FCG.Shared.Contracts;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace FCG.Payments.Application.UseCases.Feature.Payment.Commands.AddPayment
{
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public AddPaymentCommandHandler(IPaymentRepository paymentRepository, ISendEndpointProvider sendEndpointProvider)
        {
            _paymentRepository = paymentRepository;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<bool> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var payment = await _paymentRepository.AddAsync(new Domain.Entities.Payment(request.UserId, request.GameId, request.MethodPayment, request.StatusPayment));
                var paymentProcessedEvent = new PaymentProcessedEvent
                {
                    GameId = request.GameId,
                    UserId = request.UserId,
                    Price = request.Price,
                    PaymentMethod = request.StatusPayment.GetDescription(),
                    Name = request.Name,
                    Email = request.Email,
                    Game = request.Game
                };

                await CreateQueuePaymentProcess(paymentProcessedEvent, "queue:payment-process-catalog-queue");
                await CreateQueuePaymentProcess(paymentProcessedEvent, "queue:payment-process-notification-queue");

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error no envio do email: {ex.Message}");
                return false;
            }
        }

        private async Task CreateQueuePaymentProcess(PaymentProcessedEvent paymentProcessedEvent, string queue)
        {
            try
            {
                var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(queue));
                await endpoint.Send(paymentProcessedEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error no envio do email: {ex.Message}");
            }
        }
    }
}
