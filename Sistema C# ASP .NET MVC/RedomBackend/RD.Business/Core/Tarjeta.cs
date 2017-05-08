using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Business.Core
{
    public class Tarjeta
    {
        private static RD.Data.DAL.DbContextRD ctx = new RD.Data.DAL.DbContextRD();

        public static List<RD.Data.Models.Tarjeta> GetTarjetas(){
            List<RD.Data.Models.Tarjeta> res = new List<RD.Data.Models.Tarjeta>();
            RD.Data.Models.Tarjeta Ta = new RD.Data.Models.Tarjeta { Nombre = ""};
            res.Add(Ta);
            var tars = ctx.Tarjetas.ToList();
            foreach(var i in tars)
            {
                res.Add(i);
            }
            return res;
        }
    }
}
