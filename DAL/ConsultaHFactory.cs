using DCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class ConsultaHFactory:FactoryBase
    {

        public ConsultaHCollection SelectByParams(ConsultaH objConsul, int Action)
        {
            ConsultaHCollection Collection = new ConsultaHCollection();
            try
            {
                AddParameters(objConsul);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new ConsultaH(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(ConsultaH objConsul, int Action)
        {
            DataTable dt = new DataTable();
            try
            {
                AddParameters(objConsul);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                dt = GetDataSet().Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }


        private void AddParameters(ConsultaH objConsul)
        {
            CreateCommand("OPERACIONAL_ConsultaH", true);
            AddCmdParameter("@Cedula", objConsul.Cedula, ParameterDirection.Input);
            AddCmdParameter("@Code", objConsul.Codigo, ParameterDirection.Input);
            AddCmdParameter("@FechaIni", objConsul.Fecha, ParameterDirection.Input);
            AddCmdParameter("@FechaFin", objConsul.Hora, ParameterDirection.Input);
        }
    }
}
