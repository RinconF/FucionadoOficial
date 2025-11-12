using System;
using System.Data;
using DCL;

namespace DAL
{
    public class ENC_Ingreso_Encuesta_PerteneceFactory : FactoryBase
    {
        public ENC_Ingreso_Encuesta_PerteneceFactory() { }

        public ENC_Ingreso_Encuesta_Pertenece Load(ENC_Ingreso_Encuesta_Pertenece _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new ENC_Ingreso_Encuesta_Pertenece(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ENC_Ingreso_EncuestaPerteneceCollection SelectByParams(ENC_Ingreso_Encuesta_Pertenece _obj, int Action)
        {
            ENC_Ingreso_EncuestaPerteneceCollection Collection = new ENC_Ingreso_EncuestaPerteneceCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new ENC_Ingreso_Encuesta_Pertenece(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(ENC_Ingreso_Encuesta_Pertenece _obj, int Action)
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

        public int InsertOrUpdate(ENC_Ingreso_Encuesta_Pertenece _obj, int Action)
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

        private void AddParameters(ENC_Ingreso_Encuesta_Pertenece _obj)
        {
            CreateCommand("SP_ENC_Ingreso_Encuesta_Pertenece", true);
            AddCmdParameter("@Id_Ingreso_Encuesta_Pertenece", _obj.Id_Ingreso_Encuesta_Pertenece, ParameterDirection.Input);
            AddCmdParameter("@Id_Sede", _obj.Id_Sede, ParameterDirection.Input);
            AddCmdParameter("@Id_Proceso", _obj.Id_Proceso, ParameterDirection.Input);
            AddCmdParameter("@Id_Ingreso_Encuesta", _obj.Id_Ingreso_Encuesta, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Edad", _obj.Edad, ParameterDirection.Input);
            AddCmdParameter("@Antiguedad", _obj.Antiguedad, ParameterDirection.Input);
            AddCmdParameter("@Identificacion", _obj.Identificacion, ParameterDirection.Input);
        }
    }
}
