using FCG.Payments.Application.UseCases.Feature.Payment.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Payments.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IMediator mediator, ILogger<PaymentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Obter Usuário
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet("Get/{userId}/{gameId}")]
        public async Task<IActionResult> GetPayment(int userId, int gameId)
        {
            _logger.LogInformation("Iniciando busca de pagamento para o Usuário {UserId} no Jogo {GameId}", userId, gameId);

            try
            {
                var payment = await _mediator.Send(new GetPaymentQuery { UserId = userId, GameId = gameId });
                if (payment == null)
                {
                    _logger.LogWarning("Pagamento não encontrado para o Usuário {UserId}", userId);
                    return NotFound();
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar consulta de pagamento do usuário {UserId}", userId);
                return StatusCode(500, "Erro interno ao processar a requisição.");
            }
        }
    }
}
