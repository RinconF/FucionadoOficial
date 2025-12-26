using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Int_AplicativoFactory : FactoryBase
    {
        public Int_AplicativoFactory() { }

        public Int_Aplicativos Load(Int_Aplicativos _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 2, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Aplicativos(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Int_AplicativoCollection SelectByParams(Int_Aplicativos _obj, int Action)
        {
            Int_AplicativoCollection Collection = new Int_AplicativoCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Int_Aplicativos(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Int_Aplicativos _obj, int Action)
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

        public int InsertOrUpdate(Int_Aplicativos _obj, int Action)
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

        private void AddParameters(Int_Aplicativos _obj)
        {
            CreateCommand("SP_Int_Aplicativos", true);
            AddCmdParameter("@Id_Aplicativo", _obj.Id_Aplicativo, ParameterDirection.Input);
            AddCmdParameter("@Titulo", _obj.Titulo, ParameterDirection.Input);
            AddCmdParameter("@Descripcion", _obj.Descripcion, ParameterDirection.Input);
            AddCmdParameter("@Imagen", _obj.Imagen, ParameterDirection.Input);
            AddCmdParameter("@Url", _obj.Url, ParameterDirection.Input);
            AddCmdParameter("@Seccion", _obj.Seccion, ParameterDirection.Input);
            AddCmdParameter("@Orden", _obj.Orden, ParameterDirection.Input);
            AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Actualizacion", _obj.Usuario_Actualizacion, ParameterDirection.Input);
        }
    }
}