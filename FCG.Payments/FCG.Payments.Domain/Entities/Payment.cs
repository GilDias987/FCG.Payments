using FCG.Payments.Domain.Common.Exception;
using FCG.Payments.Domain.Common.Validations;
using FCG.Payments.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FCG.Payments.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Payment()
        {

        }

        public Payment(int userId, int gameId, MethodPaymentEnum methodPayment, StatusPaymentEnum statusPayment)
        {
            Initialize(userId, gameId, methodPayment, statusPayment);
        }

        public void Initialize(int userId, int gameId, MethodPaymentEnum methodPayment, StatusPaymentEnum statusPayment)
        {
            Guard.AgainstEmptyId(userId, "Usuario Id");
            Guard.AgainstEmptyId(gameId, "Game Id");

            UserId = userId;
            GameId = gameId;
            MethodPayment = methodPayment;
            StatusPayment = statusPayment;
        }


        #region Propriedades Base
        public int UserId { get; private set; }
        public int GameId { get; private set; }
        public MethodPaymentEnum MethodPayment { get; private set; }
        public StatusPaymentEnum StatusPayment { get; private set; }
        #endregion
    }
}
