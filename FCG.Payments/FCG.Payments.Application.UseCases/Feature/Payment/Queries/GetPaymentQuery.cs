using FCG.Payments.Application.Dto.Playment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FCG.Payments.Application.UseCases.Feature.Payment.Queries
{
    public class GetPaymentQuery : IRequest<PlaymentDto>
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
