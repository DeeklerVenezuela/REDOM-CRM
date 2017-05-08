using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RD.Data.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorID { get; set; }
    }
}
