using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RD.Data.Models
{
    public class Documentacion
    {
        [Key]
        public int DocumentacionID { get; set; }

        [Display(Name="Carta de Bienvenida")]
        public bool CartaBienvenida { get; set; }

        public bool Carnet { get; set; }

        [Display(Name = "Label de Sobres")]
        public bool LabelSobres { get; set; }

        [Display(Name = "Certificado de Regalo")]
        public bool CertificadoRegalo { get; set; }

        [Display(Name = "Certificado de Regalo 2")]
        public bool CertificadoRegalo2 { get; set; }

        public virtual Empleado ModificadoPor { get; set; }
        
        public bool Generada { get; set; }

        public virtual Afiliado Afiliado { get; set; }

    }
}
