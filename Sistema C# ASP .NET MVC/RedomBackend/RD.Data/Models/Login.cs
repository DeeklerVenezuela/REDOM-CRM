using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; }

        [MaxLength(30)]//Longitud de caracteres para username
        public string UserName { get; set; }

        [MaxLength(60)]
        public string Password { get; set; }

        public virtual Rol Rol { get; set; }

        public virtual Empleado Empleado { get; set; }

    }
}
