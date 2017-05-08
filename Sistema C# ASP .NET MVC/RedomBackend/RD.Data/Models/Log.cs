using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }
        [Required]
        public Empleado Empleado { get; set; }
        [Required]
        public DateTime FechaHora { get; set; }
        [Required]
        [MaxLength(15)]
        public string Direccion { get; set; }
        [Required]
        [MaxLength(45)]
        public string Tipo { get; set; }
        
    }
}
