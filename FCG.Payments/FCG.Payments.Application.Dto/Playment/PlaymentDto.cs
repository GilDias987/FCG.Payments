using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.Dto.Playment
{
    public class PlaymentDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int GameId { get; set; }
        public string DatePlayment { get; set; }
        public string MethodPayment { get; set; }
        public string StatusPayment { get; set; }
    }
}
