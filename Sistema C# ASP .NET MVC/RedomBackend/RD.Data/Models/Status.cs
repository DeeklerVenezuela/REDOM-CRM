using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }
        public DateTime Fecha { get; set; }
    }
}
