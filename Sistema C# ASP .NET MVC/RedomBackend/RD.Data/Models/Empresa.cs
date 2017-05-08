using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Empresa
    {
        [Key]
        public int EmpresaID { get; set; }

        [MaxLength(130)]
        public string Nombre { get; set; }

        [MaxLength(130)]
        public string IDFiscal { get; set; }
      
        [MaxLength(160)]
        public string Direccion { get; set; }

        [MaxLength(45)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Telefono { get; set; }

        [MaxLength(200)]
        public string Website { get; set; }

        public int MesesAdicionales { get; set; }

        public int TarjetasAdicionales { get; set; }

        public int MontoDocumentacion { get; set; }

        public int MontoReserva { get; set; }

        public int TasaDeCambio { get; set; }
    }
}
