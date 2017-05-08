using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RD.WEBAPI.Models
{
    public class pago
    {
        public string rncedula { get; set; }
        public string tipoIdentificacion { get; set; }
        public string ncomprobantef { get; set; }
        public DateTime fechaComprobante { get; set; }
        public float itbisFacturado { get; set; }
        public float montofacturado { get; set; }
    }
}