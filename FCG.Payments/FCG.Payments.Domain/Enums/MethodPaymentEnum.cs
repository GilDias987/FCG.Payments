using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FCG.Payments.Domain.Enums
{
    public enum MethodPaymentEnum
    {
        [Description("Cartão")]
        Card,
        [Description("Boleto")]
        Ticket,
        [Description("Pix")]
        Pix
    }
}
