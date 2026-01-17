using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FCG.Payments.Domain.Enums
{
    public enum StatusPaymentEnum
    {
        [Description("Approved")]
        Approved = '0',
        [Description("Rejected")]
        Rejected = '1'
    }
}
