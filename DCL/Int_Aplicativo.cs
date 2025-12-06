using System;
using System.Data;

namespace DCL
{
    public class Int_Aplicativo
    {
        #region Propiedades

        Int32? mvarId_Aplicativo = null;
        public Int32? Id_Aplicativo
        {
            get { return mvarId_Aplicativo; }
            set { mvarId_Aplicativo = value; }
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

        String mvarSeccion = null;
        public String Seccion
        {
            get { return mvarSeccion; }
            set { mvarSeccion = value; }
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

        Boolean? mvarEstado = null;
        public Boolean? Estado
        {
            get { return mvarEstado; }
            set { mvarEstado = value; }
        }

        #endregion

        #region Constructores

        public Int_Aplicativo() { }

        public Int_Aplicativo(
            Int32? varId_Aplicativo,
            String varTitulo,
            String varDescripcion,
            String varImagen,
            String varUrl,
            String varSeccion,
            DateTime? varFecha_Creacion,
            DateTime? varFecha_Actualizacion,
            Boolean? varEstado
        )
        {
            mvarId_Aplicativo = varId_Aplicativo;
            mvarTitulo = varTitulo;
            mvarDescripcion = varDescripcion;
            mvarImagen = varImagen;
            mvarUrl = varUrl;
            mvarSeccion = varSeccion;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarFecha_Actualizacion = varFecha_Actualizacion;
            mvarEstado = varEstado;
        }

        public Int_Aplicativo(IDataRecord obj)
        {
            mvarId_Aplicativo = obj["Id_Aplicativo"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Aplicativo"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarImagen = obj["Imagen"] != DBNull.Value ?
                Convert.ToString(obj["Imagen"]) : null;

            mvarUrl = obj["Url"] != DBNull.Value ?
                Convert.ToString(obj["Url"]) : null;

            mvarSeccion = obj["Seccion"] != DBNull.Value ?
                Convert.ToString(obj["Seccion"]) : null;

            mvarFecha_Creacion = obj["Fecha_Creacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Creacion"]) : (DateTime?)null;

            mvarFecha_Actualizacion = obj["Fecha_Actualizacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Actualizacion"]) : (DateTime?)null;

            mvarEstado = obj["Estado"] != DBNull.Value ?
                Convert.ToBoolean(obj["Estado"]) : (Boolean?)null;
        }

        public Int_Aplicativo(DataRow obj)
        {
            mvarId_Aplicativo = obj["Id_Aplicativo"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Aplicativo"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarImagen = obj["Imagen"] != DBNull.Value ?
                Convert.ToString(obj["Imagen"]) : null;

            mvarUrl = obj["Url"] != DBNull.Value ?
                Convert.ToString(obj["Url"]) : null;

            mvarSeccion = obj["Seccion"] != DBNull.Value ?
                Convert.ToString(obj["Seccion"]) : null;

            mvarFecha_Creacion = obj["Fecha_Creacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Creacion"]) : (DateTime?)null;

            mvarFecha_Actualizacion = obj["Fecha_Actualizacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["Fecha_Actualizacion"]) : (DateTime?)null;

            mvarEstado = obj["Estado"] != DBNull.Value ?
                Convert.ToBoolean(obj["Estado"]) : (Boolean?)null;
        }

        #endregion
    }
}
