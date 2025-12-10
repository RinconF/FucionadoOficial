using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;

namespace DAL
{
    public class Info_EncuestasFactory : FactoryBase
    {
        public Info_EncuestasFactory() { }

        public DataTable EjecutarProcedimientoEncuesta(int action, int idEncuesta)
        {
            CreateCommand("SP_Info_Encuestas", use_store_proc: true);
            AddCmdParameter("@Action", action, ParameterDirection.Input);
            AddCmdParameter("@IdEncuesta", idEncuesta, ParameterDirection.Input);
            return GetDataSet().Tables[0];
        }
    }
}
