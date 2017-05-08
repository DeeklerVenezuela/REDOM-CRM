using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class EmailUsuario
    {
        [Key]
        public int EmailUsuarioID { get; set; }
        
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
