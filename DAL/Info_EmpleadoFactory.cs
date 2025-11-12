using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DCL;

namespace DAL
{
    public class Info_EmpleadoFactory:FactoryBase
    {
        public Info_EmpleadoFactory() { }
		
        public Info_Empleado Load(Info_Empleado objIEm)
        {
            try
            {
                AddParameters(objIEm);
                AddCmdParameter("@Action", 0, ParameterDirection.Input);
                ExecuteReader();
                while(Read())
                {
                    objIEm = new Info_Empleado(GetDataReader());
                }
                return objIEm;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public Info_EmpleadoCollection SelectByParams(Info_Empleado objIEm, int Action)
        {
            Info_EmpleadoCollection Collection = new Info_EmpleadoCollection();
            try
            {
                AddParameters(objIEm);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                ExecuteReader();
                while (Read())
                {
                    Collection.Add(new Info_Empleado(GetDataReader()));
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Collection;
        }
        public DataTable SelectTable(Info_Empleado objIEm,int Action)
        {
            DataTable dt= new DataTable();
            try
            {
                AddParameters(objIEm);
                AddCmdParameter("@Action", Action, ParameterDirection.Input);
                dt = GetDataSet().Tables[0];
            }
            catch (Exception e)
            {

                throw e;
            }
            return dt;
        }
        public int InsertarOrUpdate(Info_Empleado objIEm, int Action)
        {
            int i;
            try
            {
                AddParameters(objIEm);
                AddCmdParameter("@Action",Action,ParameterDirection.Input);
                ExecuteNonQuery();
                i = 1;
            }
            catch (Exception e)
            {
                i = -1;
                throw e;
            }
            return i;
        }
        private void AddParameters(Info_Empleado objIEm)
        {
            CreateCommand("SP_Int_Inf_Empleado", true);
            AddCmdParameter("@Id_IE", objIEm.Id_IE, ParameterDirection.Input);
            AddCmdParameter("@Id_TipoDoc", objIEm.Id_TipoDoc, ParameterDirection.Input);
            AddCmdParameter("@N_Identificacion", objIEm.N_Identificacion, ParameterDirection.Input);
            AddCmdParameter("@Codigo_Sae", objIEm.Codigo_Sae, ParameterDirection.Input);
            AddCmdParameter("@Nombres", objIEm.Nombres, ParameterDirection.Input);
            AddCmdParameter("@Apellido1", objIEm.Apellido1, ParameterDirection.Input);
            AddCmdParameter("@Apellido2", objIEm.Apellido2, ParameterDirection.Input);
            AddCmdParameter("@Id_Sexo", objIEm.Id_Sexo, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Nacimiento", objIEm.Fecha_Nacimiento, ParameterDirection.Input);
            AddCmdParameter("@Id_PaisNcto", objIEm.Id_PaisNcto, ParameterDirection.Input);
            AddCmdParameter("@Id_DptoNcto", objIEm.Id_DptoNcto, ParameterDirection.Input);
            AddCmdParameter("@Id_CiudadNcto", objIEm.Id_CiudadNcto, ParameterDirection.Input);
            AddCmdParameter("@Id_Estado", objIEm.Id_Estado, ParameterDirection.Input);
            AddCmdParameter("@Id_EstadoCivil", objIEm.Id_EstadoCivil, ParameterDirection.Input);
            AddCmdParameter("@Id_RH", objIEm.Id_RH, ParameterDirection.Input);
            AddCmdParameter("@Fec_ExpDoc", objIEm.Fec_ExpDoc, ParameterDirection.Input);
            AddCmdParameter("@Id_Pais_ExpDoc", objIEm.Id_Pais_ExpDoc, ParameterDirection.Input);
            AddCmdParameter("@Id_Dpto_ExpDoc", objIEm.Id_DptoExpDoc, ParameterDirection.Input);
            AddCmdParameter("@Id_Ciudad_ExpDoc", objIEm.Id_CiudadExpDoc, ParameterDirection.Input);
            AddCmdParameter("@Fec_VenceDoc", objIEm.Fec_VenceDoc, ParameterDirection.Input);
            AddCmdParameter("@Dpto_Residencia", objIEm.Dpto_Residencia, ParameterDirection.Input);
            AddCmdParameter("@Id_CiudadResidencia", objIEm.Id_CiudadResidencia, ParameterDirection.Input);
            AddCmdParameter("@Id_LocalidadResidencia", objIEm.Id_LocalidadResidencia, ParameterDirection.Input);
            AddCmdParameter("@Id_BarrioResidencia", objIEm.Id_BarrioResidencia, ParameterDirection.Input);
            AddCmdParameter("@Dir_Residencia", objIEm.Dir_Residencia, ParameterDirection.Input);
            AddCmdParameter("@Celular", objIEm.Celular, ParameterDirection.Input);
            AddCmdParameter("@Telefono", objIEm.Telefono, ParameterDirection.Input);
            AddCmdParameter("@Email_Personal", objIEm.Email_Personal, ParameterDirection.Input);
            AddCmdParameter("@Email_Empresa", objIEm.Email_Empresa, ParameterDirection.Input);
            AddCmdParameter("@Id_ExperienciaLaboral", objIEm.Id_ExperienciaLaboral, ParameterDirection.Input);
            AddCmdParameter("@Observaciones", objIEm.Observaciones, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacion", objIEm.Fecha_Creacion, ParameterDirection.Input);
            AddCmdParameter("@Id_UsuarioCreacion", objIEm.Id_UsuarioCreacion, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Actualizacion", objIEm.Fecha_Actualizacion, ParameterDirection.Input);
            AddCmdParameter("@Id_UsuarioActualizacion", objIEm.Id_UsuarioActualizacion, ParameterDirection.Input);
            AddCmdParameter("@Id_AspiracionSalarial", objIEm.Id_AspiracionSalarial, ParameterDirection.Input);
            AddCmdParameter("@Id_Edu", objIEm.Id_Edu, ParameterDirection.Input);
            AddCmdParameter("@Id_CargoAspira", objIEm.Id_CargoAspira, ParameterDirection.Input);
            AddCmdParameter("@Id_CargoSeleccion", objIEm.Id_CargoSeleccion, ParameterDirection.Input);
            AddCmdParameter("@Id_GrupoEmpleado", objIEm.Id_GrupoEmpleado, ParameterDirection.Input);
            AddCmdParameter("@Ruta_HV", objIEm.Ruta_HV, ParameterDirection.Input);
            AddCmdParameter("@DataOK", objIEm.DataOK, ParameterDirection.Input);
            AddCmdParameter("@CentrodeCostos", objIEm.CentrodeCostos, ParameterDirection.Input);
            AddCmdParameter("@Cargo", objIEm.Cargo, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Creacionf", objIEm.Fecha_Creacionf, ParameterDirection.Input);
            AddCmdParameter("@Sede", objIEm.Sede, ParameterDirection.Input);
            AddCmdParameter("@CargoContains", objIEm.CargoContains, ParameterDirection.Input);
            AddCmdParameter("@Id_Dpto", objIEm.Id_Dpto, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Inicio_Turno", objIEm.Fecha_Inicio_Turno, ParameterDirection.Input);
            AddCmdParameter("@Fecha_Fin_Turno", objIEm.Fecha_Fin_Turno, ParameterDirection.Input);
        }
    }
}

