using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCL
{
    public class Int_01_ENC_Encuestas
    {
        #region Propiedades 
        Int32? mvarId_Info_Empleado = null;
        public Int32? Id_Info_Empleado
        {
            get { return mvarId_Info_Empleado; }
            set { mvarId_Info_Empleado = value; }
        }

        Int32? mvarId_Encuesta = null;
        public Int32? Id_Encuesta
        {
            get { return mvarId_Encuesta; }
            set { mvarId_Encuesta = value; }
        }

        Int32? mvarId_Sede = null;
        public Int32? Id_Sede
        {
            get { return mvarId_Sede; }
            set { mvarId_Sede = value; }
        }

        Int32? mvarId_Grupo_Empleado = null;
        public Int32? Id_Grupo_Empleado
        {
            get { return mvarId_Grupo_Empleado; }
            set { mvarId_Grupo_Empleado = value; }
        }

        Int32? mvarId_Ingreso_Encuesta = null;
        public Int32? Id_Ingreso_Encuesta
        {
            get { return mvarId_Ingreso_Encuesta; }
            set { mvarId_Ingreso_Encuesta = value; }
        }

        Int32? mvarId_Pregunta = null;
        public Int32? Id_Pregunta
        {
            get { return mvarId_Pregunta; }
            set { mvarId_Pregunta = value; }
        }

        String mvarRespuesta = null;
        public String Respuesta
        {
            get { return mvarRespuesta; }
            set { mvarRespuesta = value; }
        }

        Int32? mvarInt_Id_Usuario = null;
        public Int32? Int_Id_Usuario
        {
            get { return mvarInt_Id_Usuario; }
            set { mvarInt_Id_Usuario = value; }
        }

        #endregion
        #region Constructores 
        public Int_01_ENC_Encuestas() { }
        public Int_01_ENC_Encuestas(Int32? varId_Info_Empleado, Int32? varId_Encuesta, Int32? varId_Sede, Int32? varId_Grupo_Empleado, Int32? varId_Ingreso_Encuesta, Int32? varId_Pregunta, String varRespuesta, Int32? varInt_Id_Usuario)
        {
            mvarId_Info_Empleado = varId_Info_Empleado; mvarId_Encuesta = varId_Encuesta; mvarId_Sede = varId_Sede; mvarId_Grupo_Empleado = varId_Grupo_Empleado; mvarId_Ingreso_Encuesta = varId_Ingreso_Encuesta; mvarId_Pregunta = varId_Pregunta; mvarRespuesta = varRespuesta; mvarInt_Id_Usuario = varInt_Id_Usuario;
        }
        //public Int_01_ENC_Encuestas(IDataRecord obj)
        //{
        //    mvarId_Encuesta = Convert.ToInt32(obj["Id_Encuesta"]);
        //    mvarNombre = Convert.ToString(obj["Nombre"]);
        //    mvarDescripcion = Convert.ToString(obj["Descripcion"]);
        //    mvarId_Estado = Convert.ToInt32(obj["Id_Estado"]);
        //    mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]);
        //    mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
        //    mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]);
        //    mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]);

        //}
        //public Int_01_ENC_Encuestas(DataRow obj)
        //{
        //    mvarId_Encuesta = Convert.ToInt32(obj["Id_Encuesta"]);
        //    mvarNombre = Convert.ToString(obj["Nombre"]);
        //    mvarDescripcion = Convert.ToString(obj["Descripcion"]);
        //    mvarId_Estado = Convert.ToInt32(obj["Id_Estado"]);
        //    mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]);
        //    mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
        //    mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]);
        //    mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]);

        //}
        #endregion
    }
}
