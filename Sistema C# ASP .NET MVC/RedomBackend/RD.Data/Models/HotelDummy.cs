using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class HotelDummy
    {
        [Key]
        public int HotelesDummyID { get; set; }
        [DisplayName("Ubicación")]
        [MaxLength(50)]
        public string Ubicacion { get; set; }
        [MaxLength(35)]
        public string Color { get; set; }
        [MaxLength(50)]
        public string Hotel { get; set; }
    }
}
