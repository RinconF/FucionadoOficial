using System;
using System.Data;
using DCL;

namespace DAL
{
    public class ENC_RespuestaFactory : FactoryBase
    {
        public ENC_RespuestaFactory() { }

        public ENC_Respuesta Load(ENC_Respuesta _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new ENC_Respuesta(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ENC_RespuestaCollection SelectByParams(ENC_Respuesta _obj, int Action)
        {
            ENC_RespuestaCollection Collection = new ENC_RespuestaCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new ENC_Respuesta(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(ENC_Respuesta _obj, int Action)
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
        public int InsertOrUpdate(ENC_Respuesta _obj, int Action)
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
        private void AddParameters(ENC_Respuesta _obj)
        {
            CreateCommand("SP_ENC_Respuesta", true);
            AddCmdParameter("@Id_Respuesta", _obj.Id_Respuesta, ParameterDirection.Input);
            AddCmdParameter("@Id_Encuesta", _obj.Id_Encuesta, ParameterDirection.Input);
            AddCmdParameter("@Id_Pregunta", _obj.Id_Pregunta, ParameterDirection.Input);
            AddCmdParameter("@Id_Usuario_Responde", _obj.Id_Usuario_Responde, ParameterDirection.Input);
            AddCmdParameter("@Respuesta", _obj.Respuesta, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Responde", _obj.Fecha_Responde, ParameterDirection.Input);
            AddCmdParameter("@Id_Usuario_Califica", _obj.Id_Usuario_Califica, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Califica", _obj.Fecha_Califica, ParameterDirection.Input);
            AddCmdParameter("@Calificacion", _obj.Calificacion, ParameterDirection.Input);
            AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Actualiza", _obj.Usuario_Actualiza, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Actualiza", _obj.Fecha_Actualiza, ParameterDirection.Input);
            AddCmdParameter("@Inicio", _obj.Inicio, ParameterDirection.Input);
            AddCmdParameter("@Fin", _obj.Fin, ParameterDirection.Input);
        }
    }
}