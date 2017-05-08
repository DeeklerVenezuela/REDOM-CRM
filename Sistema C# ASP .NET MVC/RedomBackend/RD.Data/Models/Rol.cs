using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Rol
    {
        [Key]
        public int RolID { get; set; }
        [MaxLength(150)]
        public string Nombre { get; set; }
        public virtual UserPermision Permisos { get; set; }
        public DateTime FechaHora { get; set; }
        public Empleado Creador { get; set; }
    }
}
