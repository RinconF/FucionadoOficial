using System;
using System.Data;
namespace DCL
{
    public class ENC_Respuesta
    {
        #region Propiedades 
        Int32? mvarId_Respuesta = null;
        public Int32? Id_Respuesta
        {
            get { return mvarId_Respuesta; }
            set { mvarId_Respuesta = value; }
        }
        Int32? mvarId_Encuesta = null;
        public Int32? Id_Encuesta
        {
            get { return mvarId_Encuesta; }
            set { mvarId_Encuesta = value; }
        }
        Int32? mvarId_Pregunta = null;
        public Int32? Id_Pregunta
        {
            get { return mvarId_Pregunta; }
            set { mvarId_Pregunta = value; }
        }
        Int32? mvarId_Usuario_Responde = null;
        public Int32? Id_Usuario_Responde
        {
            get { return mvarId_Usuario_Responde; }
            set { mvarId_Usuario_Responde = value; }
        }
        
        String mvarRespuesta = null;
        public String Respuesta
        {
            get { return mvarRespuesta; }
            set { mvarRespuesta = value; }
        }
        String mvarFecha_Responde = null;
        public String Fecha_Responde
        {
            get { return mvarFecha_Responde; }
            set { mvarFecha_Responde = value; }
        }
        Int32? mvarId_Usuario_Califica = null;
        public Int32? Id_Usuario_Califica
        {
            get { return mvarId_Usuario_Califica; }
            set { mvarId_Usuario_Califica = value; }
        }
        String mvarFecha_Califica = null;
        public String Fecha_Califica
        {
            get { return mvarFecha_Califica; }
            set { mvarFecha_Califica = value; }
        }
        Int32? mvarCalificacion = null;
        public Int32? Calificacion
        {
            get { return mvarCalificacion; }
            set { mvarCalificacion = value; }
        }
        Boolean? mvarEstado = null;
        public Boolean? Estado
        {
            get { return mvarEstado; }
            set { mvarEstado = value; }
        }
        Int32? mvarUsuario_Creacion = null;
        public Int32? Usuario_Creacion
        {
            get { return mvarUsuario_Creacion; }
            set { mvarUsuario_Creacion = value; }
        }
        String mvarFecha_Creacion = null;
        public String Fecha_Creacion
        {
            get { return mvarFecha_Creacion; }
            set { mvarFecha_Creacion = value; }
        }
        Int32? mvarUsuario_Actualiza = null;
        public Int32? Usuario_Actualiza
        {
            get { return mvarUsuario_Actualiza; }
            set { mvarUsuario_Actualiza = value; }
        }

        Int32? mvarInicio = null;
        public Int32? Inicio
        {
            get { return mvarInicio; }
            set { mvarInicio = value; }
        }

        Int32? mvarFin = null;
        public Int32? Fin
        {
            get { return mvarFin; }
            set { mvarFin = value; }
        }

        String mvarFecha_Actualiza = null;
        public String Fecha_Actualiza
        {
            get { return mvarFecha_Actualiza; }
            set { mvarFecha_Actualiza = value; }
        }
        #endregion
        #region Constructores 
        public ENC_Respuesta() { }
        public ENC_Respuesta(Int32? varId_Respuesta, Int32? varId_Encuesta, Int32? varId_Pregunta, Int32? varId_Usuario_Responde, Int32? varId_Opcion_Respuesta, String varRespuesta, String varFecha_Responde, Int32? varId_Usuario_Califica, String varFecha_Califica, Int32? varCalificacion, Boolean? varEstado, Int32? varUsuario_Creacion, String varFecha_Creacion, Int32? varUsuario_Actualiza, String varFecha_Actualiza, Int32? varInicio, Int32? varFin)
        {
            mvarId_Respuesta = varId_Respuesta;
            mvarId_Encuesta = varId_Encuesta;
            mvarId_Pregunta = varId_Pregunta;
            mvarId_Usuario_Responde = varId_Usuario_Responde;
            mvarRespuesta = varRespuesta;
            mvarFecha_Responde = varFecha_Responde;
            mvarId_Usuario_Califica = varId_Usuario_Califica;
            mvarFecha_Califica = varFecha_Califica;
            mvarCalificacion = varCalificacion;
            mvarEstado = varEstado;
            mvarUsuario_Creacion = varUsuario_Creacion;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarUsuario_Actualiza = varUsuario_Actualiza;
            mvarFecha_Actualiza = varFecha_Actualiza;
            mvarInicio = varInicio;
            mvarFin = varFin;
        }
        public ENC_Respuesta(IDataRecord obj)
        {
            mvarId_Respuesta = Convert.ToInt32(obj["Id_Respuesta"]);
            mvarId_Encuesta = Convert.ToInt32(obj["Id_Encuesta"]);
            mvarId_Pregunta = Convert.ToInt32(obj["Id_Pregunta"]);
            mvarId_Usuario_Responde = Convert.ToInt32(obj["Id_Usuario_Responde"]);
            mvarRespuesta = Convert.ToString(obj["Respuesta"]);
            mvarFecha_Responde = Convert.ToString(obj["Fecha_Responde"]);
            mvarId_Usuario_Califica = Convert.ToInt32(obj["Id_Usuario_Califica"]);
            mvarFecha_Califica = Convert.ToString(obj["Fecha_Califica"]);
            mvarCalificacion = Convert.ToInt32(obj["Calificacion"]);
            mvarEstado = Convert.ToBoolean(obj["Estado"]);
            mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]);
            mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]);

        }
        public ENC_Respuesta(DataRow obj)
        {
            mvarId_Respuesta = Convert.ToInt32(obj["Id_Respuesta"]);
            mvarId_Encuesta = Convert.ToInt32(obj["Id_Encuesta"]);
            mvarId_Pregunta = Convert.ToInt32(obj["Id_Pregunta"]);
            mvarId_Usuario_Responde = Convert.ToInt32(obj["Id_Usuario_Responde"]);
            mvarRespuesta = Convert.ToString(obj["Respuesta"]);
            mvarFecha_Responde = Convert.ToString(obj["Fecha_Responde"]);
            mvarId_Usuario_Califica = Convert.ToInt32(obj["Id_Usuario_Califica"]);
            mvarFecha_Califica = Convert.ToString(obj["Fecha_Califica"]);
            mvarCalificacion = Convert.ToInt32(obj["Calificacion"]);
            mvarEstado = Convert.ToBoolean(obj["Estado"]);
            mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]);
            mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]);

        }
        #endregion
    }

}