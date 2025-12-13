using System;
using System.Data;
using DCL;

namespace DAL
{
    public class Int_TutorialesFactory : FactoryBase
    {
        public Int_TutorialesFactory() { }

        public Int_Tutoriales Load(Int_Tutoriales _obj)
        {
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", 2, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    _obj = new Int_Tutoriales(GetDataReader());
                }
                return _obj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Int_TutorialesCollection SelectByParams(Int_Tutoriales _obj, int Action)
        {
            Int_TutorialesCollection Collection = new Int_TutorialesCollection();
            try
            {
                AddParameters(_obj);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Int_Tutoriales(GetDataReader()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Collection;
        }

        public DataTable SelectTable(Int_Tutoriales _obj, int Action)
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

        public int InsertOrUpdate(Int_Tutoriales _obj, int Action)
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

        private void AddParameters(Int_Tutoriales _obj)
        {
            CreateCommand("SP_Int_Tutoriales", true);
            AddCmdParameter("@Id_Tutorial", _obj.Id_Tutorial, ParameterDirection.Input);
            AddCmdParameter("@Titulo", _obj.Titulo, ParameterDirection.Input);
            AddCmdParameter("@Descripcion", _obj.Descripcion, ParameterDirection.Input);
            AddCmdParameter("@Video", _obj.Video, ParameterDirection.Input);
            AddCmdParameter("@Seccion", _obj.Seccion, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Actualizacion", _obj.Usuario_Actualizacion, ParameterDirection.Input);
            AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input);
        }
    }
}