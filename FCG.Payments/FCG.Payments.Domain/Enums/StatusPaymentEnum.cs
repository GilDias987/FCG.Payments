using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FCG.Payments.Domain.Enums
{
    public enum StatusPaymentEnum
    {
        [Description("Autorizado")]
        Authorized,
        [Description("Não Autorizado")]
        Unauthorized
    }
}
