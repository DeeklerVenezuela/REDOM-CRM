using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class ControlDummy
    {
        [Key]
        public int ControlDummyID { get; set; }
        public virtual Prospecto Prospecto { get; set; }
        public DateTime FechaHora { get; set; }
        public virtual Login User { get; set; }
    }
}
