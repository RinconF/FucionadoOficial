# Modelo Entidad-Relación para popups con soporte de video

A continuación se describe el MER lógico que respalda la funcionalidad de popups, incorporando el nuevo atributo `Video` para almacenar la ruta del recurso audiovisual.

## Entidades principales

### dbo.Int_Popup
| Columna               | Tipo             | Descripción |
|-----------------------|------------------|-------------|
| Id_Popup (PK)         | INT              | Identificador único del popup. |
| Titulo                | NVARCHAR(200)    | Título mostrado en la UI. |
| Descripcion           | NVARCHAR(MAX)    | Texto descriptivo del popup. |
| Imagen                | NVARCHAR(MAX)    | Ruta o nombre de archivo de la imagen asociada. |
| Video                 | NVARCHAR(MAX)    | **Nuevo**: ruta o nombre de archivo del video opcional. |
| Url                   | NVARCHAR(MAX)    | Enlace que se abre al hacer clic en la llamada a la acción. |
| Fecha_Creacion        | DATETIME         | Fecha y hora de creación. |
| Estado                | BIT              | Indica si el popup está activo (1) o inactivo (0). |
| Tiempo_Visualizacion  | INT              | Segundos que permanece visible antes de autocerrarse. |
| Fecha_Inicio          | DATETIME         | Inicio de vigencia. |
| Fecha_Fin             | DATETIME         | Fin de vigencia (puede ser NULL). |
| Id_Usuario            | INT              | Usuario que creó o actualizó el registro. |

### dbo.Int_Popup_Roles
| Columna               | Tipo  | Descripción |
|-----------------------|-------|-------------|
| Id_Popup_Rol (PK)     | INT   | Identificador del vínculo. |
| Id_Popup (FK)         | INT   | Referencia a `Int_Popup`. |
| Id_Rol (FK)           | INT   | Referencia a `Int_Roles`. |

### dbo.Int_Popup_Vistas
| Columna               | Tipo      | Descripción |
|-----------------------|-----------|-------------|
| Id_Popup_Vista (PK)   | INT       | Identificador de la interacción. |
| Id_Popup (FK)         | INT       | Popup visualizado. |
| Id_Usuario (FK)       | INT       | Usuario que realizó la interacción. |
| Fecha_Vista           | DATETIME  | Marca de tiempo del evento. |
| Interaccion           | NVARCHAR(100) | Tipo de interacción (por ejemplo, `cerrar`, `clic_url`). |

### dbo.Int_Roles
| Columna        | Tipo           | Descripción |
|----------------|----------------|-------------|
| Id_Rol (PK)    | INT            | Identificador del rol. |
| Nombre_Rol     | NVARCHAR(150)  | Nombre descriptivo. |
| Estado         | BIT            | Rol activo/inactivo. |

### dbo.Int_Usuarios
| Columna        | Tipo          | Descripción |
|----------------|---------------|-------------|
| Id_Usuario (PK)| INT           | Identificador del usuario. |
| Usuario        | NVARCHAR(150) | Nombre de usuario. |
| Id_Rol (FK)    | INT           | Rol asignado. |

## Relaciones

- `Int_Popup` 1 --- N `Int_Popup_Roles`
- `Int_Roles` 1 --- N `Int_Popup_Roles`
- `Int_Popup` 1 --- N `Int_Popup_Vistas`
- `Int_Usuarios` 1 --- N `Int_Popup_Vistas`
- `Int_Usuarios` N --- 1 `Int_Roles`

El atributo `Video` reside en `Int_Popup` y no altera las cardinalidades existentes; únicamente amplía el contenido multimedia disponible para cada popup.
