using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class ComprobanteFiscal
    {
        [Key]
        public int ComprbanteFiscalID { get; set; }

        [MaxLength(30)]
        public string Secuencia { get; set; } //Se genera en el módulo de generacion de comprobantes a partir de la tabla NCF

        public DateTime FechaCreacion { get; set; } //Fecha en que se generó el comprobante

        public int Tipo { get; set; }
    }
}
