using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class TarjetaUsuario
    {
        public int TarjetaUsuarioID { get; set; }
        [MaxLength(30)]
        public string Banco { get; set; }
        [MaxLength(30)]
        public string Tipo { get; set; }
        [MaxLength(180)]
        public string NumeroTarjeta { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
