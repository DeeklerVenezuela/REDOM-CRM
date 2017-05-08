using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Business.Core
{
    public class Empleado
    {
        //Validate User Login de empeados al sistema
        private static RD.Data.DAL.DbContextRD ctx = new RD.Data.DAL.DbContextRD();
        public static bool MakeLogin(string user, string pass)
        {
            var db = new RD.Data.DAL.DbContextRD();
            var emp = db.Logins.Where(x=>x.UserName == user).FirstOrDefault();
            if (emp != null)
            {
                return Repository.Hash.ValidarClave(pass, emp.Password);
            }
            return false;
        }

        public static List<RD.Data.Models.Empleado> GetEmpleados()
        {
            List<RD.Data.Models.Empleado> emp = new List<Data.Models.Empleado>();
            emp = ctx.Empleados.ToList();
            return emp;
        }
    }
}
