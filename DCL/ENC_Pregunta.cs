using System; 
 using System.Data; 
 namespace DCL { 
 public class ENC_Pregunta 
 { 
 #region Propiedades 
 Int32? mvarId_Pregunta = null; 
 public Int32? Id_Pregunta  
{ 
 get { return mvarId_Pregunta; } 
 set { mvarId_Pregunta = value; } 
 }Int32? mvarId_Encuesta = null; 
 public Int32? Id_Encuesta  
{ 
 get { return mvarId_Encuesta; } 
 set { mvarId_Encuesta = value; } 
 }Int32? mvarId_Tipo_Pregunta = null; 
 public Int32? Id_Tipo_Pregunta  
{ 
 get { return mvarId_Tipo_Pregunta; } 
 set { mvarId_Tipo_Pregunta = value; } 
 }String mvarPregunta = null; 
 public String Pregunta  
{ 
 get { return mvarPregunta; } 
 set { mvarPregunta = value; } 
 }Boolean? mvarEstado = null; 
 public Boolean? Estado  
{ 
 get { return mvarEstado; } 
 set { mvarEstado = value; } 
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
 } 
#endregion 
 #region Constructores 
 public ENC_Pregunta() { } 
 public ENC_Pregunta(Int32? varId_Pregunta, Int32? varId_Encuesta, Int32? varId_Tipo_Pregunta, String varPregunta, Boolean? varEstado, Int32? varUsuario_Creacion, String varFecha_Creacion, Int32? varUsuario_Actualiza, String varFecha_Actualiza) 
 { 
 mvarId_Pregunta = varId_Pregunta;mvarId_Encuesta = varId_Encuesta;mvarId_Tipo_Pregunta = varId_Tipo_Pregunta;mvarPregunta = varPregunta;mvarEstado = varEstado;mvarUsuario_Creacion = varUsuario_Creacion;mvarFecha_Creacion = varFecha_Creacion;mvarUsuario_Actualiza = varUsuario_Actualiza;mvarFecha_Actualiza = varFecha_Actualiza;} 
 public ENC_Pregunta(IDataRecord obj) 
 {  
mvarId_Pregunta = Convert.ToInt32(obj["Id_Pregunta"]); 
mvarId_Encuesta = Convert.ToInt32(obj["Id_Encuesta"]); 
mvarId_Tipo_Pregunta = Convert.ToInt32(obj["Id_Tipo_Pregunta"]); 
mvarPregunta = Convert.ToString(obj["Pregunta"]); 
mvarEstado = Convert.ToBoolean(obj["Estado"]); 
mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]); 
mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]); 
mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]); 
mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]); 
 
} 
 public ENC_Pregunta(DataRow obj)  
{  
mvarId_Pregunta = Convert.ToInt32(obj["Id_Pregunta"]); 
mvarId_Encuesta = Convert.ToInt32(obj["Id_Encuesta"]); 
mvarId_Tipo_Pregunta = Convert.ToInt32(obj["Id_Tipo_Pregunta"]); 
mvarPregunta = Convert.ToString(obj["Pregunta"]); 
mvarEstado = Convert.ToBoolean(obj["Estado"]); 
mvarUsuario_Creacion = Convert.ToInt32(obj["Usuario_Creacion"]); 
mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]); 
mvarUsuario_Actualiza = Convert.ToInt32(obj["Usuario_Actualiza"]); 
mvarFecha_Actualiza = Convert.ToString(obj["Fecha_Actualiza"]); 

 } 
 #endregion 
} 
 
}