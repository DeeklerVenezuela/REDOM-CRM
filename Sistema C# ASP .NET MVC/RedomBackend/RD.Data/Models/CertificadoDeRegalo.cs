using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class CertificadoDeRegalo
    {
        [Key]
        public int CertificadoDeRegaloID { get; set; }
        [MaxLength(60)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Tipo { get; set; }
        public int Cantidad { get; set; }
        public int Descuento { get; set; }
        public bool Status { get; set; }
    }
}
