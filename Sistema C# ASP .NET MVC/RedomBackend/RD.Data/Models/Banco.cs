using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Banco
    {
        [Key]
        public int BancoID { get; set; }

        [MaxLength(30)]
        public string Nombre { get; set; }

        [MaxLength(120)]
        public string Descripcion { get; set; }

    }
}
