using DCL;
using System.Data;

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
                AddCmdParameter("@Action", 3, ParameterDirection.Input);  // Action 3: obtener por ID
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Popup(GetDataReader());
                }
                return _obj;
            }
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                i = -1;
                throw;
            }
            return i;
        }

        private void AddParameters(Int_Popup _obj)
        {
            // Debe coincidir con la firma actual del SP_Int_Popup
            // (@Id_Popup, @Titulo, @Descripcion, @Imagen, @Video, @Url,
            //  @Estado, @Tiempo_Visualizacion, @Fecha_Inicio, @Fecha_Fin,
            //  @Id_Usuario, @Id_Rol, @RolesIds, @Interaccion, @Action)
            CreateCommand("SP_Int_Popup", true);

            AddCmdParameter("@Id_Popup", _obj.Id_Popup, ParameterDirection.Input);
            AddCmdParameter("@Titulo", _obj.Titulo, ParameterDirection.Input);
            AddCmdParameter("@Descripcion", _obj.Descripcion, ParameterDirection.Input);
            AddCmdParameter("@Imagen", _obj.Imagen, ParameterDirection.Input);
            AddCmdParameter("@Video", _obj.Video, ParameterDirection.Input);
            AddCmdParameter("@Url", _obj.Url, ParameterDirection.Input);

            AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input);
            AddCmdParameter("@Tiempo_Visualizacion", _obj.Tiempo_Visualizacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Inicio", _obj.Fecha_Inicio, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Fin", _obj.Fecha_Fin, ParameterDirection.Input);

            AddCmdParameter("@Id_Usuario", _obj.Id_Usuario, ParameterDirection.Input);
            AddCmdParameter("@Id_Rol", _obj.Id_Rol, ParameterDirection.Input);

            AddCmdParameter("@RolesIds", _obj.RolesIds, ParameterDirection.Input);
            AddCmdParameter("@Interaccion", _obj.Interaccion, ParameterDirection.Input);
        }
    }
}
