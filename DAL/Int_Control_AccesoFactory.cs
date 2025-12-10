using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Int_Control_AccesoFactory : FactoryBase
    {
        public Int_Control_AccesoFactory() { }

        public Int_Control_Acceso Load(Int_Control_Acceso _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Control_Acceso(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Int_Control_AccesoCollection SelectByParams(Int_Control_Acceso _obj, int Action)
        {
            Int_Control_AccesoCollection Collection = new Int_Control_AccesoCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Int_Control_Acceso(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Int_Control_Acceso _obj, int Action)
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
        public int InsertOrUpdate(Int_Control_Acceso _obj, int Action)
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
        private void AddParameters(Int_Control_Acceso _obj)
        {
            CreateCommand("SP_Int_Control_Acceso", true);
            AddCmdParameter("@Id_Ctrl_Acso", _obj.Id_Ctrl_Acso, ParameterDirection.Input);
            AddCmdParameter("@Id_IE", _obj.Id_IE, ParameterDirection.Input);
            AddCmdParameter("@Identificacion", _obj.Identificacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Hora", _obj.Fecha_Hora, ParameterDirection.Input);
            AddCmdParameter("@Id_Tp_Acso", _obj.Id_Tp_Acso, ParameterDirection.Input);
            AddCmdParameter("@Id_Sede", _obj.Id_Sede, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Temperatura", _obj.Temperatura, ParameterDirection.Input);
        }
    }
}
