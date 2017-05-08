using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Membresia
    {
        [Key]
        public int MembresiaID { get; set; }

        public int Costo { get; set; }

        public int Duracion { get; set; }

        public DateTime FechaCreacion { get; set; }
         
        [MaxLength(45)]
        public string Nombre { get; set; }

    }
}
