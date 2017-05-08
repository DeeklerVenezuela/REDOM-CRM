using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class HabitacionReserva
    {
        [Key]
        public int HabitacionReservaID { get; set; }
        public string Tipo { get; set; }
        public string Cantidad { get; set; }
    }
}
