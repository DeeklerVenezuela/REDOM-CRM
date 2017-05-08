using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaID { get; set; }
        public Empleado Empleado { get; set; }
        public Hotel Hotel { get; set; }
        public DateTime FechaHora { get; set; }
        public int Costo { get; set; }
        public Afiliado Afiliado { get; set; }
        public virtual List<HabitacionReserva> Habitaciones { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        [DisplayName("Adultos")]
        public int CantidadAdultos { get; set; }
        [DisplayName("Niños")]
        public int CantidadNinios { get; set; }
        public DateTime Entrada { get; set; }
        public int Dias { get; set; }
        public virtual List<PagosReserva> PagoReservas { get; set; }
        public virtual CertificadoDeRegalo Certificado { get; set; }
    }
}
