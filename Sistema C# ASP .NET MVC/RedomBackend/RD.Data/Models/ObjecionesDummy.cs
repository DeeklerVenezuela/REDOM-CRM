using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class ObjecionesDummy
    {
        [Key]
        public int ObjecionesDummyID { get; set; }
        [MaxLength(50)]
        public string Titulo { get; set; }

        [MaxLength]
        [Column(TypeName = "ntext")]
        public string Descripcion { get; set; }

        [MaxLength(35)]
        public string Color { get; set; }
    }
}
