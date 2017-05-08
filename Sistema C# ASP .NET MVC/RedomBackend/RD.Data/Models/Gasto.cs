using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Gasto
    {
        [Key]
        public int GastoID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Concepto { get; set; }


        [MaxLength(60)]
        public string Observaciones { get; set; }
        
        [Required]
        public DateTime FechaPago { get; set; }

        public string Nombre { get; set; }
        public string cedula { get; set; }
        public DateTime FechaComprobante { get; set; }
        public float ItbisFacturado { get; set; }
        public float ItbisRetenido { get; set; }
        public float MontoFacturado { get; set; }
        public float RetencionRenta { get; set; }
        public string TipoBienesServicios { get; set; }
        public string NCF { get; set; }
        public string TipoId{get; set;}
        public string DocumentoModificado { get; set; }
        public string status { get; set; }

    }
}
