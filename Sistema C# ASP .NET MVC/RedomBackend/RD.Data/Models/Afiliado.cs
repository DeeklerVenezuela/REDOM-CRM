using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    //Tabla Afiliado
    public class Afiliado{
        
        [Key]
        public int AfiliadoID { get; set; }
        
        public int AfiliadoIndice { get; set; }

        [MaxLength(60)]
        public string Cedula { get; set; }
        
        [MaxLength(60)]
        public string Pasaporte { get; set; }
        
        [MaxLength(35)]
        public string Nombres { get; set; }
        
        [MaxLength(256)]
        public string Apellido1 { get; set; }
        
        [MaxLength(256)]
        public string Apellido2 { get; set; }
        
        [MaxLength(50)]
        public string RazonSocial { get; set; }
        
        [MaxLength(60)]
        public string RNC { get; set; }
        
        [MaxLength(30)]
        public string Email { get; set; }

        public DateTime Cumpleanio { get; set; }

        public bool MesesAdicionales { get; set; }

        public int MesAdicionalCan { get; set; }

        public bool TarjetaAdicional{ get; set; }

        public int TarjetaAdicionalCan { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public virtual Empleado UsuarioRegistro { get; set; } // Usuario que raliza el registro

        public DateTime FechaHora { get; set; }

        [MaxLength(255)]
        public string Observaciones { get; set; }

        public virtual Empleado UsuarioBienvenida { get; set; } //Usuario asignado para la bienvenida del nuevo afiliado

        [MaxLength(15)]
        public string Status { get; set; }

        public int CostoMembresia { get; set; }

        public virtual List<Telefono> Telefonos { get; set; }

        public virtual List<Direccion> Direcciones { get; set; }

        public virtual List<TarjetaUsuario> Tarjetas { get; set; }

        public virtual List<EmailUsuario> Emails { get; set; }

        public virtual List<TarjetaAdicional> TarjetasAdicionales { get; set; }

    }
}
