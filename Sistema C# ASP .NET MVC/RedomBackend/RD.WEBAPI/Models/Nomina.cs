using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RD.WEBAPI.Models
{
    public class Nomina
    {
        public string nombre { get; set; }
        public string cedula { get; set; }
        public int ventasConfirmadas { get; set; }
        public float MontoVentasConfirmadas { get; set; }
        public float totalACobrar { get; set; }

    }
}