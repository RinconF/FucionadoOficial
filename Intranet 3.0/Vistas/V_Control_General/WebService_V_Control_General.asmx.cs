using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BRL;

namespace Intranet_3._0.Vistas.V_Control_General
{
    /// <summary>
    /// Descripción breve de WebService_V_Control_General
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService_V_Control_General : System.Web.Services.WebService
    {
        //GRUPOS
        [WebMethod]
        public List<string[]> cargar_datos_modal_actualizar_grupo(string Id_Grupo)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(Id_Grupo);
                dt = Int_Usuarios_BRL.SelectTable(obj, 9);
                if(dt.Rows.Count > 0)
                {
                    string[] array = new string[5];
                    array[0] = dt.Rows[0]["Id_Grupo_Vista"].ToString();
                    array[1] = dt.Rows[0]["Nombre_Grupo"].ToString();
                    array[2] = dt.Rows[0]["Estado"].ToString();
                    array[3] = dt.Rows[0]["Descripcion"].ToString();
                    array[4] = dt.Rows[0]["Icono_Grupo"].ToString();

                    list.Add(array);

                    return list;
                }
                else
                {
                    string[] array = new string[dt.Rows.Count];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public List<string[]> cargar_tabla_rol_grupo(string Id_Grupo)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(Id_Grupo);
                dt = Int_Usuarios_BRL.SelectTable(obj, 10);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[4];
                        array[0] = row["Id_Grupo_Detalle"].ToString();
                        array[1] = row["Nombre_Rol"].ToString();
                        array[2] = row["Fecha"].ToString();
                        array[3] = row["Nombre_Grupo"].ToString();

                        list.Add(array);
                    }

                    return list;
                }
                else
                {
                    string[] array = new string[dt.Rows.Count];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public List<string[]> cargar_estado_grupos(string Id_Grupo)
        {
            try
            {
                if (Id_Grupo == "")
                {
                    Id_Grupo = "0";
                }

                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Rol = Convert.ToInt32(Id_Grupo);
                dt = Int_Usuarios_BRL.SelectTable(obj, 13);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[1];
                        array[0] = row["Estado"].ToString();

                        list.Add(array);
                    }

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public string asignar_roles(string Id_Grupo, string Id_Rol) {
            try
            {
                string retonar = "";
                if (!String.IsNullOrEmpty(Id_Grupo) && !String.IsNullOrEmpty(Id_Rol))
                {
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Id_Usuario = Convert.ToInt32(Id_Grupo);
                    obj.Id_Rol = Convert.ToInt32(Id_Rol);
                    Int_Usuarios_BRL.InsertOrUpdate(obj, 51);

                    retonar = "1";
                }
                else
                {
                    retonar = "0";
                }

                return retonar;
            }
            catch (Exception ex)
            {
                string retonar = ex.ToString();
                return retonar;
            }
        }


        //vistas
        [WebMethod]
        public List<string[]> cargar_datos_modal_actualizar_vista(string Id_Vista)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(Id_Vista);
                dt = Int_Usuarios_BRL.SelectTable(obj, 17);
                if (dt.Rows.Count > 0)
                {
                    string[] array = new string[6];
                    array[0] = dt.Rows[0]["Id_Vista"].ToString();
                    array[1] = dt.Rows[0]["Nombre_Vista"].ToString();
                    array[2] = dt.Rows[0]["Estado"].ToString();
                    array[3] = dt.Rows[0]["Descripcion"].ToString();
                    array[4] = dt.Rows[0]["Icono_Vista"].ToString();
                    array[5] = dt.Rows[0]["Ruta"].ToString();

                    list.Add(array);

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public List<string[]> cargar_tabla_grupo_vista(string Id_Vista)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(Id_Vista);
                dt = Int_Usuarios_BRL.SelectTable(obj, 18);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[4];
                        array[0] = row["Id_Vista_Detalle"].ToString();
                        array[1] = row["Nombre_Grupo"].ToString();
                        array[2] = row["Fecha"].ToString();
                        array[3] = row["Nombre_Vista"].ToString();

                        list.Add(array);
                    }

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public List<string[]> cargar_estado_vistas(string Id_Vista)
        {
            try
            {
                if (Id_Vista == "")
                {
                    Id_Vista = "0";
                }

                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Rol = Convert.ToInt32(Id_Vista);
                dt = Int_Usuarios_BRL.SelectTable(obj, 19);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[1];
                        array[0] = row["Estado"].ToString();

                        list.Add(array);
                    }

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }


        //Roles
        [WebMethod]
        public List<string[]> cargar_datos_modal_actualizar_roles(string Id_Rol)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(Id_Rol);
                dt = Int_Usuarios_BRL.SelectTable(obj, 23);
                if (dt.Rows.Count > 0)
                {
                    string[] array = new string[4];
                    array[0] = dt.Rows[0]["Id_Rol"].ToString();
                    array[1] = dt.Rows[0]["Nombre_Rol"].ToString();
                    array[2] = dt.Rows[0]["Estado"].ToString();
                    array[3] = dt.Rows[0]["Descripcion"].ToString();

                    list.Add(array);

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }
        [WebMethod]
        public List<string[]> cargar_estado_roles(string Id_Rol)
        {
            try
            {
                if (Id_Rol == "")
                {
                    Id_Rol = "0";
                }

                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Rol = Convert.ToInt32(Id_Rol);
                dt = Int_Usuarios_BRL.SelectTable(obj, 25);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[1];
                        array[0] = row["Estado"].ToString();

                        list.Add(array);
                    }

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }


        //Usuario
        [WebMethod]
        public List<string[]> cargar_datos_modal_actualizar_usuarios(string Id_Usuario)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(Id_Usuario);
                dt = Int_Usuarios_BRL.SelectTable(obj, 29);
                if (dt.Rows.Count > 0)
                {
                    string[] array = new string[5];
                    array[0] = dt.Rows[0]["Usuario"].ToString();
                    array[1] = dt.Rows[0]["Contraseña"].ToString();
                    array[2] = dt.Rows[0]["Nombre_Rol"].ToString();
                    array[3] = dt.Rows[0]["Id_Rol"].ToString();
                    array[4] = dt.Rows[0]["Estado"].ToString();

                    list.Add(array);

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }
    }
}
