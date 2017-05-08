using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class TipoComprobante
    {
        [Key]
        public int TipoComprobanteID { get; set; }
        
        [MaxLength(4)]
        public string Codigo { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}
