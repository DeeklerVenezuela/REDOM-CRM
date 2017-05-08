using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class UserEstado
    {
        [Key]
        public int UserEstadoID { get; set; }
        
        [MaxLength(30)]
        public string Estado { get; set; }
    }
}
