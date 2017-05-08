using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class UserPermision
    {
        //        Permissions is based in CRUD and RE (CRUD-RE)      //
        //        Create = 1 and 0
        //        Read = 1 and 0
        //        Update = 1 and 0
        //        Delete = 1 and 0
        //        Read Extended = 1 and 0

        [Key]
        public int UserPermisionID { get; set; }

        [MaxLength(8)]
        public string Afiliados { get; set; }

        [MaxLength(8)]
        public string Archivos { get; set; }

        [MaxLength(8)]
        public string Certificados { get; set; }

        [MaxLength(8)]
        public string Personas { get; set; }


        [MaxLength(8)]
        public string Hoteles { get; set; }

        [MaxLength(8)]
        public string Dummy { get; set; }

        [MaxLength(8)]
        public string Cobros { get; set; }

        [MaxLength(8)]
        public string Prospectos { get; set; }

        [MaxLength(8)]
        public string Reservas { get; set; }

        [MaxLength(8)]
        public string Permisos { get; set; }

        [MaxLength(8)]
        public string Documentacion { get; set; }

        [MaxLength(8)]
        public string Sistema { get; set; }

        [MaxLength(20)]
        public string Especiales { get; set; }

        public virtual Empleado Empleado { get; set; }
    }
}
