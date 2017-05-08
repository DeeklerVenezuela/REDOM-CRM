using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Tarjeta
    {
        [Key]
        public int TarjetaID { get; set; }
        [MaxLength(20)]
        public string Tipo { get; set; }
        [MaxLength(30)]
        public string Nombre { get; set; }
        public List<Banco> Bancos { get; set; }
    }
}
