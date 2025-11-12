using System;
using System.Data;

namespace DCL
{
    public class ENC_Ingreso_Encuesta
    {
        #region Propiedades 
        Int32? mvarId_Ingreso_Encuesta = null;
        public Int32? Id_Ingreso_Encuesta
        {
            get { return mvarId_Ingreso_Encuesta; }
            set { mvarId_Ingreso_Encuesta = value; }
        }

        String mvarId_Encuesta = null;
        public String Id_Encuesta
        {
            get { return mvarId_Encuesta; }
            set { mvarId_Encuesta = value; }
        }

        String mvarFecha_Creacion = null;
        public String Fecha_Creacion
        {
            get { return mvarFecha_Creacion; }
            set { mvarFecha_Creacion = value; }
        }

        String mvarIdentificacion = null;
        public String Identificacion
        {
            get { return mvarIdentificacion; }
            set { mvarIdentificacion = value; }
        }
        #endregion

        #region Constructores 
        public ENC_Ingreso_Encuesta() { }
        public ENC_Ingreso_Encuesta(
            Int32? varId_Ingreso_Encuesta,
            String varId_Encuesta,
            String varFecha_Creacion,
            String varIdentificacion
            )
        {
            mvarId_Ingreso_Encuesta = varId_Ingreso_Encuesta;
            mvarId_Encuesta = varId_Encuesta;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarIdentificacion = varIdentificacion;

        }
        public ENC_Ingreso_Encuesta(IDataRecord obj)
        {
            mvarId_Ingreso_Encuesta = Convert.ToInt32(obj["Id_Ingreso_Encuesta"]);
            mvarId_Encuesta = Convert.ToString(obj["Id_Encuesta"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarIdentificacion = Convert.ToString(obj["Identificacion"]);

        }
        public ENC_Ingreso_Encuesta(DataRow obj)
        {
            mvarId_Ingreso_Encuesta = Convert.ToInt32(obj["Id_Ingreso_Encuesta"]);
            mvarId_Encuesta = Convert.ToString(obj["Id_Encuesta"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarIdentificacion = Convert.ToString(obj["Identificacion"]);

        }
    }
    #endregion
}
