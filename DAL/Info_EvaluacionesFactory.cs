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
    public class Info_EvaluacionesFactory : FactoryBase
    {
        public Info_EvaluacionesFactory() { }


        public DataTable SelectTable(Info_Evaluaciones _obj, int Action)
        {
            DataTable dt = new DataTable();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                dt = GetDataSet().Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }

        public int InsertOrUpdate(Info_Evaluaciones _obj, int Action)
        {
            int i;
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteNonQuery();
                i = 1;
            }
            catch (Exception e)
            {
                i = -1;
                throw e;
            }
            return i;
        }

        private void AddParameters(Info_Evaluaciones _obj)
        {
            CreateCommand("SP_Info_Evaluacion", true);
            AddCmdParameter("@IdEvaluacion", _obj.IdEvaluacion, ParameterDirection.Input);
            AddCmdParameter("@Nombre", _obj.Nombre, ParameterDirection.Input);
            AddCmdParameter("@Descripcion", _obj.Descripcion, ParameterDirection.Input);
            AddCmdParameter("@Id_UsuarioCreacion", _obj.Id_UsuarioCreacion, ParameterDirection.Input);
            AddCmdParameter("@Id_UsuarioActualiza", _obj.Id_UsuarioActualiza, ParameterDirection.Input);
            AddCmdParameter("@FechaCreacion", _obj.FechaCreacion, ParameterDirection.Input);
            AddCmdParameter("@FechaActualizacion", _obj.FechaActualizacion, ParameterDirection.Input);
            AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input);
        }
    }
}
