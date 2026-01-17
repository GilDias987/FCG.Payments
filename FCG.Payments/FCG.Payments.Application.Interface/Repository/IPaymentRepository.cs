using FCG.Payments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.Interface.Repository.Base
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment?> GetPayment(int userId, int gameId);
    }
}
