using FCG.Payments.Application.Dto.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.Interface.Service
{
    public interface IEmailService
    {
        public EmailMessageDto EmailMessage(string email, string name, string game, decimal? price);
    }
}
