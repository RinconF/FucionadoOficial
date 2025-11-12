using System;
using System.Data;
namespace DCL
{
    public class Int_Nucleo_Familiar
    {
        #region Propiedades 
        Int32? mvarId_Familiar = null;
        public Int32? Id_Familiar
        {
            get { return mvarId_Familiar; }
            set { mvarId_Familiar = value; }
        }

        Int32? mvarId_IE = null;
        public Int32? Id_IE
        {
            get { return mvarId_IE; }
            set { mvarId_IE = value; }
        }

        String mvarNombre_Familiar = null;
        public String Nombre_Familiar
        {
            get { return mvarNombre_Familiar; }
            set { mvarNombre_Familiar = value; }
        }

        String mvarId_Genero = null;
        public String Id_Genero
        {
            get { return mvarId_Genero; }
            set { mvarId_Genero = value; }
        }

        String mvarId_Tipo_Doc = null;
        public String Id_Tipo_Doc
        {
            get { return mvarId_Tipo_Doc; }
            set { mvarId_Tipo_Doc = value; }
        }

        String mvarIdentificacion = null;
        public String Identificacion
        {
            get { return mvarIdentificacion; }
            set { mvarIdentificacion = value; }
        }

        String mvarEdad = null;
        public String Edad
        {
            get { return mvarEdad; }
            set { mvarEdad = value; }
        }

        String mvarCelular = null;
        public String Celular
        {
            get { return mvarCelular; }
            set { mvarCelular = value; }
        }

        String mvarId_Parentesco = null;
        public String Id_Parentesco
        {
            get { return mvarId_Parentesco; }
            set { mvarId_Parentesco = value; }
        }

        String mvarId_Escolaridad = null;
        public String Id_Escolaridad
        {
            get { return mvarId_Escolaridad; }
            set { mvarId_Escolaridad = value; }
        }

        String mvarId_Ocupa = null;
        public String Id_Ocupa
        {
            get { return mvarId_Ocupa; }
            set { mvarId_Ocupa = value; }
        }

        String mvarDiscapacidad = null;
        public String Discapacidad
        {
            get { return mvarDiscapacidad; }
            set { mvarDiscapacidad = value; }
        }

        String mvarAnexo_Reg_Civil = null;
        public String Anexo_Reg_Civil
        {
            get { return mvarAnexo_Reg_Civil; }
            set { mvarAnexo_Reg_Civil = value; }
        }

        String mvarAnexo_Dos = null;
        public String Anexo_Dos
        {
            get { return mvarAnexo_Dos; }
            set { mvarAnexo_Dos = value; }
        }

        String mvarAnexo_Tres = null;
        public String Anexo_Tres
        {
            get { return mvarAnexo_Tres; }
            set { mvarAnexo_Tres = value; }
        }

        String mvarAnexo_Cuatro = null;
        public String Anexo_Cuatro
        {
            get { return mvarAnexo_Cuatro; }
            set { mvarAnexo_Cuatro = value; }
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
        #endregion
        #region Constructores 
        public Int_Nucleo_Familiar() { }
        public Int_Nucleo_Familiar(
            Int32? varId_Familiar,
            Int32? varId_IE,
            String varNombre_Familiar,
            String varId_Genero,
            String varId_Tipo_Doc,
            String varIdentificacion,
            String varEdad,
            String varCelular,
            String varId_Parentesco,
            String varId_Escolaridad,
            String varId_Ocupa,
            String varDiscapacidad,
            String varAnexo_Reg_Civil,
            String varAnexo_Dos,
            String varAnexo_Tres,
            String varAnexo_Cuatro,
            String varUsuario_Creacion,
            String varFecha_Creacion,
            String varUsuario_Actualizacion,
            String varFecha_Actualizacion
            )
        {
            mvarId_Familiar = varId_Familiar;
            mvarId_IE = varId_IE;
            mvarNombre_Familiar = varNombre_Familiar;
            mvarId_Genero = varId_Genero;
            mvarId_Tipo_Doc = varId_Tipo_Doc;
            mvarIdentificacion = varIdentificacion;
            mvarEdad = varEdad;
            mvarCelular = varCelular;
            mvarId_Parentesco = varId_Parentesco;
            mvarId_Escolaridad = varId_Escolaridad;
            mvarId_Ocupa = varId_Ocupa;
            mvarDiscapacidad = varDiscapacidad;
            mvarAnexo_Reg_Civil = varAnexo_Reg_Civil;
            mvarAnexo_Dos = varAnexo_Dos;
            mvarAnexo_Tres = varAnexo_Tres;
            mvarAnexo_Cuatro = varAnexo_Cuatro;
            mvarUsuario_Creacion = varUsuario_Creacion;
            mvarFecha_Creacion = varFecha_Creacion;
            mvarUsuario_Actualizacion = varUsuario_Actualizacion;
            mvarFecha_Actualizacion = varFecha_Actualizacion;
        }
        public Int_Nucleo_Familiar(IDataRecord obj)
        {

            mvarId_Familiar = Convert.ToInt32(obj["Id_Familiar"]); ;
            mvarId_IE = Convert.ToInt32(obj["Id_IE"]);
            mvarNombre_Familiar = Convert.ToString(obj["Nombre_Familiar"]);
            mvarId_Genero = Convert.ToString(obj["Id_Genero"]);
            mvarId_Tipo_Doc = Convert.ToString(obj["Id_Tipo_Doc"]);
            mvarIdentificacion = Convert.ToString(obj["Identificacion"]);
            mvarEdad = Convert.ToString(obj["Edad"]);
            mvarCelular = Convert.ToString(obj["Celular"]);
            mvarId_Parentesco = Convert.ToString(obj["Id_Parentesco"]);
            mvarId_Escolaridad = Convert.ToString(obj["Id_Escolaridad"]);
            mvarId_Ocupa = Convert.ToString(obj["Id_Ocupa"]);
            mvarDiscapacidad = Convert.ToString(obj["Discapacidad"]);
            mvarAnexo_Reg_Civil = Convert.ToString(obj["Anexo_Reg_Civil"]);
            mvarAnexo_Dos = Convert.ToString(obj["Anexo_Dos"]);
            mvarAnexo_Tres = Convert.ToString(obj["Anexo_Tres"]);
            mvarAnexo_Cuatro = Convert.ToString(obj["Anexo_Cuatro"]);
            mvarUsuario_Creacion = Convert.ToString(obj["Usuario_Creacion"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarUsuario_Actualizacion = Convert.ToString(obj["Usuario_Actualizacion"]);
            mvarFecha_Actualizacion = Convert.ToString(obj["Fecha_Actualizacion"]);

        }
        public Int_Nucleo_Familiar(DataRow obj)
        {
            mvarId_Familiar = Convert.ToInt32(obj["Id_Familiar"]); ;
            mvarId_IE = Convert.ToInt32(obj["Id_IE"]);
            mvarNombre_Familiar = Convert.ToString(obj["Nombre_Familiar"]);
            mvarId_Genero = Convert.ToString(obj["Id_Genero"]);
            mvarId_Tipo_Doc = Convert.ToString(obj["Id_Tipo_Doc"]);
            mvarIdentificacion = Convert.ToString(obj["Identificacion"]);
            mvarEdad = Convert.ToString(obj["Edad"]);
            mvarCelular = Convert.ToString(obj["Celular"]);
            mvarId_Parentesco = Convert.ToString(obj["Id_Parentesco"]);
            mvarId_Escolaridad = Convert.ToString(obj["Id_Escolaridad"]);
            mvarId_Ocupa = Convert.ToString(obj["Id_Ocupa"]);
            mvarDiscapacidad = Convert.ToString(obj["Discapacidad"]);
            mvarAnexo_Reg_Civil = Convert.ToString(obj["Anexo_Reg_Civil"]);
            mvarAnexo_Dos = Convert.ToString(obj["Anexo_Dos"]);
            mvarAnexo_Tres = Convert.ToString(obj["Anexo_Tres"]);
            mvarAnexo_Cuatro = Convert.ToString(obj["Anexo_Cuatro"]);
            mvarUsuario_Creacion = Convert.ToString(obj["Usuario_Creacion"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mvarUsuario_Actualizacion = Convert.ToString(obj["Usuario_Actualizacion"]);
            mvarFecha_Actualizacion = Convert.ToString(obj["Fecha_Actualizacion"]);

        }
        #endregion
    }

}
