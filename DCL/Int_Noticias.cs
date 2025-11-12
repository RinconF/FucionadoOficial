using System;
using System.Data;
namespace DCL
{
    public class Int_Noticias
    {
        #region Propiedades 
        Int32? mvarId_Noticia = null;
        public Int32? Id_Noticia
        {
            get { return mvarId_Noticia; }
            set { mvarId_Noticia = value; }
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
        String mvarFecha_Creacion = null;
        public String Fecha_Creacion
        {
            get { return mvarFecha_Creacion; }
            set { mvarFecha_Creacion = value; }
        }
        Int32? mvarEstado = null;
        public Int32? Estadp
        {
            get { return mvarEstado; }
            set { mvarEstado = value; }
        }
        String mvarId_Visibilidad = null;
        public String Id_Visibilidad
        {
            get { return mvarId_Visibilidad; }
            set { mvarId_Visibilidad = value; }
        }
        #endregion
        #region Constructores 
        public Int_Noticias() { }
        public Int_Noticias(
            Int32? varId_Noticia,
            String varTitulo,
            String varDescripcion,
            String varImagen,
            String varUrl,
            String varFecha_Creacion,
            Int32? varEstado,
            String varId_Visibilidad
            )
        {
            mvarId_Noticia = varId_Noticia;
            mvarTitulo = varTitulo;
            mvarDescripcion = varDescripcion;
            mvarImagen = varImagen;
            mvarUrl = varUrl;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarEstado = varEstado;
            mvarId_Visibilidad = varId_Visibilidad;
        }
        public Int_Noticias(IDataRecord obj)
        {
            mvarId_Noticia = Convert.ToInt32(obj["Id_Noticia"]);
            mvarTitulo = Convert.ToString(obj["Titulo"]);
            mvarDescripcion = Convert.ToString(obj["Descripcion"]);
            mvarImagen = Convert.ToString(obj["Imagen"]);
            mvarUrl = Convert.ToString(obj["Url"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarEstado = Convert.ToInt32(obj["Estado"]);
            mvarId_Visibilidad = Convert.ToString(obj["Id_Visibilidad"]);

        }
        public Int_Noticias(DataRow obj)
        {
            mvarId_Noticia = Convert.ToInt32(obj["Id_Noticia"]);
            mvarTitulo = Convert.ToString(obj["Titulo"]);
            mvarDescripcion = Convert.ToString(obj["Descripcion"]);
            mvarImagen = Convert.ToString(obj["Imagen"]);
            mvarUrl = Convert.ToString(obj["Url"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarEstado = Convert.ToInt32(obj["Estado"]);
            mvarId_Visibilidad = Convert.ToString(obj["Id_Visibilidad"]);
        }
        #endregion
    }

}
