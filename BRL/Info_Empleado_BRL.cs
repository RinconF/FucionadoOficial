using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DCL;
using DAL;


namespace BRL
{
    public class Info_Empleado_BRL
    {
        public static Info_Empleado Load(Info_Empleado objIEm,int Action)
        {
            Info_EmpleadoFactory objIEmf = new Info_EmpleadoFactory();
            return objIEmf.Load(objIEm);
        }
        public static Info_EmpleadoCollection SelectByParams(Info_Empleado objIEm, int Action)
        {
            Info_EmpleadoFactory objIEmf = new Info_EmpleadoFactory();
            return objIEmf.SelectByParams(objIEm, Action);
        }
        public static DataTable SelectTable(Info_Empleado objIEm, int Action)
        {
            Info_EmpleadoFactory objIEmf = new Info_EmpleadoFactory();
            return objIEmf.SelectTable(objIEm, Action);
        }
        public static int InsertarOrUpdate(Info_Empleado objIEm, int Action)
        {
            Info_EmpleadoFactory objIEmf = new Info_EmpleadoFactory();
            return objIEmf.InsertarOrUpdate(objIEm, Action);
        }
    }
}
