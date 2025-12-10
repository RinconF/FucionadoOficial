using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Int_UsuariosFactory : FactoryBase
    {
        public Int_UsuariosFactory() { }

        public Int_Usuarios Load(Int_Usuarios _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Usuarios(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Int_UsuariosCollection SelectByParams(Int_Usuarios _obj, int Action)
        {
            Int_UsuariosCollection Collection = new Int_UsuariosCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Int_Usuarios(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Int_Usuarios _obj, int Action)
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
        public int InsertOrUpdate(Int_Usuarios _obj, int Action)
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
        private void AddParameters(Int_Usuarios _obj)
        {
            CreateCommand("SP_Int_Usuarios", true);
            AddCmdParameter("@Id_Usuario", _obj.Id_Usuario, ParameterDirection.Input);
            AddCmdParameter("@Usuario", _obj.Usuario, ParameterDirection.Input);
            AddCmdParameter("@Contraseña", _obj.Contraseña, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Actualizacion", _obj.Usuario_Actualizacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Actualizacion", _obj.Fecha_Actualizacion, ParameterDirection.Input);
            AddCmdParameter("@Anexo_Foto", _obj.Anexo_Foto, ParameterDirection.Input);
            AddCmdParameter("@Id_Rol", _obj.Id_Rol, ParameterDirection.Input);
            AddCmdParameter("@Inicio", _obj.Inicio, ParameterDirection.Input);
            AddCmdParameter("@Fin", _obj.Fin, ParameterDirection.Input);
        }
    }
}