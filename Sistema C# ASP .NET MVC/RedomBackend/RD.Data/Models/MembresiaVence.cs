using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class MembresiaVence
    {
        [Key]
        public int MembresiaVenceID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public virtual Afiliado AfiliadoM { get; set; }
        public bool Called { get; set; }

    }
}
