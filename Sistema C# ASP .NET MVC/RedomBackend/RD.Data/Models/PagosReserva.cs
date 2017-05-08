using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class PagosReserva
    {
        [Key]
        public int PagoReservaID { get; set; }
        public int Monto { get; set; }
        public virtual Afiliado Afiliado { get; set; }
        public DateTime FechaHora { get; set; }
        public virtual TarjetaUsuario Tarjeta { get; set; }
        public virtual Reserva Reserva { get; set; }
        public virtual ComprobanteFiscal Comprobante { get; set; }

    }
}
