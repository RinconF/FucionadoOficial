using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCL
{
    public class ENC_Encuesta
    {
        #region Propiedades 
        Int32? mvarId_Encuesta = null;
        public Int32? Id_Encuesta
        {
            get { return mvarId_Encuesta; }
            set { mvarId_Encuesta = value; }
        }
        String mvarNombre = null;
        public String Nombre
        {
            get { return mvarNombre; }
            set { mvarNombre = value; }
        }
        String mvarDescripcion = null;
        public String Descripcion
        {
            get { return mvarDescripcion; }
            set { mvarDescripcion = value; }
        }
        Int32? mvarId_Estado = null;
        public Int32? Id_Estado
        {
            get { return mvarId_Estado; }
            set { mvarId_Estado = value; }
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
        String mvarFecha_Actualiza = null;
        public String Fecha_Actualiza
        {
            get { return mvarFecha_Actualiza; }
            set { mvarFecha_Actualiza = value; }
        }
        #endregion
        #region Constructores 
        public ENC_Encuesta() { }
        public ENC_Encuesta(Int32? varId_Encuesta, String varNombre, String varDescripcion, Int32? varId_Estado, Int32? varUsuario_Creacion, String varFecha_Creacion, Int32? varUsuario_Actualiza, String varFecha_Actualiza)
        {
            mvarId_Encuesta = varId_Encuesta; mvarNombre = varNombre; mvarDescripcion = varDescripcion; mvarId_Estado = varId_Estado; mvarUsuario_Creacion = varUsuario_Creacion; mvarFecha_Creacion = varFecha_Creacion; mvarUsuario_Actualiza = varUsuario_Actualiza; mvarFecha_Actualiza = varFecha_Actualiza;
        }
        public ENC_Encuesta(IDataRecord obj)
        {
            mvarId_Encuesta = Convert.ToInt32(obj["Id_Encuesta"]);
            mvarNombre = Convert.ToString(obj["Nombre"]);
            mvarDescripcion = Convert.ToString(obj["Descripcion"]);
            mvarId_Estado = Convert.ToInt32(obj["Id_Estado"]);
            mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]);
            mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]);

        }
        public ENC_Encuesta(DataRow obj)
        {
            mvarId_Encuesta = Convert.ToInt32(obj["Id_Encuesta"]);
            mvarNombre = Convert.ToString(obj["Nombre"]);
            mvarDescripcion = Convert.ToString(obj["Descripcion"]);
            mvarId_Estado = Convert.ToInt32(obj["Id_Estado"]);
            mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]);
            mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]);

        }
        #endregion
    }
}
