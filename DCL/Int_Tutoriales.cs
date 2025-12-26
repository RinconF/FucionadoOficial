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

        String mvarVideo = null;
        public String Video
        {
            get { return mvarVideo; }
            set { mvarVideo = value; }
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

        /// <summary>
        /// Roles para filtrar/asignar. Formato: "1" o "1,2,3" (IDs separados por coma)
        /// Usado en Actions 1, 6, 7, 8, 9
        /// </summary>
        String mvarRoles = null;
        public String Roles
        {
            get { return mvarRoles; }
            set { mvarRoles = value; }
        }

        /// <summary>
        /// Roles asignados al tutorial (solo lectura desde Action 0)
        /// Viene como string concatenado del SP, ejemplo: "Admin, Usuario, Supervisor"
        /// </summary>
        String mvarRoles_Asignados = null;
        public String Roles_Asignados
        {
            get { return mvarRoles_Asignados; }
            set { mvarRoles_Asignados = value; }
        }

        #endregion

        #region Constructores

        public Int_Tutoriales() { }

        public Int_Tutoriales(
            Int32? varId_Tutorial,
            String varTitulo,
            String varDescripcion,
            String varVideo,
            String varSeccion,
            DateTime? varFecha_Creacion,
            DateTime? varFecha_Actualizacion,
            Int32? varUsuario_Creacion,
            Int32? varUsuario_Actualizacion,
            Boolean? varEstado,
            String varRoles = null,
            String varRoles_Asignados = null
        )
        {
            mvarId_Tutorial = varId_Tutorial;
            mvarTitulo = varTitulo;
            mvarDescripcion = varDescripcion;
            mvarVideo = varVideo;
            mvarSeccion = varSeccion;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarFecha_Actualizacion = varFecha_Actualizacion;
            mvarUsuario_Creacion = varUsuario_Creacion;
            mvarUsuario_Actualizacion = varUsuario_Actualizacion;
            mvarEstado = varEstado;
            mvarRoles = varRoles;
            mvarRoles_Asignados = varRoles_Asignados;
        }

        public Int_Tutoriales(IDataRecord obj)
        {
            mvarId_Tutorial = obj["Id_Tutorial"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Tutorial"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarVideo = obj["Video"] != DBNull.Value ?
                Convert.ToString(obj["Video"]) : null;

            mvarSeccion = obj["Seccion"] != DBNull.Value ?
                Convert.ToString(obj["Seccion"]) : null;

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

            try
            {
                mvarRoles_Asignados = obj["Roles_Asignados"] != DBNull.Value ?
                    Convert.ToString(obj["Roles_Asignados"]) : null;
            }
            catch
            {
                // Columna no existe en otros Actions
                mvarRoles_Asignados = null;
            }
        }

        public Int_Tutoriales(DataRow obj)
        {
            mvarId_Tutorial = obj["Id_Tutorial"] != DBNull.Value ?
                Convert.ToInt32(obj["Id_Tutorial"]) : (Int32?)null;

            mvarTitulo = obj["Titulo"] != DBNull.Value ?
                Convert.ToString(obj["Titulo"]) : null;

            mvarDescripcion = obj["Descripcion"] != DBNull.Value ?
                Convert.ToString(obj["Descripcion"]) : null;

            mvarVideo = obj["Video"] != DBNull.Value ?
                Convert.ToString(obj["Video"]) : null;

            mvarSeccion = obj["Seccion"] != DBNull.Value ?
                Convert.ToString(obj["Seccion"]) : null;

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

            if (obj.Table.Columns.Contains("Roles_Asignados"))
            {
                mvarRoles_Asignados = obj["Roles_Asignados"] != DBNull.Value ?
                    Convert.ToString(obj["Roles_Asignados"]) : null;
            }
        }

        #endregion
    }
}