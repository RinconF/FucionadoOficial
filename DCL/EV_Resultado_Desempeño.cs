using System; 
 using System.Data; 
 namespace DCL { 
 public class EV_Resultado_Desempeño 
 { 
 #region Propiedades 
 Int32? mvarId_EV_Desempeño = null; 
 public Int32? Id_EV_Desempeño  
{ 
 get { return mvarId_EV_Desempeño; } 
 set { mvarId_EV_Desempeño = value; } 
 }Int32? mvarId_IE = null; 
 public Int32? Id_IE  
{ 
 get { return mvarId_IE; } 
 set { mvarId_IE = value; } 
 }Int32? mvarId_Consideracion_Item = null; 
 public Int32? Id_Consideracion_Item  
{ 
 get { return mvarId_Consideracion_Item; } 
 set { mvarId_Consideracion_Item = value; } 
 }String mvarAuto_Evaluacion = null; 
 public String Auto_Evaluacion  
{ 
 get { return mvarAuto_Evaluacion; } 
 set { mvarAuto_Evaluacion = value; } 
 }String mvarJefe_Evaluacion = null; 
 public String Jefe_Evaluacion  
{ 
 get { return mvarJefe_Evaluacion; } 
 set { mvarJefe_Evaluacion = value; } 
 }String mvarTotal = null;
 public String Total
{
 get { return mvarTotal; }
 set { mvarTotal = value; }
 }String mvarFecha_Creacion = null; 
 public String Fecha_Creacion  
{ 
 get { return mvarFecha_Creacion; } 
 set { mvarFecha_Creacion = value; } 
 }Int32? mvarUsuario_Actualizacion = null; 
 public Int32? Usuario_Actualizacion  
{ 
 get { return mvarUsuario_Actualizacion; } 
 set { mvarUsuario_Actualizacion = value; } 
 }String mvarFecha_Actualizacion = null; 
 public String Fecha_Actualizacion  
{ 
 get { return mvarFecha_Actualizacion; } 
 set { mvarFecha_Actualizacion = value; } 
 }Boolean? mvarEstado = null; 
 public Boolean? Estado  
{ 
 get { return mvarEstado; } 
 set { mvarEstado = value; } 
 } 
#endregion 
 #region Constructores 
 public EV_Resultado_Desempeño() { } 
 public EV_Resultado_Desempeño(Int32? varId_EV_Desempeño, Int32? varId_IE, Int32? varId_Consideracion_Item, String varAuto_Evaluacion, String varJefe_Evaluacion, String varTotal, String varFecha_Creacion, Int32? varUsuario_Actualizacion, String varFecha_Actualizacion, Boolean? varEstado) 
 { 
 mvarId_EV_Desempeño = varId_EV_Desempeño;mvarId_IE = varId_IE;mvarId_Consideracion_Item = varId_Consideracion_Item;mvarAuto_Evaluacion = varAuto_Evaluacion;mvarJefe_Evaluacion = varJefe_Evaluacion;mvarTotal = varTotal; mvarFecha_Creacion = varFecha_Creacion;mvarUsuario_Actualizacion = varUsuario_Actualizacion;mvarFecha_Actualizacion = varFecha_Actualizacion;mvarEstado = varEstado;} 
 public EV_Resultado_Desempeño(IDataRecord obj) 
 {  
mvarId_EV_Desempeño = Convert.ToInt32(obj["Id_EV_Desempeño"]); 
mvarId_IE = Convert.ToInt32(obj["Id_IE"]); 
mvarId_Consideracion_Item = Convert.ToInt32(obj["Id_Consideracion_Item"]); 
mvarAuto_Evaluacion = Convert.ToString(obj["Auto_Evaluacion"]); 
mvarJefe_Evaluacion = Convert.ToString(obj["Jefe_Evaluacion"]);
mvarTotal = Convert.ToString(obj["Total"]);
mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]); 
mvarUsuario_Actualizacion = Convert.ToInt32(obj["Usuario_Actualizacion"]); 
mvarFecha_Actualizacion = Convert.ToString(obj["Fecha_Actualizacion"]); 
mvarEstado = Convert.ToBoolean(obj["Estado"]); 
 
} 
 public EV_Resultado_Desempeño(DataRow obj)  
{  
mvarId_EV_Desempeño = Convert.ToInt32(obj["Id_EV_Desempeño"]); 
mvarId_IE = Convert.ToInt32(obj["Id_IE"]); 
mvarId_Consideracion_Item = Convert.ToInt32(obj["Id_Consideracion_Item"]); 
mvarAuto_Evaluacion = Convert.ToString(obj["Auto_Evaluacion"]); 
mvarJefe_Evaluacion = Convert.ToString(obj["Jefe_Evaluacion"]);
mvarTotal = Convert.ToString(obj["Total"]);
mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]); 
mvarUsuario_Actualizacion = Convert.ToInt32(obj["Usuario_Actualizacion"]); 
mvarFecha_Actualizacion = Convert.ToString(obj["Fecha_Actualizacion"]); 
mvarEstado = Convert.ToBoolean(obj["Estado"]); 

 } 
 #endregion 
} 
 
}