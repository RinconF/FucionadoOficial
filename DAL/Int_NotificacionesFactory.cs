using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Int_NotificacionesFactory : FactoryBase
    {
        public Int_NotificacionesFactory() { }

        public Int_Notificaciones Load(Int_Notificaciones _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Notificaciones(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Int_NotificacionesCollection SelectByParams(Int_Notificaciones _obj, int Action)
        {
            Int_NotificacionesCollection Collection = new Int_NotificacionesCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Int_Notificaciones(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Int_Notificaciones _obj, int Action)
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
        public int InsertOrUpdate(Int_Notificaciones _obj, int Action)
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
        private void AddParameters(Int_Notificaciones _obj)
        {
            CreateCommand("SP_Int_Notificaciones", true);
            AddCmdParameter("@Id_Notificacion", _obj.Id_Notificacion, ParameterDirection.Input);
            AddCmdParameter("@Id_Usuario_Emisor", _obj.Id_Usuario_Emisor, ParameterDirection.Input);
            AddCmdParameter("@Id_Usuario_Receptor", _obj.Id_Usuario_Receptor, ParameterDirection.Input);
            AddCmdParameter("@Asunto", _obj.Asunto, ParameterDirection.Input);
            AddCmdParameter("@Mensaje", _obj.Mensaje, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input);
        }
    }
}
