using System; 
 using System.Data; 
 namespace DCL { 
 public class CTRL_Capacitaciones 
 { 
 #region Propiedades 
 Int32? mvarId_Capacitacion = null; 
 public Int32? Id_Capacitacion  
{ 
 get { return mvarId_Capacitacion; } 
 set { mvarId_Capacitacion = value; } 
 }String mvarNombre_Capacitacion = null; 
 public String Nombre_Capacitacion  
{ 
 get { return mvarNombre_Capacitacion; } 
 set { mvarNombre_Capacitacion = value; } 
 }Int32? mvarUsuario_Creacion = null; 
 public Int32? Usuario_Creacion  
{ 
 get { return mvarUsuario_Creacion; } 
 set { mvarUsuario_Creacion = value; } 
 }String mvarFecha_Creacion = null; 
 public String Fecha_Creacion  
{ 
 get { return mvarFecha_Creacion; } 
 set { mvarFecha_Creacion = value; } 
 }Int32? mvarUsuario_Actualiza = null; 
 public Int32? Usuario_Actualiza  
{ 
 get { return mvarUsuario_Actualiza; } 
 set { mvarUsuario_Actualiza = value; } 
 }String mvarFecha_Actualiza = null; 
 public String Fecha_Actualiza  
{ 
 get { return mvarFecha_Actualiza; } 
 set { mvarFecha_Actualiza = value; } 
 }String mvarEstado = null; 
 public String Estado  
{ 
 get { return mvarEstado; } 
 set { mvarEstado = value; } 
 }Int32? mvarId_Acceso = null; 
 public Int32? Id_Acceso  
{ 
 get { return mvarId_Acceso; } 
 set { mvarId_Acceso = value; } 
 }Int32? mvarId_IE = null; 
 public Int32? Id_IE  
{ 
 get { return mvarId_IE; } 
 set { mvarId_IE = value; } 
 }Int32? mvarId_Sede = null; 
 public Int32? Id_Sede  
{ 
 get { return mvarId_Sede; } 
 set { mvarId_Sede = value; } 
 } 
#endregion 
 #region Constructores 
 public CTRL_Capacitaciones() { } 
 public CTRL_Capacitaciones(Int32? varId_Capacitacion, String varNombre_Capacitacion, Int32? varUsuario_Creacion, String varFecha_Creacion, Int32? varUsuario_Actualiza, String varFecha_Actualiza, String varEstado, Int32? varId_Acceso, Int32? varId_IE, Int32? varId_Sede) 
 { 
 mvarId_Capacitacion = varId_Capacitacion;mvarNombre_Capacitacion = varNombre_Capacitacion;mvarUsuario_Creacion = varUsuario_Creacion;mvarFecha_Creacion = varFecha_Creacion;mvarUsuario_Actualiza = varUsuario_Actualiza;mvarFecha_Actualiza = varFecha_Actualiza;mvarEstado = varEstado;mvarId_Acceso = varId_Acceso;mvarId_IE = varId_IE;mvarId_Sede = varId_Sede;} 
 public CTRL_Capacitaciones(IDataRecord obj) 
 {  
mvarId_Capacitacion = Convert.ToInt32(obj["Id_Capacitacion"]); 
mvarNombre_Capacitacion = Convert.ToString(obj["Nombre_Capacitacion"]); 
mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]); 
mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]); 
mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]); 
mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]); 
mvarEstado = Convert.ToString(obj["Estado"]); 
mvarId_Acceso = Convert.ToInt32(obj["Id_Acceso"]); 
mvarId_IE = Convert.ToInt32(obj["Id_IE"]); 
mvarId_Sede = Convert.ToInt32(obj["Id_Sede"]); 
 
} 
 public CTRL_Capacitaciones(DataRow obj)  
{  
mvarId_Capacitacion = Convert.ToInt32(obj["Id_Capacitacion"]); 
mvarNombre_Capacitacion = Convert.ToString(obj["Nombre_Capacitacion"]); 
mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]); 
mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]); 
mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]); 
mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]); 
mvarEstado = Convert.ToString(obj["Estado"]); 
mvarId_Acceso = Convert.ToInt32(obj["Id_Acceso"]); 
mvarId_IE = Convert.ToInt32(obj["Id_IE"]); 
mvarId_Sede = Convert.ToInt32(obj["Id_Sede"]); 

 } 
 #endregion 
} 
 
}