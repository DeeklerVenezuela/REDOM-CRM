using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    //Tabla 
    public class Empleado
    {


        [Key]
        public int EmpleadoID { get; set; }

        [Required]
        [MaxLength(35)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Apellido 1")]
        public string Apellido1 { get; set; }

        [MaxLength(20)]
        [DisplayName("Apellido 2")]
        public string Apellido2 { get; set; }

        [Required]
        [MaxLength(15)]
        [DisplayName("Cédula")]
        public string Cedula { get; set; }
        public IEnumerable<Telefono> Telefonos { get; set; }
        public IEnumerable<Direccion> Direcciones { get; set; }
        [DisplayName("Cumpleaño")]
        public DateTime Cumpleanio { get; set; }


        [MaxLength(60)]
        public string Comentarios { get; set; }

        [Required]
        [MaxLength(15)]
        public string Status { get; set; }

        [Required]
        [DisplayName("Sueldo Base")]
        public int SueldoBase { get; set; }

        [Required]
        [DisplayName("% Membresía")]
        public int PComisionMembresia { get; set; }

        [Required]
        [DisplayName("% Reserva")]
        public int PComisionReserva { get; set; }

    }
}
