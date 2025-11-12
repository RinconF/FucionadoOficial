using System;
using System.Data;
namespace DCL
{
    public class Int_Usuarios
    {
        #region Propiedades 
        Int32? mvarId_Usuario = null;
        public Int32? Id_Usuario
        {
            get { return mvarId_Usuario; }
            set { mvarId_Usuario = value; }
        }
        String mvarUsuario = null;
        public String Usuario
        {
            get { return mvarUsuario; }
            set { mvarUsuario = value; }
        }
        String mvarContraseña = null;
        public String Contraseña
        {
            get { return mvarContraseña; }
            set { mvarContraseña = value; }
        }
        String mvarUsuario_Creacion = null;
        public String Usuario_Creacion
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
        String mvarUsuario_Actualizacion = null;
        public String Usuario_Actualizacion
        {
            get { return mvarUsuario_Actualizacion; }
            set { mvarUsuario_Actualizacion = value; }
        }
        String mvarFecha_Actualizacion = null;
        public String Fecha_Actualizacion
        {
            get { return mvarFecha_Actualizacion; }
            set { mvarFecha_Actualizacion = value; }
        }
        String mvarAnexo_Foto = null;
        public String Anexo_Foto
        {
            get { return mvarAnexo_Foto; }
            set { mvarAnexo_Foto = value; }
        }

        Int32? mvarId_Rol = null;
        public Int32? Id_Rol
        {
            get { return mvarId_Rol; }
            set { mvarId_Rol = value; }
        }

        Int32? mvarInicio = null;
        public Int32? Inicio
        {
            get { return mvarInicio; }
            set { mvarInicio = value; }
        }

        Int32? mvarFin = null;
        public Int32? Fin
        {
            get { return mvarFin; }
            set { mvarFin = value; }
        }
        #endregion
        #region Constructores 
        public Int_Usuarios() { }
        public Int_Usuarios(
            Int32? varId_Usuario,
            String varUsuario,
            String varContraseña,
            String varUsuario_Creacion,
            String varFecha_Creacion,
            String varUsuario_Actualizacion,
            String varFecha_Actualizacion,
            String varAnexo_Foto,
            Int32? varId_Rol,
            Int32? varInicio,
            Int32? varFin
            )
        {
            mvarId_Usuario = varId_Usuario;
            mvarUsuario = varUsuario;
            mvarContraseña = varContraseña;
            mvarUsuario_Creacion = varUsuario_Creacion;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarUsuario_Actualizacion = varUsuario_Actualizacion;
            mvarFecha_Actualizacion = varFecha_Actualizacion;
            mvarAnexo_Foto = varAnexo_Foto;
            mvarId_Rol = varId_Rol;
            mvarInicio = varInicio;
            mvarFin = varFin;
        }
        public Int_Usuarios(IDataRecord obj)
        {
            mvarId_Usuario = Convert.ToInt32(obj["Id_Usuario"]);
            mvarUsuario = Convert.ToString(obj["Usuario"]);
            mvarContraseña = Convert.ToString(obj["Contrase�a"]);
            mvarUsuario_Creacion = Convert.ToString(obj["Usuario_Creacion"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarUsuario_Actualizacion = Convert.ToString(obj["Usuario_Actualizacion"]);
            mvarFecha_Actualizacion = Convert.ToString(obj["Fecha_Actualizacion"]);
            mvarAnexo_Foto = Convert.ToString(obj["Anexo_Foto"]);
            mvarId_Rol = Convert.ToInt32(obj["Id_Rol"]);
            mvarInicio = Convert.ToInt32(obj["Inicio"]);
            mvarFin = Convert.ToInt32(obj["Fin"]);

        }
        public Int_Usuarios(DataRow obj)
        {
            mvarId_Usuario = Convert.ToInt32(obj["Id_Usuario"]);
            mvarUsuario = Convert.ToString(obj["Usuario"]);
            mvarContraseña = Convert.ToString(obj["Contrase�a"]);
            mvarUsuario_Creacion = Convert.ToString(obj["Usuario_Creacion"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarUsuario_Actualizacion = Convert.ToString(obj["Usuario_Actualizacion"]);
            mvarFecha_Actualizacion = Convert.ToString(obj["Fecha_Actualizacion"]);
            mvarAnexo_Foto = Convert.ToString(obj["Anexo_Foto"]);
            mvarId_Rol = Convert.ToInt32(obj["Id_Rol"]);
            mvarInicio = Convert.ToInt32(obj["Inicio"]);
            mvarFin = Convert.ToInt32(obj["Fin"]);
        }
        #endregion
    }

}