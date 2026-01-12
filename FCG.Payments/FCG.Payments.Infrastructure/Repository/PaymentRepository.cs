using FCG.Payments.Application.Interface.Repository.Base;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Infrastructure.Context;
using FCG.Payments.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Repository
{

    public class PaymentRepository : EFRepository<Playment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Playment?> GetPayment(int userId, int gameId)
        {
            return await _dbSet.Where(x => x.UserId == userId && x.GameId == gameId).FirstOrDefaultAsync();
        }

    }
}
