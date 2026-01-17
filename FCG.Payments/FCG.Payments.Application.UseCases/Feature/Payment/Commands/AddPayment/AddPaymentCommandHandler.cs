using FCG.Payments.Application.Interface.Repository.Base;
using FCG.Payments.Domain.Extensions;
using FCG.Shared.Contracts;
using MassTransit;
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
        private readonly IBus _bus;

        public AddPaymentCommandHandler(IPaymentRepository paymentRepository, IBus bus)
        {
            _paymentRepository = paymentRepository;
            _bus = bus;
        }

        public async Task<bool> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var payment = await _paymentRepository.AddAsync(new Domain.Entities.Payment(request.UserId, request.GameId, request.MethodPayment, request.StatusPayment));

                await _bus.Publish(new PaymentProcessedEvent
                {
                    GameId = request.GameId,
                    UserId = request.UserId,
                    Price = request.Price,
                    PaymentMethod = request.StatusPayment.GetDescription(),
                    Name = request.Name,
                    Email = request.Email,
                    Game = request.Game 
                });
                                                                
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error no envio do email: {ex.Message}");
                return false;
            }
        }
    }
}
