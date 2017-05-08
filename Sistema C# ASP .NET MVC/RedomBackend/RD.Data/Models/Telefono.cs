using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Telefono
    {
        [Key]
        public int TelefonoID { get; set; }
        //Use to if is direction for "Prospecto" or "Empleado"
        [MaxLength(15)]
        public string Tipo { get; set; }
        [MaxLength(45)]
        public string Descripcion { get; set; }

        [MaxLength(6)]
        public string Extension { get; set; }
    }
}
