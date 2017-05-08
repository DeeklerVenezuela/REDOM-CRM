using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Pago
    {
        [Key]
        public int PagoID { get; set; }
        public virtual Afiliado Afiliado { get; set; }
        public virtual Empleado Empleado{ get; set; }
        public DateTime FechaHora { get; set; }
        public int CostoMembresia { get; set; }
        public int MontoPago { get; set; }
        public virtual TarjetaUsuario TarjetaUsuario { get; set; }
        [MaxLength(60)]
        public string Comentario { get; set; }
        public virtual ComprobanteFiscal Comprobante { get; set; }
    }
}
