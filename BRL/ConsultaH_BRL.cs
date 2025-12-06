using DAL;
using DCL;
using System.Data;

namespace BRL
{
    public class ConsultaH_BRL
    {
        public static ConsultaHCollection SelectByParams(ConsultaH objCons, int Action)
        {
            ConsultaHFactory objConsf = new ConsultaHFactory();
            return objConsf.SelectByParams(objCons, Action);
        }

        public static DataTable selectHorario(ConsultaH objCons, int Action)
        {
            ConsultaHFactory objConsf = new ConsultaHFactory();
            return objConsf.SelectTable(objCons, Action);
        }
    }
}
