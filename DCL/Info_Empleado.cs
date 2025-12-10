using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace DCL
{
    public class Info_Empleado
    {
        #region Constructores
        public Info_Empleado() { }
        public Info_Empleado(Int32? numId_IE, Int32? numId_TipoDoc, String strN_Identificacion, String strCodigo_Sae, String strNombres, String strApellido1, String strApellido2,
            Int32? numId_Sexo, String strFecha_Nacimiento, Int32? numId_PaisNcto, Int32? numId_DptoNcto, Int32? numId_CiudadNcto, Int32? numId_Estado, Int32? numId_EstadoCivil,
            Int32? numId_RH, String strFec_ExpDoc, Int32? numId_Pais_ExpDoc, Int32? numId_DptoExpDoc, Int32? numId_CiudadExpDoc, String strFec_VenceDoc, String strDpto_Residencia,
            Int32? numId_CiudadResidencia, Int32? numId_LocalidadResidencia, Int32? numId_BarrioResidencia, String strDir_Residencia, String strCelular, String strTelefono,
            String strEmail_Personal, String strEmail_Empresa, Int32? numId_ExperienciaLaboral, String strObservaciones, String strFecha_Creacion, Int32? numId_UsuarioCreacion,
            String strFecha_Actualizacion, Int32? numId_UsuarioActualizacion, Int32? numId_AspiracionSalarial, Int32? numId_Edu, Int32? numId_CargoAspira, Int32? numId_CargoSeleccion,
            Int32? numId_GrupoEmpleado, String strRuta_HV, Boolean blnDataOk, String strCentrodeCostos, String strCargo, String strFecha_Creacionf, String strSede, String strCargoCont,
            String _strFecha_Inicio_Turno, String _strFecha_Fin_Turno
            )
        {
            mnumId_IE = numId_IE;
            mnumId_TipoDoc = numId_TipoDoc;
            mstrN_Identificacion = strN_Identificacion;
            mstrCodigo_Sae = strCodigo_Sae;
            mstrNombres = strNombres;
            mstrApellido1 = strApellido1;
            mstrApellido2 = strApellido2;
            mnumId_Sexo = numId_Sexo;
            mstrFecha_Nacimiento = strFecha_Nacimiento;
            mnumId_PaisNcto = numId_PaisNcto;
            mnumId_DptoNcto = numId_DptoNcto;
            mnumId_CiudadNcto = numId_CiudadNcto;
            mnumId_Estado = numId_Estado;
            mnumId_EstadoCivil = numId_EstadoCivil;
            mnumId_RH = numId_RH;
            mstrFec_ExpDoc = strFec_ExpDoc;
            mnumId_Pais_ExpDoc = numId_Pais_ExpDoc;
            mnumId_DptoExpDoc = numId_DptoExpDoc;
            mnumId_CiudadExpDoc = numId_CiudadExpDoc;
            mstrFec_VenceDoc = strFec_VenceDoc;
            mstrDpto_Residencia = strDpto_Residencia;
            mnumId_CiudadResidencia = numId_CiudadResidencia;
            mnumId_LocalidadResidencia = numId_LocalidadResidencia;
            mnumId_BarrioResidencia = numId_BarrioResidencia;
            mstrDir_Residencia = strDir_Residencia;
            mstrCelular = strCelular;
            mstrTelefono = strTelefono;
            mstrEmail_Personal = strEmail_Personal;
            mstrEmail_Empresa = strEmail_Empresa;
            mnumId_ExperienciaLaboral = numId_ExperienciaLaboral;
            mstrObservaciones = strObservaciones;
            mstrFecha_Creacion = strFecha_Creacion;
            mnumId_UsuarioCreacion = numId_UsuarioCreacion;
            mstrFecha_Actualizacion = strFecha_Actualizacion;
            mnumId_UsuarioActualizacion = numId_UsuarioActualizacion;
            mnumId_AspiracionSalarial = numId_AspiracionSalarial;
            mnumId_Edu = numId_Edu;
            mnumId_CargoAspira = numId_CargoAspira;
            mnumId_CargoSeleccion = numId_CargoSeleccion;
            mnumId_GrupoEmpleado = numId_GrupoEmpleado;
            mstrRuta_HV = strRuta_HV;
            mblnDataOK = blnDataOk;
            mstrCentrodeCostos = strCentrodeCostos;
            mstrCargo = strCargo;
            mstrFecha_Creacionf = strFecha_Creacionf;
            mstrSede = strSede;
            mstrCargoCont = strCargoCont;
            mstrFecha_Inicio_Turno = _strFecha_Inicio_Turno;
            mstrFecha_Fin_Turno = _strFecha_Fin_Turno;

        }
        public Info_Empleado(IDataRecord obj)
        {
            mnumId_IE = Convert.ToInt32(obj["Id_IE"]);
            mnumId_TipoDoc = Convert.ToInt32(obj["Id_TipoDoc"]);
            mstrN_Identificacion = Convert.ToString(obj["N_Identificacion"]);
            mstrCodigo_Sae = Convert.ToString(obj["Codigo_Sae"]);
            mstrNombres = Convert.ToString(obj["Nombres"]);
            mstrApellido1 = Convert.ToString(obj["Apellido1"]);
            mstrApellido2 = Convert.ToString(obj["Apellido2"]);
            mnumId_Sexo = Convert.ToInt32(obj["Id_Sexo"]);
            mstrFecha_Nacimiento = Convert.ToString(obj["Fecha_Nacimiento"]);
            mnumId_PaisNcto = Convert.ToInt32(obj["Id_PaisNcto"]);
            mnumId_DptoNcto = Convert.ToInt32(obj["Id_DptoNcto"]);
            mnumId_CiudadNcto = Convert.ToInt32(obj["Id_CiudadNcto"]);
            mnumId_Estado = Convert.ToInt32(obj["Id_Estado"]);
            mnumId_EstadoCivil = Convert.ToInt32(obj["Id_EstadoCivil"]);
            mnumId_RH = Convert.ToInt32(obj["Id_RH"]);
            mstrFec_ExpDoc = Convert.ToString(obj["Fec_ExpDoc"]);
            mnumId_Pais_ExpDoc = Convert.ToInt32(obj["Id_Pais_ExpDoc"]);
            mnumId_DptoExpDoc = Convert.ToInt32(obj["Id_DptoExpDoc"]);
            mnumId_CiudadExpDoc = Convert.ToInt32(obj["Id_CiudadExpDoc"]);
            mstrFec_VenceDoc = Convert.ToString(obj["Fec_VenceDoc"]);
            mstrDpto_Residencia = Convert.ToString(obj["Dpto_Residencia"]);
            mnumId_CiudadResidencia = Convert.ToInt32(obj["Id_CiudadResidencia"]);
            mnumId_LocalidadResidencia = Convert.ToInt32(obj["Id_LocalidadResidencia"]);
            mnumId_BarrioResidencia = Convert.ToInt32(obj["Id_BarrioResidencia"]);
            mstrDir_Residencia = Convert.ToString(obj["Dir_Residencia"]);
            mstrCelular = Convert.ToString(obj["Celular"]);
            mstrTelefono = Convert.ToString(obj["Telefono"]);
            mstrEmail_Personal = Convert.ToString(obj["Email_Personal"]);
            mstrEmail_Empresa = Convert.ToString(obj["Email_Empresa"]);
            mnumId_ExperienciaLaboral = Convert.ToInt32(obj["Id_ExperienciaLaboral"]);
            mstrObservaciones = Convert.ToString(obj["Observaciones"]);
            mstrFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mnumId_UsuarioCreacion = Convert.ToInt32(obj["Id_UsuarioCreacion"]);
            mstrFecha_Actualizacion = Convert.ToString(obj["Fecha_Actualizacion"]);
            mnumId_UsuarioActualizacion = Convert.ToInt32(obj["Id_UsuarioActualizacion"]);
            mnumId_AspiracionSalarial = Convert.ToInt32(obj["Id_AspiracionSalarial"]);
            mnumId_Edu = Convert.ToInt32(obj["Id_Edu"]);
            mnumId_CargoAspira = Convert.ToInt32(obj["Id_CargoAspira"]);
            mnumId_CargoSeleccion = Convert.ToInt32(obj["Id_CargoSeleccion"]);
            mnumId_GrupoEmpleado = Convert.ToInt32(obj["Id_GrupoEmpleado"]);
            mstrRuta_HV = Convert.ToString(obj["Ruta_HV"]);
            mblnDataOK = Convert.ToBoolean(obj["DataOK"]);
            mstrCentrodeCostos = Convert.ToString(obj["CentrodeCostos"]);
            mstrCargo = Convert.ToString(obj["Cargo"]);
            mstrFecha_Creacionf = Convert.ToString(obj["Fecha_Creacionf"]);
            mstrSede = Convert.ToString(obj["Sede"]);
            mstrCargoCont = Convert.ToString(obj["CargoContains"]);
        }
        public Info_Empleado(DataRow obj)
        {
            mnumId_IE = Convert.ToInt32(obj["Id_IE"]);
            mnumId_TipoDoc = Convert.ToInt32(obj["Id_TipoDoc"]);
            mstrN_Identificacion = Convert.ToString(obj["N_Identificacion"]);
            mstrCodigo_Sae = Convert.ToString(obj["Codigo_Sae"]);
            mstrNombres = Convert.ToString(obj["Nombres"]);
            mstrApellido1 = Convert.ToString(obj["Apellido1"]);
            mstrApellido2 = Convert.ToString(obj["Apellido2"]);
            mnumId_Sexo = Convert.ToInt32(obj["Id_Sexo"]);
            mstrFecha_Nacimiento = Convert.ToString(obj["Fecha_Nacimiento"]);
            mnumId_PaisNcto = Convert.ToInt32(obj["Id_PaisNcto"]);
            mnumId_DptoNcto = Convert.ToInt32(obj["Id_DptoNcto"]);
            mnumId_CiudadNcto = Convert.ToInt32(obj["Id_CiudadNcto"]);
            mnumId_Estado = Convert.ToInt32(obj["Id_Estado"]);
            mnumId_EstadoCivil = Convert.ToInt32(obj["Id_EstadoCivil"]);
            mnumId_RH = Convert.ToInt32(obj["Id_RH"]);
            mstrFec_ExpDoc = Convert.ToString(obj["Fec_ExpDoc"]);
            mnumId_Pais_ExpDoc = Convert.ToInt32(obj["Id_Pais_ExpDoc"]);
            mnumId_DptoExpDoc = Convert.ToInt32(obj["Id_DptoExpDoc"]);
            mnumId_CiudadExpDoc = Convert.ToInt32(obj["Id_CiudadExpDoc"]);
            mstrFec_VenceDoc = Convert.ToString(obj["Fec_VenceDoc"]);
            mstrDpto_Residencia = Convert.ToString(obj["Dpto_Residencia"]);
            mnumId_CiudadResidencia = Convert.ToInt32(obj["Id_CiudadResidencia"]);
            mnumId_LocalidadResidencia = Convert.ToInt32(obj["Id_LocalidadResidencia"]);
            mnumId_BarrioResidencia = Convert.ToInt32(obj["Id_BarrioResidencia"]);
            mstrDir_Residencia = Convert.ToString(obj["Dir_Residencia"]);
            mstrCelular = Convert.ToString(obj["Celular"]);
            mstrTelefono = Convert.ToString(obj["Telefono"]);
            mstrEmail_Personal = Convert.ToString(obj["Email_Personal"]);
            mstrEmail_Empresa = Convert.ToString(obj["Email_Empresa"]);
            mnumId_ExperienciaLaboral = Convert.ToInt32(obj["Id_ExperienciaLaboral"]);
            mstrObservaciones = Convert.ToString(obj["Observaciones"]);
            mstrFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
            mnumId_UsuarioCreacion = Convert.ToInt32(obj["Id_UsuarioCreacion"]);
            mstrFecha_Actualizacion = Convert.ToString(obj["Fecha_Actualizacion"]);
            mnumId_UsuarioActualizacion = Convert.ToInt32(obj["Id_UsuarioActualizacion"]);
            mnumId_AspiracionSalarial = Convert.ToInt32(obj["Id_AspiracionSalarial"]);
            mnumId_Edu = Convert.ToInt32(obj["Id_Edu"]);
            mnumId_CargoAspira = Convert.ToInt32(obj["Id_CargoAspira"]);
            mnumId_CargoSeleccion = Convert.ToInt32(obj["Id_CargoSeleccion"]);
            mnumId_GrupoEmpleado = Convert.ToInt32(obj["Id_GrupoEmpleado"]);
            mstrRuta_HV = Convert.ToString(obj["Ruta_HV"]);
            mblnDataOK = Convert.ToBoolean(obj["DataOK"]);
            mstrCentrodeCostos = Convert.ToString(obj["CentrodeCostos"]);
            mstrCargo = Convert.ToString(obj["Cargo"]);
            mstrFecha_Creacionf = Convert.ToString(obj["Fecha_Creacionf"]);
            mstrSede = Convert.ToString(obj["Sede"]);
            mstrCargoCont = Convert.ToString(obj["CargoContains"]);
        }
        #endregion

        #region Propiedades

        Int32? mnumId_IE = null;
        public Int32? Id_IE
        {
            get { return mnumId_IE; }
            set { mnumId_IE = value; }
        }
        Int32? mnumId_TipoDoc = null;
        public Int32? Id_TipoDoc
        {
            get { return mnumId_TipoDoc; }
            set { mnumId_TipoDoc = value; }
        }
        String mstrN_Identificacion = null;
        public String N_Identificacion
        {
            get { return mstrN_Identificacion; }
            set { mstrN_Identificacion = value; }
        }
        String mstrCodigo_Sae = null;
        public String Codigo_Sae
        {
            get { return mstrCodigo_Sae; }
            set { mstrCodigo_Sae = value; }
        }
        String mstrNombres = null;
        public String Nombres
        {
            get { return mstrNombres; }
            set { mstrNombres = value; }
        }
        String mstrApellido1 = null;
        public String Apellido1
        {
            get { return mstrApellido1; }
            set { mstrApellido1 = value; }
        }
        String mstrApellido2 = null;
        public String Apellido2
        {
            get { return mstrApellido2; }
            set { mstrApellido2 = value; }
        }
        Int32? mnumId_Sexo = null;
        public Int32? Id_Sexo
        {
            get { return mnumId_Sexo; }
            set { mnumId_Sexo = value; }
        }
        String mstrFecha_Nacimiento = null;
        public String Fecha_Nacimiento
        {
            get { return mstrFecha_Nacimiento; }
            set { mstrFecha_Nacimiento = value; }
        }
        Int32? mnumId_PaisNcto = null;
        public Int32? Id_PaisNcto
        {
            get { return mnumId_PaisNcto; }
            set { mnumId_PaisNcto = value; }
        }
        Int32? mnumId_DptoNcto = null;
        public Int32? Id_DptoNcto
        {
            get { return mnumId_DptoNcto; }
            set { mnumId_DptoNcto = value; }
        }
        Int32? mnumId_CiudadNcto = null;
        public Int32? Id_CiudadNcto
        {
            get { return mnumId_CiudadNcto; }
            set { mnumId_CiudadNcto = value; }
        }
        Int32? mnumId_Estado = null;
        public Int32? Id_Estado
        {
            get { return mnumId_Estado; }
            set { mnumId_Estado = value; }
        }
        Int32? mnumId_EstadoCivil = null;
        public Int32? Id_EstadoCivil
        {
            get { return mnumId_EstadoCivil; }
            set { mnumId_EstadoCivil = value; }
        }
        Int32? mnumId_RH = null;
        public Int32? Id_RH
        {
            get { return mnumId_RH; }
            set { mnumId_RH = value; }
        }
        String mstrFec_ExpDoc = null;
        public String Fec_ExpDoc
        {
            get { return mstrFec_ExpDoc; }
            set { mstrFec_ExpDoc = value; }
        }
        Int32? mnumId_Pais_ExpDoc = null;
        public Int32? Id_Pais_ExpDoc
        {
            get { return mnumId_Pais_ExpDoc; }
            set { mnumId_Pais_ExpDoc = value; }
        }
        Int32? mnumId_DptoExpDoc = null;
        public Int32? Id_DptoExpDoc
        {
            get { return mnumId_DptoExpDoc; }
            set { mnumId_DptoExpDoc = value; }
        }
        Int32? mnumId_CiudadExpDoc = null;
        public Int32? Id_CiudadExpDoc
        {
            get { return mnumId_CiudadExpDoc; }
            set { mnumId_CiudadExpDoc = value; }
        }
        String mstrFec_VenceDoc = null;
        public String Fec_VenceDoc
        {
            get { return mstrFec_VenceDoc; }
            set { mstrFec_VenceDoc = value; }
        }
        String mstrDpto_Residencia = null;
        public String Dpto_Residencia
        {
            get { return mstrDpto_Residencia; }
            set { mstrDpto_Residencia = value; }
        }
        Int32? mnumId_CiudadResidencia = null;
        public Int32? Id_CiudadResidencia
        {
            get { return mnumId_CiudadResidencia; }
            set { mnumId_CiudadResidencia = value; }
        }
        Int32? mnumId_LocalidadResidencia = null;
        public Int32? Id_LocalidadResidencia
        {
            get { return mnumId_LocalidadResidencia; }
            set { mnumId_LocalidadResidencia = value; }
        }
        Int32? mnumId_BarrioResidencia = null;
        public Int32? Id_BarrioResidencia
        {
            get { return mnumId_BarrioResidencia; }
            set { mnumId_BarrioResidencia = value; }
        }
        String mstrDir_Residencia = null;
        public String Dir_Residencia
        {
            get { return mstrDir_Residencia; }
            set { mstrDir_Residencia = value; }
        }
        String mstrCelular = null;
        public String Celular
        {
            get { return mstrCelular; }
            set { mstrCelular = value; }
        }
        String mstrTelefono = null;
        public String Telefono
        {
            get { return mstrTelefono; }
            set { mstrTelefono = value; }
        }
        String mstrEmail_Personal = null;
        public String Email_Personal
        {
            get { return mstrEmail_Personal; }
            set { mstrEmail_Personal = value; }
        }
        String mstrEmail_Empresa = null;
        public String Email_Empresa
        {
            get { return mstrEmail_Empresa; }
            set { mstrEmail_Empresa = value; }
        }
        Int32? mnumId_ExperienciaLaboral = null;
        public Int32? Id_ExperienciaLaboral
        {
            get { return mnumId_ExperienciaLaboral; }
            set { mnumId_ExperienciaLaboral = value; }
        }
        String mstrObservaciones = null;
        public String Observaciones
        {
            get { return mstrObservaciones; }
            set { mstrObservaciones = value; }
        }
        String mstrFecha_Creacion = null;
        public String Fecha_Creacion
        {
            get { return mstrFecha_Creacion; }
            set { mstrFecha_Creacion = value; }
        }
        Int32? mnumId_UsuarioCreacion = null;
        public Int32? Id_UsuarioCreacion
        {
            get { return mnumId_UsuarioCreacion; }
            set { mnumId_UsuarioCreacion = value; }
        }
        String mstrFecha_Actualizacion = null;
        public String Fecha_Actualizacion
        {
            get { return mstrFecha_Actualizacion; }
            set { mstrFecha_Actualizacion = value; }
        }
        Int32? mnumId_UsuarioActualizacion = null;
        public Int32? Id_UsuarioActualizacion
        {
            get { return mnumId_UsuarioActualizacion; }
            set { mnumId_UsuarioActualizacion = value; }
        }
        Int32? mnumId_AspiracionSalarial = null;
        public Int32? Id_AspiracionSalarial
        {
            get { return mnumId_AspiracionSalarial; }
            set { mnumId_AspiracionSalarial = value; }
        }
        Int32? mnumId_Edu = null;
        public Int32? Id_Edu
        {
            get { return mnumId_Edu; }
            set { mnumId_Edu = value; }
        }
        Int32? mnumId_CargoAspira = null;
        public Int32? Id_CargoAspira
        {
            get { return mnumId_CargoAspira; }
            set { mnumId_CargoAspira = value; }
        }

        Int32? mnumId_CargoSeleccion = null;
        public Int32? Id_CargoSeleccion
        {
            get { return mnumId_CargoSeleccion; }
            set { mnumId_CargoSeleccion = value; }
        }

        Int32? mnumId_GrupoEmpleado = null;
        public Int32? Id_GrupoEmpleado
        {
            get { return mnumId_GrupoEmpleado; }
            set { mnumId_GrupoEmpleado = value; }
        }
        String mstrRuta_HV = null;
        public String Ruta_HV
        {
            get { return mstrRuta_HV; }
            set { mstrRuta_HV = value; }
        }
        Boolean mblnDataOK = true;
        public Boolean DataOK
        {
            get { return mblnDataOK; }
            set { mblnDataOK = value; }
        }
        String mstrCentrodeCostos = null;
        public String CentrodeCostos
        {
            get { return mstrCentrodeCostos; }
            set { mstrCentrodeCostos = value; }
        }
        String mstrCargo = null;
        public String Cargo
        {
            get { return mstrCargo; }
            set { mstrCargo = value; }
        }
        String mstrCargoCont = null;
        public String CargoContains
        {
            get { return mstrCargoCont; }
            set { mstrCargoCont = value; }
        }

        String mstrFecha_Creacionf = null;
        public String Fecha_Creacionf
        {
            get { return mstrFecha_Creacionf; }
            set { mstrFecha_Creacionf = value; }
        }
        String mstrSede = null;
        public String Sede
        {
            get { return mstrSede; }
            set { mstrSede = value; }
        }

        Int32? mnumId_Dpto = null;
        public Int32? Id_Dpto
        {
            get { return mnumId_Dpto; }
            set { mnumId_Dpto = value; }
        }

        String mstrFecha_Inicio_Turno = null;
        public String Fecha_Inicio_Turno
        {
            get { return mstrFecha_Inicio_Turno; }
            set { mstrFecha_Inicio_Turno = value; }
        }
        String mstrFecha_Fin_Turno = null;
        public String Fecha_Fin_Turno
        {
            get { return mstrFecha_Fin_Turno; }
            set { mstrFecha_Fin_Turno = value; }
        }
        #endregion
    }
}

