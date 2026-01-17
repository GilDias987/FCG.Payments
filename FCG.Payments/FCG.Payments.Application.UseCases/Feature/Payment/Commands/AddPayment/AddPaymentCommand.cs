using FCG.Payments.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.UseCases.Feature.Payment.Commands.AddPayment
{
    public class AddPaymentCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int GameId { get; set; } 
        public decimal Price { get; set; }
        public MethodPaymentEnum MethodPayment { get; set; }
        public StatusPaymentEnum StatusPayment { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Game { get; set; }

    }

}
