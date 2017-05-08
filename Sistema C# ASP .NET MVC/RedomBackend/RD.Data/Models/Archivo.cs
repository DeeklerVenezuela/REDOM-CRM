using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    //Tabla Archivo
    public class Archivo
    {
        [Key]
        public int ArchivoID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Eliminado { get; set; }
        public bool Status { get; set; }
        public byte[] Content { get; set; }
        public virtual List<Prospecto> Prospectos { get; set; }
    }
}
