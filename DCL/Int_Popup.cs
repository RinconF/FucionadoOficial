using iTextSharp.xmp.impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCL
{
    public class Int_Popup
    {
        #region Propiedades 

        Int32? mvarId_Popup = null;
        public Int32? Id_Popup
        {
            get { return mvarId_Popup; }
            set { mvarId_Popup = value; }
        }

        String mvarTitulo = null;
        public String Titulo
        {
            get { return mvarTitulo; }
            set { mvarTitulo = value; }
        }

        String mvarDescripcion = null;
        public String Descripcion
        {
            get { return mvarDescripcion; }
            set { mvarDescripcion = value; }
        }

        String mvarImagen = null;
        public String Imagen
        {
            get { return mvarImagen; }
            set { mvarImagen = value; }
        }

        String mvarUrl = null;
        public String Url
        {
            get { return mvarUrl; }
            set { mvarUrl = value; }
        }

        DateTime? mvarFecha_Creacion = null;
        public DateTime? Fecha_Creacion
        {
            get { return mvarFecha_Creacion; }
            set { mvarFecha_Creacion = value; }
        }

        Boolean? mvarEstado = null;
        public Boolean? Estado
        {
            get { return mvarEstado; }
            set { mvarEstado = value; }
        }

        Int32? mvarTiempo_Visualizacion = null;
        public Int32? Tiempo_Visualizacion
        {
            get { return mvarTiempo_Visualizacion; }
            set { mvarTiempo_Visualizacion = value; }
        }

        DateTime? mvarFecha_Inicio = null;
        public DateTime? Fecha_Inicio
        {
            get { return mvarFecha_Inicio; }
            set { mvarFecha_Inicio = value; }
        }

        DateTime? mvarFecha_Fin = null;
        public DateTime? Fecha_Fin
        {
            get { return mvarFecha_Fin; }
            set { mvarFecha_Fin = value; }
        }

        Int32? mvarId_Usuario = null;
        public Int32? Id_Usuario
        {
            get { return mvarId_Usuario; }
            set { mvarId_Usuario = value; }
        }

        String mvarInteraccion = null;
        public String Interaccion
        {
            get { return mvarInteraccion; }
            set { mvarInteraccion = value; }
        }

        String mvarRolesIds = null;
        public String RolesIds
        {
            get { return mvarRolesIds; }
            set { mvarRolesIds = value; }
        }

        #endregion

        #region Constructores 

        public Int_Popup() { }

        public Int_Popup(
            Int32? varId_Popup,
            String varTitulo,
            String varDescripcion,
            String varImagen,
            String varUrl,
            DateTime? varFecha_Creacion,
            Boolean? varEstado,
            Int32? varTiempo_Visualizacion,
            DateTime? varFecha_Inicio,
            DateTime? varFecha_Fin
        )
        {
            mvarId_Popup = varId_Popup;
            mvarTitulo = varTitulo;
            mvarDescripcion = varDescripcion;
            mvarImagen = varImagen;
            mvarUrl = varUrl;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarEstado = varEstado;
            mvarTiempo_Visualizacion = varTiempo_Visualizacion;
            mvarFecha_Inicio = varFecha_Inicio;
            mvarFecha_Fin = varFecha_Fin;
        }

        public Int_Popup(IDataRecord obj)
        {
            mvarId_Popup = obj["Id_Popup"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Popup"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarImagen = obj["Imagen"] != DBNull.Value ?
                Convert.ToString(obj["Imagen"]) : null;

            mvarUrl = obj["Url"] != DBNull.Value ?
                Convert.ToString(obj["Url"]) : null;

            mvarFecha_Creacion = obj["Fecha_Creacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Creacion"]) : (DateTime?)null;

            mvarEstado = obj["Estado"] != DBNull.Value ?
                Convert.ToBoolean(obj["Estado"]) : (Boolean?)null;

            mvarTiempo_Visualizacion = obj["Tiempo_Visualizacion"] != DBNull.Value ?
                Convert.ToInt32(obj["Tiempo_Visualizacion"]) : (Int32?)null;

            if (obj["Fecha_Inicio"] != DBNull.Value)
            {
                var fechaInicio = obj["Fecha_Inicio"];
                if (fechaInicio is DateTime)
                    mvarFecha_Inicio = (DateTime)fechaInicio;
                else if (fechaInicio is String && DateTime.TryParse(fechaInicio.ToString(), out DateTime parsedDate))
                    mvarFecha_Inicio = parsedDate;
            }

            if (obj["Fecha_Fin"] != DBNull.Value)
            {
                var fechaFin = obj["Fecha_Fin"];
                if (fechaFin is DateTime)
                    mvarFecha_Fin = (DateTime)fechaFin;
                else if (fechaFin is String && DateTime.TryParse(fechaFin.ToString(), out DateTime parsedDate))
                    mvarFecha_Fin = parsedDate;
            }
        }
        public Int_Popup(DataRow obj)
        {
            mvarId_Popup = obj["Id_Popup"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Popup"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarImagen = obj["Imagen"] != DBNull.Value ?
                Convert.ToString(obj["Imagen"]) : null;

            mvarUrl = obj["Url"] != DBNull.Value ?
                Convert.ToString(obj["Url"]) : null;

            mvarFecha_Creacion = obj["Fecha_Creacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Creacion"]) : (DateTime?)null;

            mvarEstado = obj["Estado"] != DBNull.Value ?
                Convert.ToBoolean(obj["Estado"]) : (Boolean?)null;

            mvarTiempo_Visualizacion = obj["Tiempo_Visualizacion"] != DBNull.Value ?
                Convert.ToInt32(obj["Tiempo_Visualizacion"]) : (Int32?)null;
        }
        #endregion
    }
}