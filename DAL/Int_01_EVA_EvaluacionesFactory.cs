using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCL;

namespace DAL
{
    public class Int_01_EVA_EvaluacionesFactory : FactoryBase
    {
        public Int_01_EVA_EvaluacionesFactory() { }

        //public Int_01_ENC_Encuestas Load(Int_01_ENC_Encuestas _obj)
        //{
        //    try
        //    {
        //        AddParameters(_obj);
        //        AddCmdParameter("@Action", 0, ParameterDirection.Input);
        //        ExecuteReader();
        //        while (Read())
        //        {
        //            _obj = new Int_01_EVA_Evaluaciones(GetDataReader());
        //        }
        //        return _obj;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //public Int_01_EVA_EvaluacionesCollection SelectByParams(Int_01_EVA_Evaluaciones _obj, int Action)
        //{
        //    Int_01_EVA_EvaluacionesCollection Collection = new Int_01_EVA_EvaluacionesCollection();
        //    try
        //    {
        //        AddParameters(_obj);
        //        AddCmdParameter("@Action", Action, ParameterDirection.Input);
        //        ExecuteReader();
        //        while (Read())
        //        {
        //            Collection.Add(new Int_01_EVA_Evaluaciones(GetDataReader()));
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return Collection;
        //}

        public DataTable SelectTable(Int_01_EVA_Evaluaciones _obj, int Action)
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
        public int InsertOrUpdate(Int_01_EVA_Evaluaciones _obj, int Action)
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
        private void AddParameters(Int_01_EVA_Evaluaciones _obj)
        {
            CreateCommand("Int_01_EVA_Evaluaciones_SP", true);
            AddCmdParameter("@Id_Info_Empleado", _obj.Id_Info_Empleado, ParameterDirection.Input);
            AddCmdParameter("@Id_Evaluacion", _obj.Id_Evaluacion, ParameterDirection.Input);
            AddCmdParameter("@Id_Sede", _obj.Id_Sede, ParameterDirection.Input);
            AddCmdParameter("@Id_Grupo_Empleado", _obj.Id_Grupo_Empleado, ParameterDirection.Input);
            AddCmdParameter("@Id_Ingreso_Evaluacion", _obj.Id_Ingreso_Evaluacion, ParameterDirection.Input);
            AddCmdParameter("@Id_Pregunta", _obj.Id_Pregunta, ParameterDirection.Input);
            AddCmdParameter("@Respuesta", _obj.Respuesta, ParameterDirection.Input);
            AddCmdParameter("@Int_Id_Usuario", _obj.Int_Id_Usuario, ParameterDirection.Input);
            AddCmdParameter("@Numero_Intento", _obj.Numero_Intento, ParameterDirection.Input);
        }
    }
}
