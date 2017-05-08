using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class NCF
    {
        [Key]
        public int NCFID { get; set; }

        public virtual TipoComprobante TipoComprobante { get; set; }

        [MaxLength(25)]
        public string Serie { get; set; }
        
        [MaxLength(25)]
        public string NumeradorInicial { get; set; }
        
        [MaxLength(25)]
        public string NumeradorFinal { get; set; }

        [MaxLength(25)]
        public string NumeradorActual { get; set; }

        public int IndicadorDeFinal { get; set; }
    }
}
