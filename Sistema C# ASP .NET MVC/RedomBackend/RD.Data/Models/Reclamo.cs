using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Reclamo
    {
        [Key]
        public int ReclamoID { get; set; }
        public Afiliado Afiliado { get; set; }

        [Required]
        [MaxLength(15)]
        public string Status { get; set; }

        [Required]
        [MaxLength(20)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(60)]
        public string Descripcion { get; set; }

        public Empleado Empleado { get; set; }// Empleado que procesa el reclamo
        
        [Required]
        public int TiempoDeProcesamiento { get; set; }

        [Required]
        [MaxLength(7)]
        public string UnidadDeTiempo { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

    }
}
