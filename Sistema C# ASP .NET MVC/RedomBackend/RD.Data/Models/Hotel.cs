using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }
        [MaxLength(125)]
        public string Nombre { get; set; }
        public string Moneda { get; set; }
        public int Simple { get; set; }
        public int Doble { get; set; }
        public int Triple { get; set; }
        public int Cuadruple { get; set; }
        public int Ninio { get; set; }
        public int NinioGratis { get; set; }
        public DateTime ValidoDesde { get; set; }
        public DateTime ValidoHasta { get; set; }

    }
}
