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

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
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
            var payment = await _mediator.Send(new GetPaymentQuery { UserId = userId, GameId = gameId });
            return Ok(payment);
        }
    }
}
