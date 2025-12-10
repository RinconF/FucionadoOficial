using System;
using System.Data;

namespace DCL
{
    public class Nomina_Desprendibles
    {
        #region Propiedades 
        Int32? mvarN_Identificacion = null;
        public Int32? N_Identificacion
        {
            get { return mvarN_Identificacion; }
            set { mvarN_Identificacion = value; }
        }

        Int32? mvarMes = null;
        public Int32? Mes
        {
            get { return mvarMes; }
            set { mvarMes = value; }
        }

        Int32? mvarYear = null;
        public Int32? Year
        {
            get { return mvarYear; }
            set { mvarYear = value; }
        }

        Int32? mvarPeriodo = null;
        public Int32? Periodo
        {
            get { return mvarPeriodo; }
            set { mvarPeriodo = value; }
        }

        Int32? mvarFilaInical = null;
        public Int32? FilaInicial
        {
            get { return mvarFilaInical; }
            set { mvarFilaInical = value; }
        }

        Int32? mvarFilaFinal = null;
        public Int32? FilaFinal
        {
            get { return mvarFilaFinal; }
            set { mvarFilaFinal = value; }
        }

        Int32? mvarNumero_Lote= null;
        public Int32? Numero_Lote
        {
            get { return mvarNumero_Lote; }
            set { mvarNumero_Lote = value; }
        }

        String mvarRuta_Archivo= null;
        public String Ruta_Archivo
        {
            get { return mvarRuta_Archivo; }
            set { mvarRuta_Archivo = value; }
        }

        String mvarCorreo = null;
        public String Correo
        {
            get { return mvarCorreo; }
            set { mvarCorreo = value; }
        }

        #endregion

        #region Constructores 
        public Nomina_Desprendibles() { }
        public Nomina_Desprendibles(
            Int32? varN_Identificacion,
            Int32? varMes,
            Int32? varYear,
            Int32? varPeriodo,
            Int32? varFilaInical,
            Int32? varFilaFinal,
            Int32? varNumero_Lote,
            String varRuta_Archivo,
            String varCorreo
            )
        {
            mvarN_Identificacion = varN_Identificacion;
            mvarMes = varMes;
            mvarYear = varYear;
            mvarPeriodo = varPeriodo;
            mvarFilaInical= varFilaInical;
            mvarFilaFinal = varFilaFinal;
            mvarNumero_Lote= varNumero_Lote;
            mvarRuta_Archivo= varRuta_Archivo;
            mvarCorreo = varCorreo;
        }
        public Nomina_Desprendibles(IDataRecord obj)
        {
            mvarN_Identificacion = Convert.ToInt32(obj["N_Identificacion"]);
            mvarMes = Convert.ToInt32(obj["Mes"]);
            mvarYear = Convert.ToInt32(obj["Year"]);
            mvarPeriodo = Convert.ToInt32(obj["Periodo"]);
            mvarFilaInical = Convert.ToInt32(obj["FilaInicial"]);
            mvarFilaFinal = Convert.ToInt32(obj["FilaFinal"]);
            mvarNumero_Lote = Convert.ToInt32(obj["Numero_Lote"]);
            mvarRuta_Archivo= Convert.ToString(obj["Ruta_Archivo"]);
            mvarCorreo = Convert.ToString(obj["Correo"]);
        }
        public Nomina_Desprendibles(DataRow obj)
        {
            mvarN_Identificacion = Convert.ToInt32(obj["N_Identificacion"]);
            mvarMes = Convert.ToInt32(obj["Mes"]);
            mvarYear = Convert.ToInt32(obj["Year"]);
            mvarPeriodo = Convert.ToInt32(obj["Periodo"]);
            mvarFilaInical = Convert.ToInt32(obj["FilaInicial"]);
            mvarFilaFinal = Convert.ToInt32(obj["FilaFinal"]);
            mvarNumero_Lote = Convert.ToInt32(obj["Numero_Lote"]);
            mvarRuta_Archivo = Convert.ToString(obj["Ruta_Archivo"]);
            mvarCorreo = Convert.ToString(obj["Correo"]);
        }
    }
    #endregion
}