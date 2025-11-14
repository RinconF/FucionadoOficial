using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Int_PopupFactory : FactoryBase
    {
        public Int_PopupFactory() { }

        public Int_Popup Load(Int_Popup _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 3, ParameterDirection.Input);  // Action 3 = Obtener por ID
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Popup(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Int_PopupCollection SelectByParams(Int_Popup _obj, int Action)
        {
            Int_PopupCollection Collection = new Int_PopupCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Int_Popup(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Int_Popup _obj, int Action)
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

        public int InsertOrUpdate(Int_Popup _obj, int Action)
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

        private void AddParameters(Int_Popup _obj)
        {
            CreateCommand("SP_Int_Popup", true);
            AddCmdParameter("@Id_Popup", _obj.Id_Popup, ParameterDirection.Input);
            AddCmdParameter("@Titulo", _obj.Titulo, ParameterDirection.Input);
            AddCmdParameter("@Descripcion", _obj.Descripcion, ParameterDirection.Input);
            AddCmdParameter("@Imagen", _obj.Imagen, ParameterDirection.Input);
            AddCmdParameter("@Url", _obj.Url, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input);
            AddCmdParameter("@Tiempo_Visualizacion", _obj.Tiempo_Visualizacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Inicio", _obj.Fecha_Inicio, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Fin", _obj.Fecha_Fin, ParameterDirection.Input);
            AddCmdParameter("@Id_Usuario", _obj.Id_Usuario, ParameterDirection.Input);
            AddCmdParameter("@Interaccion", _obj.Interaccion, ParameterDirection.Input);
            AddCmdParameter("@RolesIds", _obj.RolesIds, ParameterDirection.Input);
        }
    }
}