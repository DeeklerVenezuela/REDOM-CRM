using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RD.Data.Models;

namespace RD.Business.Core
{
    public class Banco
    {
        private static RD.Data.DAL.DbContextRD ctx = new RD.Data.DAL.DbContextRD();
        public static List<RD.Data.Models.Banco> GetBancos()
        {
           
            List<RD.Data.Models.Banco> res = new List<RD.Data.Models.Banco>();
            RD.Data.Models.Banco Banco = new Data.Models.Banco{ Nombre = "" , Descripcion = ""};
            res.Add(Banco);
            var bans = ctx.Bancos.ToList();
            foreach(var i in bans)
            {
                res.Add(i);
            }
            return res;
        }
    }
}
