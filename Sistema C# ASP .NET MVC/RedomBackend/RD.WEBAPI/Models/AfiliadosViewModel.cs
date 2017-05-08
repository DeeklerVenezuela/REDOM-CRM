using RD.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.WEBAPI.Models
{
    public class AfiliadosViewModel
    {
        [Key]
        public int ID { get; set; }
        public Afiliado Afiliado {get; set;}
        public List<Telefono> Telefonos { get; set; }
        public List<Direccion> Direcciones { get; set; }
        public List<TarjetaUsuario> Tarjetas { get; set; }
        public List<EmailUsuario> Emails { get; set; }
        public List<TarjetaAdicional> TarjetasAdicionales { get; set; }

    }
}
