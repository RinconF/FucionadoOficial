using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Int_DocumentosFactory : FactoryBase
    {
        public Int_DocumentosFactory() { }

        public Int_Documentos Load(Int_Documentos _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 2, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Documentos(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Int_DocumentosCollection SelectByParams(Int_Documentos _obj, int Action)
        {
            Int_DocumentosCollection Collection = new Int_DocumentosCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Int_Documentos(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Int_Documentos _obj, int Action)
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

        public int InsertOrUpdate(Int_Documentos _obj, int Action)
        {
            int i;
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);

                if (Action == 3) // Insert - retorna el ID
                {
                    object result = ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        i = Convert.ToInt32(result);
                    }
                    else
                    {
                        i = 1;
                    }
                }
                else // Update/Delete
                {
                    ExecuteNonQuery();
                    i = 1;
                }
            }
            catch (Exception e)
            {
                i = -1;
                throw e;
            }
            return i;
        }

        private void AddParameters(Int_Documentos _obj)
        {
            CreateCommand("SP_Int_Documentos", true);
            AddCmdParameter("@Id_Documentos", _obj.Id_Documentos, ParameterDirection.Input);
            AddCmdParameter("@Titulo", _obj.Titulo, ParameterDirection.Input);
            AddCmdParameter("@Descripcion", _obj.Descripcion, ParameterDirection.Input);
            AddCmdParameter("@Archivo", _obj.Archivo, ParameterDirection.Input);
            AddCmdParameter("@Url", _obj.Url, ParameterDirection.Input);
            AddCmdParameter("@UsuarioCreacion", _obj.UsuarioCreacion, ParameterDirection.Input);
            AddCmdParameter("@UsuarioActualizacion", _obj.UsuarioActualizacion, ParameterDirection.Input);
            AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input);
        }
    }
}