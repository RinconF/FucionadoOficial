using System;
using System.Data;

namespace DCL
{
    public class Int_Tutoriales
    {
        #region Propiedades

        Int32? mvarId_Tutorial = null;
        public Int32? Id_Tutorial
        {
            get { return mvarId_Tutorial; }
            set { mvarId_Tutorial = value; }
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

        String mvarUrl = null;
        public String Url
        {
            get { return mvarUrl; }
            set { mvarUrl = value; }
        }

        String mvarImagen = null;
        public String Imagen
        {
            get { return mvarImagen; }
            set { mvarImagen = value; }
        }

        String mvarSeccion = null;
        public String Seccion
        {
            get { return mvarSeccion; }
            set { mvarSeccion = value; }
        }

        Int32? mvarOrden = null;
        public Int32? Orden
        {
            get { return mvarOrden; }
            set { mvarOrden = value; }
        }

        DateTime? mvarFecha_Creacion = null;
        public DateTime? Fecha_Creacion
        {
            get { return mvarFecha_Creacion; }
            set { mvarFecha_Creacion = value; }
        }

        DateTime? mvarFecha_Actualizacion = null;
        public DateTime? Fecha_Actualizacion
        {
            get { return mvarFecha_Actualizacion; }
            set { mvarFecha_Actualizacion = value; }
        }

        Int32? mvarUsuario_Creacion = null;
        public Int32? Usuario_Creacion
        {
            get { return mvarUsuario_Creacion; }
            set { mvarUsuario_Creacion = value; }
        }

        Int32? mvarUsuario_Actualizacion = null;
        public Int32? Usuario_Actualizacion
        {
            get { return mvarUsuario_Actualizacion; }
            set { mvarUsuario_Actualizacion = value; }
        }

        Boolean? mvarEstado = null;
        public Boolean? Estado
        {
            get { return mvarEstado; }
            set { mvarEstado = value; }
        }

        #endregion

        #region Constructores

        public Int_Tutoriales() { }

        public Int_Tutoriales(
            Int32? varId_Tutorial,
            String varTitulo,
            String varDescripcion,
            String varUrl,
            String varImagen,
            String varSeccion,
            Int32? varOrden,
            DateTime? varFecha_Creacion,
            DateTime? varFecha_Actualizacion,
            Int32? varUsuario_Creacion,
            Int32? varUsuario_Actualizacion,
            Boolean? varEstado
        )
        {
            mvarId_Tutorial = varId_Tutorial;
            mvarTitulo = varTitulo;
            mvarDescripcion = varDescripcion;
            mvarUrl = varUrl;
            mvarImagen = varImagen;
            mvarSeccion = varSeccion;
            mvarOrden = varOrden;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarFecha_Actualizacion = varFecha_Actualizacion;
            mvarUsuario_Creacion = varUsuario_Creacion;
            mvarUsuario_Actualizacion = varUsuario_Actualizacion;
            mvarEstado = varEstado;
        }

        public Int_Tutoriales(IDataRecord obj)
        {
            mvarId_Tutorial = obj["Id_Tutorial"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Tutorial"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarUrl = obj["Url"] != DBNull.Value ?
                Convert.ToString(obj["Url"]) : null;

            mvarImagen = obj["Imagen"] != DBNull.Value ?
                Convert.ToString(obj["Imagen"]) : null;

            mvarSeccion = obj["Seccion"] != DBNull.Value ?
                Convert.ToString(obj["Seccion"]) : null;

            mvarOrden = obj["Orden"] != DBNull.Value ?
                Convert.ToInt32(obj["Orden"]) : (Int32?)null;

            mvarFecha_Creacion = obj["Fecha_Creacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Creacion"]) : (DateTime?)null;

            mvarFecha_Actualizacion = obj["Fecha_Actualizacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Actualizacion"]) : (DateTime?)null;

            mvarUsuario_Creacion = obj["Usuario_Creacion"] != DBNull.Value ?
                Convert.ToInt32(obj["Usuario_Creacion"]) : (Int32?)null;

            mvarUsuario_Actualizacion = obj["Usuario_Actualizacion"] != DBNull.Value ?
                Convert.ToInt32(obj["Usuario_Actualizacion"]) : (Int32?)null;

            mvarEstado = obj["Estado"] != DBNull.Value ?
                Convert.ToBoolean(obj["Estado"]) : (Boolean?)null;
        }

        public Int_Tutoriales(DataRow obj)
        {
            mvarId_Tutorial = obj["Id_Tutorial"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Tutorial"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarUrl = obj["Url"] != DBNull.Value ?
                Convert.ToString(obj["Url"]) : null;

            mvarImagen = obj["Imagen"] != DBNull.Value ?
                Convert.ToString(obj["Imagen"]) : null;

            mvarSeccion = obj["Seccion"] != DBNull.Value ?
                Convert.ToString(obj["Seccion"]) : null;

            mvarOrden = obj["Orden"] != DBNull.Value ?
                Convert.ToInt32(obj["Orden"]) : (Int32?)null;

            mvarFecha_Creacion = obj["Fecha_Creacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Creacion"]) : (DateTime?)null;

            mvarFecha_Actualizacion = obj["Fecha_Actualizacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Actualizacion"]) : (DateTime?)null;

            mvarUsuario_Creacion = obj["Usuario_Creacion"] != DBNull.Value ?
                Convert.ToInt32(obj["Usuario_Creacion"]) : (Int32?)null;

            mvarUsuario_Actualizacion = obj["Usuario_Actualizacion"] != DBNull.Value ?
                Convert.ToInt32(obj["Usuario_Actualizacion"]) : (Int32?)null;

            mvarEstado = obj["Estado"] != DBNull.Value ?
                Convert.ToBoolean(obj["Estado"]) : (Boolean?)null;
        }

        #endregion
    }
}
