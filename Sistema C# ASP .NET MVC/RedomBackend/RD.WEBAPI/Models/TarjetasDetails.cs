using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RD.WEBAPI.Models
{
    public class TarjetasDetails
    {
        public float Monto { get; set; }
        public DateTime Fecha{get; set;}
        public string Concepto { get; set; }
        public string Tipo { get; set; }
        public string Url{get; set;}
    }
}