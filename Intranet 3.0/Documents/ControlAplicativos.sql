-- MER resumido
-- Entidad principal: Int_Aplicativos
--  * Id_Aplicativo (PK, int, identity)
--  * Titulo (nvarchar(150))
--  * Descripcion (nvarchar(300))
--  * Imagen (nvarchar(300))
--  * Url (nvarchar(500))
--  * Seccion (varchar(20)) -- EMPRESARIALES | CONSULTA | SOPORTE
--  * Fecha_Creacion (datetime)
--  * Fecha_Actualizacion (datetime)
--  * Estado (bit)
--
-- Relación sugerida: Int_Aplicativos.Id_Usuario_Crea (FK opcional a Int_Usuarios.Id_Usuario) si se quiere
-- auditar quién gestiona los aplicativos.

/****************************************************************************************
 Tabla base
****************************************************************************************/
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Int_Aplicativos]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Int_Aplicativos](
        [Id_Aplicativo] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Titulo] NVARCHAR(150) NOT NULL,
        [Descripcion] NVARCHAR(300) NULL,
        [Imagen] NVARCHAR(300) NULL,
        [Url] NVARCHAR(500) NULL,
        [Seccion] VARCHAR(20) NOT NULL,
        [Fecha_Creacion] DATETIME NOT NULL DEFAULT(GETDATE()),
        [Fecha_Actualizacion] DATETIME NULL,
        [Estado] BIT NOT NULL DEFAULT(1)
    );
END
GO

/****************************************************************************************
 Procedimiento almacenado: SP_Int_Aplicativos
 Acciones:
 0 -> Listar todos (con filtro opcional por estado)
 1 -> Listar activos para el portal
 2 -> Consultar por Id_Aplicativo
 3 -> Insertar nuevo
 4 -> Actualizar
 5 -> Eliminar (soft delete: Estado = 0)
****************************************************************************************/
IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Int_Aplicativos]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[SP_Int_Aplicativos];
GO

CREATE PROCEDURE [dbo].[SP_Int_Aplicativos]
    @Id_Aplicativo       INT = NULL,
    @Titulo              NVARCHAR(150) = NULL,
    @Descripcion         NVARCHAR(300) = NULL,
    @Imagen              NVARCHAR(300) = NULL,
    @Url                 NVARCHAR(500) = NULL,
    @Seccion             VARCHAR(20) = NULL,
    @Fecha_Creacion      DATETIME = NULL,
    @Fecha_Actualizacion DATETIME = NULL,
    @Estado              BIT = NULL,
    @Action              INT
AS
BEGIN
    SET NOCOUNT ON;

    IF (@Action = 0)
    BEGIN
        SELECT Id_Aplicativo, Titulo, Descripcion, Imagen, Url, Seccion, Fecha_Creacion, Fecha_Actualizacion, Estado
        FROM   Int_Aplicativos
        WHERE  (@Estado IS NULL OR Estado = @Estado)
        ORDER BY Fecha_Creacion DESC;
    END
    ELSE IF (@Action = 1)
    BEGIN
        SELECT Id_Aplicativo, Titulo, Descripcion, Imagen, Url, Seccion, Fecha_Creacion, Fecha_Actualizacion, Estado
        FROM   Int_Aplicativos
        WHERE  Estado = 1
        ORDER BY Seccion, Fecha_Creacion DESC;
    END
    ELSE IF (@Action = 2)
    BEGIN
        SELECT Id_Aplicativo, Titulo, Descripcion, Imagen, Url, Seccion, Fecha_Creacion, Fecha_Actualizacion, Estado
        FROM   Int_Aplicativos
        WHERE  Id_Aplicativo = @Id_Aplicativo;
    END
    ELSE IF (@Action = 3)
    BEGIN
        INSERT INTO Int_Aplicativos (Titulo, Descripcion, Imagen, Url, Seccion, Fecha_Creacion, Estado)
        VALUES (ISNULL(@Titulo, ''), @Descripcion, @Imagen, @Url, @Seccion, ISNULL(@Fecha_Creacion, GETDATE()), 1);
    END
    ELSE IF (@Action = 4)
    BEGIN
        UPDATE Int_Aplicativos
        SET    Titulo = ISNULL(@Titulo, Titulo),
               Descripcion = @Descripcion,
               Imagen = CASE WHEN @Imagen IS NULL OR @Imagen = '' THEN Imagen ELSE @Imagen END,
               Url = @Url,
               Seccion = ISNULL(@Seccion, Seccion),
               Estado = ISNULL(@Estado, Estado),
               Fecha_Actualizacion = ISNULL(@Fecha_Actualizacion, GETDATE())
        WHERE  Id_Aplicativo = @Id_Aplicativo;
    END
    ELSE IF (@Action = 5)
    BEGIN
        UPDATE Int_Aplicativos
        SET    Estado = 0,
               Fecha_Actualizacion = ISNULL(@Fecha_Actualizacion, GETDATE())
        WHERE  Id_Aplicativo = @Id_Aplicativo;
    END
END
GO
