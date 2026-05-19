using FCG.Payments.Application.Dto.Playment;
using FCG.Payments.Application.Interface.Repository.Base;
using FCG.Payments.Application.Interface.Service;
using FCG.Payments.Domain.Extensions;
using MediatR;

namespace FCG.Payments.Application.UseCases.Feature.Payment.Queries
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PlaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "payments:get";

        public GetPaymentQueryHandler(IPaymentRepository paymentRepository, ICacheService cacheService)
        {
            _paymentRepository = paymentRepository;
            _cacheService = cacheService;
        }

        public async Task<PlaymentDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cacheService.GetAsync<PlaymentDto>(CacheKey);

            if (cached is not null && cached.Id != 0)
                return cached;

            var payment = await _paymentRepository.GetPayment(request.UserId, request.GameId);

            if (payment is null)
                throw new ArgumentException("Nenhum registro encontrado.");

            return new PlaymentDto 
            {
                Id            = payment.Id,
                UsuarioId     = payment.UserId,
                GameId        = payment.GameId,
                DatePlayment  = payment.DateCreation.ToString("N2"),
                MethodPayment = payment.MethodPayment.GetDescription(),
                StatusPayment = payment.StatusPayment.GetDescription()
            };
        }
    }
}
