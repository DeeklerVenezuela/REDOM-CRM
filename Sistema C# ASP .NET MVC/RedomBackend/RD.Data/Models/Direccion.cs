using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    //Tabla Direccion
    public class Direccion
    {
        [Key]
        public int DireccionID { get; set; }

        //Use it to select if is direction for "Prospecto" or "Empleado"
 
        [MaxLength(15)]
        public string Tipo { get; set; }

  
        [MaxLength(200)]
        public string Descripcion { get; set; }
    }
}
