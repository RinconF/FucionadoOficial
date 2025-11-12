using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Nomina_Desprendibles_BRL
    {
        public static Nomina_Desprendibles Load(Nomina_Desprendibles _Obj, int Action)
        {
            return new Nomina_DesprendiblesFactory().Load(_Obj);
        }
        public static Nomina_DesprendiblesCollection SelectByParams(Nomina_Desprendibles _Obj, int Action)
        {
            return new Nomina_DesprendiblesFactory().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(Nomina_Desprendibles _Obj, int Action)
        {
            return new Nomina_DesprendiblesFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Nomina_Desprendibles _Obj, int Action)
        {
            return new Nomina_DesprendiblesFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}