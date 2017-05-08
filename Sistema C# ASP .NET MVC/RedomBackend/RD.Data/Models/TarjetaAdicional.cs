using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class TarjetaAdicional
    {
        [Key]
        public int TarjetaAdicionalID { get; set; }
        [MaxLength(60)]
        public string Cedula { get; set; }
        [MaxLength(30)]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public string Cargo { get; set; }
        [MaxLength(30)]
        public string Telefono1 { get; set; }
        [MaxLength(30)]
        public string Telefono2 { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(60)]
        public string Direccion { get; set; }

    }
}
