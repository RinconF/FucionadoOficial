using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;

namespace DAL
{
    public class ENC_EncuestaFactory : FactoryBase
    {
        public ENC_EncuestaFactory() { }

        public ENC_Encuesta Load(ENC_Encuesta _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new ENC_Encuesta(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ENC_EncuestaCollection SelectByParams(ENC_Encuesta _obj, int Action)
        {
            ENC_EncuestaCollection Collection = new ENC_EncuestaCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new ENC_Encuesta(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(ENC_Encuesta _obj, int Action)
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
        public int InsertOrUpdate(ENC_Encuesta _obj, int Action)
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
        private void AddParameters(ENC_Encuesta _obj)
        {
            CreateCommand("SP_ENC_Encuesta", true);
            AddCmdParameter("@Id_Encuesta", _obj.Id_Encuesta, ParameterDirection.Input);
            AddCmdParameter("@Nombre", _obj.Nombre, ParameterDirection.Input);
            AddCmdParameter("@Descripcion", _obj.Descripcion, ParameterDirection.Input);
            AddCmdParameter("@Id_Estado", _obj.Id_Estado, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Actualiza", _obj.Usuario_Actualiza, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Actualiza", _obj.Fecha_Actualiza, ParameterDirection.Input);
        }
    }
}
