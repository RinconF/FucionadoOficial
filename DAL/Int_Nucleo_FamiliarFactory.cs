using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Int_Nucleo_FamiliarFactory : FactoryBase
    {
        public Int_Nucleo_FamiliarFactory() { }

        public Int_Nucleo_Familiar Load(Int_Nucleo_Familiar _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Nucleo_Familiar(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Int_Nucleo_FamiliarCollection SelectByParams(Int_Nucleo_Familiar _obj, int Action)
        {
            Int_Nucleo_FamiliarCollection Collection = new Int_Nucleo_FamiliarCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Int_Nucleo_Familiar(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Int_Nucleo_Familiar _obj, int Action)
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
        public int InsertOrUpdate(Int_Nucleo_Familiar _obj, int Action)
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
        private void AddParameters(Int_Nucleo_Familiar _obj)
        {
            CreateCommand("SP_Int_Nucleo_Familiar", true);
            AddCmdParameter("@Id_Familiar", _obj.Id_Familiar, ParameterDirection.Input);
            AddCmdParameter("@Id_IE", _obj.Id_IE, ParameterDirection.Input);
            AddCmdParameter("@Nombre_Familiar", _obj.Nombre_Familiar, ParameterDirection.Input);
            AddCmdParameter("@Id_Genero", _obj.Id_Genero, ParameterDirection.Input);
            AddCmdParameter("@Id_Tipo_Doc", _obj.Id_Tipo_Doc, ParameterDirection.Input);
            AddCmdParameter("@Identificacion", _obj.Identificacion, ParameterDirection.Input);
            AddCmdParameter("@Edad", _obj.Edad, ParameterDirection.Input);
            AddCmdParameter("@Celular", _obj.Celular, ParameterDirection.Input);
            AddCmdParameter("@Id_Parentesco", _obj.Id_Parentesco, ParameterDirection.Input);
            AddCmdParameter("@Id_Escolaridad", _obj.Id_Escolaridad, ParameterDirection.Input);
            AddCmdParameter("@Id_Ocupa", _obj.Id_Ocupa, ParameterDirection.Input);
            AddCmdParameter("@Discapacidad", _obj.Discapacidad, ParameterDirection.Input);
            AddCmdParameter("@Anexo_Reg_Civil", _obj.Anexo_Reg_Civil, ParameterDirection.Input);
            AddCmdParameter("@Anexo_Dos", _obj.Anexo_Dos, ParameterDirection.Input);
            AddCmdParameter("@Anexo_Tres", _obj.Anexo_Tres, ParameterDirection.Input);
            AddCmdParameter("@Anexo_Cuatro", _obj.Anexo_Cuatro, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Actualizacion", _obj.Usuario_Actualizacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Actualizacion", _obj.Fecha_Actualizacion, ParameterDirection.Input);
        }
    }
}
