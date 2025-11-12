using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Nomina_DesprendiblesFactory : FactoryBase
    {
        public Nomina_DesprendiblesFactory() { }

        public Nomina_Desprendibles Load(Nomina_Desprendibles _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new Nomina_Desprendibles(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Nomina_DesprendiblesCollection SelectByParams(Nomina_Desprendibles _obj, int Action)
        {
            Nomina_DesprendiblesCollection Collection = new Nomina_DesprendiblesCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Nomina_Desprendibles(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Nomina_Desprendibles _obj, int Action)
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

        public int InsertOrUpdate(Nomina_Desprendibles _obj, int Action)
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

        private void AddParameters(Nomina_Desprendibles _obj)
        {
            CreateCommand("SP_Nomina_Desprendibles", true);
            AddCmdParameter("@N_Identificacion ", _obj.N_Identificacion, ParameterDirection.Input);
            AddCmdParameter("@Mes", _obj.Mes, ParameterDirection.Input);
            AddCmdParameter("@Year", _obj.Year, ParameterDirection.Input);
            AddCmdParameter("@Periodo", _obj.Periodo, ParameterDirection.Input);
            AddCmdParameter("@FilaInicial", _obj.FilaInicial, ParameterDirection.Input);
            AddCmdParameter("@FilaFinal", _obj.FilaFinal, ParameterDirection.Input);
            AddCmdParameter("@Numero_Lote", _obj.Numero_Lote, ParameterDirection.Input);
            AddCmdParameter("@Ruta_Archivo", _obj.Ruta_Archivo, ParameterDirection.Input);
            AddCmdParameter("@Correo", _obj.Correo, ParameterDirection.Input);
        }
    }
}

