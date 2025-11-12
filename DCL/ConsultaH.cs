using System;
using System.Data;

namespace DCL
{
    public class ConsultaH
    {
        #region Constructores

        public ConsultaH() { }

        public ConsultaH(String strCodigo, String strCedula, String strConductor, String strFecha, String strAsignacion, String strAmplitud, String strProduccion, String strInicioParte, String strFinParte, String strHora, String strTarea, String strBus, String strParada)
        {
            mstrCodigo = strCodigo;
            mstrCedula = strCedula;
            mstrConductor = strConductor;
            mstrFecha = strFecha;
            mstrAsignacion = strAsignacion;
            mstrAmplitud = strAmplitud;
            mstrProduccion = strProduccion;
            mstrInicioParte = strInicioParte;
            mstrFinParte = strFinParte;
            mstrHora = strHora;
            mstrTarea = strTarea;
            mstrBus = strBus;
            mstrParada = strParada;
        }

        public ConsultaH(IDataRecord obj)
        {
            mstrCodigo = Convert.ToString(obj["Codigo"]);
            mstrCedula = Convert.ToString(obj["Cedula"]);
            mstrConductor = Convert.ToString(obj["Conductor"]);
            mstrFecha = Convert.ToString(obj["Fecha"]);
            mstrAsignacion = Convert.ToString(obj["Asignacion"]);
            mstrAmplitud = Convert.ToString(obj["Amplitud"]);
            mstrProduccion = Convert.ToString(obj["Produccion"]);
            mstrInicioParte = Convert.ToString(obj["InicioParte"]);
            mstrFinParte = Convert.ToString(obj["FinParte"]);
            mstrHora = Convert.ToString(obj["Hora"]);
            mstrTarea = Convert.ToString(obj["Tarea"]);
            mstrBus = Convert.ToString(obj["Bus"]);
            mstrParada = Convert.ToString(obj["Parada"]);
        }

        public ConsultaH(DataRow obj)
        {
            mstrCodigo = Convert.ToString(obj["Codigo"]);
            mstrCedula = Convert.ToString(obj["Cedula"]);
            mstrConductor = Convert.ToString(obj["Conductor"]);
            mstrFecha = Convert.ToString(obj["Fecha"]);
            mstrAsignacion = Convert.ToString(obj["Asignacion"]);
            mstrAmplitud = Convert.ToString(obj["Amplitud"]);
            mstrProduccion = Convert.ToString(obj["Produccion"]);
            mstrInicioParte = Convert.ToString(obj["InicioParte"]);
            mstrFinParte = Convert.ToString(obj["FinParte"]);
            mstrHora = Convert.ToString(obj["Hora"]);
            mstrTarea = Convert.ToString(obj["Tarea"]);
            mstrBus = Convert.ToString(obj["Bus"]);
            mstrParada = Convert.ToString(obj["Parada"]);
        }

        #endregion

        #region Propiedades

        String mstrCodigo = null;
        public String Codigo
        {
            get { return mstrCodigo; }
            set { mstrCodigo = value; }
        }

        String mstrCedula = null;
        public String Cedula
        {
            get { return mstrCedula; }
            set { mstrCedula = value; }
        }

        String mstrConductor = null;
        public String Conductor
        {
            get { return mstrConductor; }
            set { mstrConductor = value; }
        }

        String mstrFecha = null;
        public String Fecha
        {
            get { return mstrFecha; }
            set { mstrFecha = value; }
        }

        String mstrAsignacion = null;
        public String Asignacion
        {
            get { return mstrAsignacion; }
            set { mstrAsignacion = value; }
        }

        String mstrAmplitud = null;
        public String Amplitud
        {
            get { return mstrAmplitud; }
            set { mstrAmplitud = value; }
        }

        String mstrProduccion = null;
        public String Produccion
        {
            get { return mstrProduccion; }
            set { mstrProduccion = value; }
        }

        String mstrInicioParte = null;
        public String InicioParte
        {
            get { return mstrInicioParte; }
            set { mstrInicioParte = value; }
        }

        String mstrFinParte = null;
        public String FinParte
        {
            get { return mstrFinParte; }
            set { mstrFinParte = value; }
        }

        String mstrHora = null;
        public String Hora
        {
            get { return mstrHora; }
            set { mstrHora = value; }
        }

        String mstrTarea = null;
        public String Tarea
        {
            get { return mstrTarea; }
            set { mstrTarea = value; }
        }

        String mstrBus = null;
        public String Bus
        {
            get { return mstrBus; }
            set { mstrBus = value; }
        }

        String mstrParada = null;
        public String Parada
        {
            get { return mstrParada; }
            set { mstrParada = value; }
        }
        #endregion
    }
}

