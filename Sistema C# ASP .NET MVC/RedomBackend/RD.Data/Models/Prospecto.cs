using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Prospecto
    {
        [Key]
        public int ProspectoID { get; set; }
        [MaxLength(35)]
        public string Nombres { get; set; }
        [MaxLength(20)]
        public string Apellido1 { get; set; }
        [MaxLength(20)]
        public string Apellido2 { get; set; }
        [MaxLength(30)]
        public string Cedula { get; set; }
        public virtual Empleado Empleado { get; set; }
        [MaxLength(255)]
        public string Respuesta { get; set; }
        [MaxLength(30)]
        public string Fax { get; set; }
        public DateTime FechaHora { get; set; }
        [MaxLength(255)]
        public string Comentario { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        public bool Activo { get; set; }
        public bool Ocupado { get; set; }
        public bool Trabajado { get; set; }
        public bool Afiliado { get; set; }
        public virtual List<Direccion> Direcciones { get; set; }
        public virtual List<Telefono> Telefonos { get; set; }
        public virtual Archivo Archivo { get; set; }
    }
}
