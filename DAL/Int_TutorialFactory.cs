using System.Data;
using DCL;

namespace DAL
{
    public class Int_TutorialFactory : FactoryBase
    {
        public Int_TutorialFactory() { }

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
            catch
            {
                throw;
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
            catch
            {
                throw;
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
            catch
            {
                throw;
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

        private void AddParameters(Int_Tutoriales _obj)
        {
            CreateCommand("SP_Int_Tutoriales", true);
            AddCmdParameter("@Id_Tutorial", _obj.Id_Tutorial, ParameterDirection.Input);
            AddCmdParameter("@Titulo", _obj.Titulo, ParameterDirection.Input);
            AddCmdParameter("@Descripcion", _obj.Descripcion, ParameterDirection.Input);
            AddCmdParameter("@Url", _obj.Url, ParameterDirection.Input);
            AddCmdParameter("@Imagen", _obj.Imagen, ParameterDirection.Input);
            AddCmdParameter("@Seccion", _obj.Seccion, ParameterDirection.Input);
            AddCmdParameter("@Orden", _obj.Orden, ParameterDirection.Input);
            AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Usuario_Actualizacion", _obj.Usuario_Actualizacion, ParameterDirection.Input);
        }
    }
}
