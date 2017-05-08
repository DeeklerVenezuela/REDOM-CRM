using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class ScriptDummy
    {
        [Key]
        public int ScriptDummyID { get; set; }
        
        [DisplayName("Título")]
        [MaxLength(50)]
        public string Titulo { get; set; }

        [DisplayName("Descripción")]
        [MaxLength]
        [Column(TypeName = "ntext")]
        public string Descripcion { get; set; }

        [MaxLength(35)]
        public string Color { get; set; }
    }
}
