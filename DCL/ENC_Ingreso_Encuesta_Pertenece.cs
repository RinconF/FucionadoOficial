using System;
using System.Data;

namespace DCL
{
    public class ENC_Ingreso_Encuesta_Pertenece
    {
        #region Propiedades 
        Int32? mvarId_Ingreso_Encuesta_Pertenece = null;
        public Int32? Id_Ingreso_Encuesta_Pertenece
        {
            get { return mvarId_Ingreso_Encuesta_Pertenece; }
            set { mvarId_Ingreso_Encuesta_Pertenece = value; }
        }

        String mvarId_Sede = null;
        public String Id_Sede
        {
            get { return mvarId_Sede; }
            set { mvarId_Sede = value; }
        }

        String mvarId_Proceso = null;
        public String Id_Proceso
        {
            get { return mvarId_Proceso; }
            set { mvarId_Proceso = value; }
        }

        String mvarId_Ingreso_Encuesta = null;
        public String Id_Ingreso_Encuesta
        {
            get { return mvarId_Ingreso_Encuesta; }
            set { mvarId_Ingreso_Encuesta = value; }
        }

        String mvarFecha_Creacion = null;
        public String Fecha_Creacion
        {
            get { return mvarFecha_Creacion; }
            set { mvarFecha_Creacion = value; }
        }

        String mvarEdad = null;
        public String Edad
        {
            get { return mvarEdad; }
            set { mvarEdad = value; }
        }

        String mvarAntiguedad = null;
        public String Antiguedad
        {
            get { return mvarAntiguedad; }
            set { mvarAntiguedad = value; }
        }

        String mvarIdentificacion = null;
        public String Identificacion
        {
            get { return mvarIdentificacion; }
            set { mvarIdentificacion = value; }
        }
        #endregion

        #region Constructores 
        public ENC_Ingreso_Encuesta_Pertenece() { }
        public ENC_Ingreso_Encuesta_Pertenece(
            Int32? varId_Ingreso_Encuesta_Pertenece,
            String varId_Sede,
            String varId_Proceso,
            String varId_Ingreso_Encuesta,
            String varFecha_Creacion,
            String varEdad,
            String varAntiguedad,
            String varIdentificacion
            )
        {
            mvarId_Ingreso_Encuesta_Pertenece = varId_Ingreso_Encuesta_Pertenece;
            mvarId_Sede = varId_Sede;
            mvarId_Proceso = varId_Proceso;
            mvarId_Ingreso_Encuesta = varId_Ingreso_Encuesta;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarEdad = varEdad;
            mvarAntiguedad = varAntiguedad;
            mvarIdentificacion = varIdentificacion;
        }
        public ENC_Ingreso_Encuesta_Pertenece(IDataRecord obj)
        {
            mvarId_Ingreso_Encuesta_Pertenece = Convert.ToInt32(obj["Id_Ingreso_Encuesta_Pertenece"]);
            mvarId_Sede = Convert.ToString(obj["Id_Sede"]);
            mvarId_Proceso = Convert.ToString(obj["Id_Proceso"]);
            mvarId_Ingreso_Encuesta = Convert.ToString(obj["Id_Ingreso_Encuesta"]);
            mvarId_Proceso = Convert.ToString(obj["Id_Proceso"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarEdad = Convert.ToString(obj["Edad"]);
            mvarAntiguedad = Convert.ToString(obj["Antiguedad"]);
            mvarIdentificacion = Convert.ToString(obj["Identificacion"]);

        }
        public ENC_Ingreso_Encuesta_Pertenece(DataRow obj)
        {
            mvarId_Ingreso_Encuesta_Pertenece = Convert.ToInt32(obj["Id_Ingreso_Encuesta_Pertenece"]);
            mvarId_Sede = Convert.ToString(obj["Id_Sede"]);
            mvarId_Proceso = Convert.ToString(obj["Id_Proceso"]);
            mvarId_Ingreso_Encuesta = Convert.ToString(obj["Id_Ingreso_Encuesta"]);
            mvarId_Proceso = Convert.ToString(obj["Id_Proceso"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarEdad = Convert.ToString(obj["Edad"]);
            mvarAntiguedad = Convert.ToString(obj["Antiguedad"]);
            mvarIdentificacion = Convert.ToString(obj["Identificacion"]);
        }
    }
    #endregion
}
