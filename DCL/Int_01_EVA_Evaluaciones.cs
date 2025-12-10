using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCL
{
    public class Int_01_EVA_Evaluaciones
    {
        #region Propiedades 
        Int32? mvarId_Info_Empleado = null;
        public Int32? Id_Info_Empleado
        {
            get { return mvarId_Info_Empleado; }
            set { mvarId_Info_Empleado = value; }
        }

        Int32? mvarId_Evaluacion = null;
        public Int32? Id_Evaluacion
        {
            get { return mvarId_Evaluacion; }
            set { mvarId_Evaluacion = value; }
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

        Int32? mvarId_Ingreso_Evaluacion = null;
        public Int32? Id_Ingreso_Evaluacion
        {
            get { return mvarId_Ingreso_Evaluacion; }
            set { mvarId_Ingreso_Evaluacion = value; }
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
        Int32? mvarNumero_Intento = null;
        public Int32? Numero_Intento
        {
            get { return mvarNumero_Intento; }
            set { mvarNumero_Intento = value; }
        }

        Decimal? mvarResultado = null;
        public Decimal? Resultado
        {
            get { return mvarResultado; }
            set { mvarResultado = value; }
        }

        Boolean? mvarEstado_Aprobacion = null;
        public Boolean? Estado_Aprobacion
        {
            get { return mvarEstado_Aprobacion; }
            set { mvarEstado_Aprobacion = value; }
        }

        #endregion
        #region Constructores
        public Int_01_EVA_Evaluaciones() { }

        public Int_01_EVA_Evaluaciones(Int32? varId_Info_Empleado, Int32? varId_Evaluacion, Int32? varId_Sede, Int32? varId_Grupo_Empleado, Int32? varId_Ingreso_Evaluacion, Int32? varId_Pregunta, String varRespuesta, Int32? varInt_Id_Usuario, Int32? varNumero_Intento, Decimal? varResultado, Boolean? varEstado_Aprobacion)
        {
            mvarId_Info_Empleado = varId_Info_Empleado;
            mvarId_Evaluacion = varId_Evaluacion;
            mvarId_Sede = varId_Sede;
            mvarId_Grupo_Empleado = varId_Grupo_Empleado;
            mvarId_Ingreso_Evaluacion = varId_Ingreso_Evaluacion;
            mvarId_Pregunta = varId_Pregunta;
            mvarRespuesta = varRespuesta;
            mvarInt_Id_Usuario = varInt_Id_Usuario;
            mvarNumero_Intento = varNumero_Intento;
            mvarResultado = varResultado;
            mvarEstado_Aprobacion = varEstado_Aprobacion;

        }

        //public Int_01_EVA_Evaluaciones(IDataRecord obj)
        //{
        //    mvarId_Evaluacion = Convert.ToInt32(obj["Id_Evaluacion"]);
        //    mvarAno_Evaluacion = Convert.ToInt32(obj["Ano_Encuesta"]);
        //    mvarNombre = Convert.ToString(obj["Nombre"]);
        //    mvarDescripcion = Convert.ToString(obj["Descripcion"]);
        //    mvarEstado = Convert.ToBoolean(obj["Id_Estado"]);
        //    mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]);
        //    mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
        //    mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]);
        //    mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]);
        //}

        //public Int_01_EVA_Evaluaciones(DataRow obj)
        //{
        //    mvarId_Evaluacion = Convert.ToInt32(obj["Id_Evaluacion"]);
        //    mvarAno_Evaluacion = Convert.ToInt32(obj["Ano_Encuesta"]);
        //    mvarNombre = Convert.ToString(obj["Nombre"]);
        //    mvarDescripcion = Convert.ToString(obj["Descripcion"]);
        //    mvarEstado = Convert.ToBoolean(obj["Id_Estado"]);
        //    mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]);
        //    mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
        //    mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]);
        //    mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]);
        //}
        #endregion
    }
}
