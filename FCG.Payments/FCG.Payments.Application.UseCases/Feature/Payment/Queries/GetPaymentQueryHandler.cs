using FCG.Payments.Application.Dto.Playment;
using FCG.Payments.Application.Interface.Repository.Base;
using FCG.Payments.Domain.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.UseCases.Feature.Payment.Queries
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PlaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PlaymentDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetPayment(request.UserId, request.GameId);

            if (payment is null)
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return new PlaymentDto {
                Id = payment.Id,
                UsuarioId = payment.UserId,
                GameId = payment.GameId,
                DatePlayment = payment.DateCreation.ToString("N2"),
                MethodPayment = payment.MethodPayment.GetDescription(),
                StatusPayment = payment.StatusPayment.GetDescription()
            };
        }
    }
}
