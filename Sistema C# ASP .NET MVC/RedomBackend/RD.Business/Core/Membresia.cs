using RD.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Business.Core
{
    public class Membresia
    {
        
        public static RD.Data.Models.Membresia GetMembresia()
        {
            RD.Data.Models.Membresia Memb = new RD.Data.Models.Membresia();
            using (var db = new DbContextRD())
            {
                var Mems = db.Membresias.ToList();
            }
            
            return Memb;
        }
    }
}
