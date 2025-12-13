using System;
using System.Data;

namespace DCL
{
    public class Int_Documentos
    {
        #region Propiedades

        Int32? mvarId_Documentos = null;
        public Int32? Id_Documentos
        {
            get { return mvarId_Documentos; }
            set { mvarId_Documentos = value; }
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

        String mvarArchivo = null;
        public String Archivo
        {
            get { return mvarArchivo; }
            set { mvarArchivo = value; }
        }

        String mvarUrl = null;
        public String Url
        {
            get { return mvarUrl; }
            set { mvarUrl = value; }
        }

        DateTime? mvarFechaCreacion = null;
        public DateTime? FechaCreacion
        {
            get { return mvarFechaCreacion; }
            set { mvarFechaCreacion = value; }
        }

        DateTime? mvarFechaActualizacion = null;
        public DateTime? FechaActualizacion
        {
            get { return mvarFechaActualizacion; }
            set { mvarFechaActualizacion = value; }
        }

        Int32? mvarUsuarioCreacion = null;
        public Int32? UsuarioCreacion
        {
            get { return mvarUsuarioCreacion; }
            set { mvarUsuarioCreacion = value; }
        }

        Int32? mvarUsuarioActualizacion = null;
        public Int32? UsuarioActualizacion
        {
            get { return mvarUsuarioActualizacion; }
            set { mvarUsuarioActualizacion = value; }
        }

        Boolean? mvarEstado = null;
        public Boolean? Estado
        {
            get { return mvarEstado; }
            set { mvarEstado = value; }
        }

        #endregion

        #region Constructores

        public Int_Documentos() { }

        public Int_Documentos(
            Int32? varId_Documentos,
            String varTitulo,
            String varDescripcion,
            String varArchivo,
            String varUrl,
            DateTime? varFechaCreacion,
            DateTime? varFechaActualizacion,
            Int32? varUsuarioCreacion,
            Int32? varUsuarioActualizacion,
            Boolean? varEstado
        )
        {
            mvarId_Documentos = varId_Documentos;
            mvarTitulo = varTitulo;
            mvarDescripcion = varDescripcion;
            mvarArchivo = varArchivo;
            mvarUrl = varUrl;
            mvarFechaCreacion = varFechaCreacion;
            mvarFechaActualizacion = varFechaActualizacion;
            mvarUsuarioCreacion = varUsuarioCreacion;
            mvarUsuarioActualizacion = varUsuarioActualizacion;
            mvarEstado = varEstado;
        }

        public Int_Documentos(IDataRecord obj)
        {
            mvarId_Documentos = obj["Id_Documentos"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Documentos"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarArchivo = obj["Archivo"] != DBNull.Value ?
                Convert.ToString(obj["Archivo"]) : null;

            mvarUrl = obj["Url"] != DBNull.Value ?
                Convert.ToString(obj["Url"]) : null;

            mvarFechaCreacion = obj["FechaCreacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["FechaCreacion"]) : (DateTime?)null;

            mvarFechaActualizacion = obj["FechaActualizacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["FechaActualizacion"]) : (DateTime?)null;

            mvarUsuarioCreacion = obj["UsuarioCreacion"] != DBNull.Value ?
                Convert.ToInt32(obj["UsuarioCreacion"]) : (Int32?)null;

            mvarUsuarioActualizacion = obj["UsuarioActualizacion"] != DBNull.Value ?
                Convert.ToInt32(obj["UsuarioActualizacion"]) : (Int32?)null;

            mvarEstado = obj["Estado"] != DBNull.Value ?
                Convert.ToBoolean(obj["Estado"]) : (Boolean?)null;
        }

        public Int_Documentos(DataRow obj)
        {
            mvarId_Documentos = obj["Id_Documentos"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Documentos"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarArchivo = obj["Archivo"] != DBNull.Value ?
                Convert.ToString(obj["Archivo"]) : null;

            mvarUrl = obj["Url"] != DBNull.Value ?
                Convert.ToString(obj["Url"]) : null;

            mvarFechaCreacion = obj["FechaCreacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["FechaCreacion"]) : (DateTime?)null;

            mvarFechaActualizacion = obj["FechaActualizacion"] != DBNull.Value ?
                Convert.ToDateTime(obj["FechaActualizacion"]) : (DateTime?)null;

            mvarUsuarioCreacion = obj["UsuarioCreacion"] != DBNull.Value ?
                Convert.ToInt32(obj["UsuarioCreacion"]) : (Int32?)null;

            mvarUsuarioActualizacion = obj["UsuarioActualizacion"] != DBNull.Value ?
                Convert.ToInt32(obj["UsuarioActualizacion"]) : (Int32?)null;

            mvarEstado = obj["Estado"] != DBNull.Value ?
                Convert.ToBoolean(obj["Estado"]) : (Boolean?)null;
        }

        #endregion
    }
}