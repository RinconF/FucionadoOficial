USE [Intranet_V2_NE]
GO

/******
    Procedimiento almacenado: SP_Int_Popup
    Soporte para atributo Video (NVARCHAR(MAX))
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_Int_Popup]
(
    @Id_Popup INT = NULL,
    @Titulo NVARCHAR(200) = NULL,
    @Descripcion NVARCHAR(MAX) = NULL,
    @Imagen NVARCHAR(MAX) = NULL,
    @Video NVARCHAR(MAX) = NULL, -- Nuevo parámetro para el recurso audiovisual
    @Url NVARCHAR(MAX) = NULL,
    @Fecha_Creacion DATETIME = NULL,
    @Estado BIT = NULL,
    @Tiempo_Visualizacion INT = NULL,
    @Fecha_Inicio DATETIME = NULL,
    @Fecha_Fin DATETIME = NULL,
    @Id_Usuario INT = NULL,
    @Id_Rol INT = NULL,
    @RolesIds NVARCHAR(MAX) = NULL,
    @Interaccion NVARCHAR(100) = NULL,
    @Action INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Action = 0
    BEGIN
        SELECT DISTINCT
            p.Id_Popup,
            p.Titulo,
            p.Descripcion,
            p.Imagen,
            p.Video,
            p.Url,
            p.Tiempo_Visualizacion,
            CONVERT(NVARCHAR, p.Fecha_Creacion, 103) AS Fecha_Creacion
        FROM dbo.Int_Popup p
        WHERE p.Estado = 1
          AND GETDATE() >= p.Fecha_Inicio
          AND (p.Fecha_Fin IS NULL OR GETDATE() <= p.Fecha_Fin)
          AND NOT EXISTS (
                SELECT 1 FROM dbo.Int_Popup_Vistas pv
                WHERE pv.Id_Popup = p.Id_Popup
                  AND pv.Id_Usuario = @Id_Usuario)
          AND (
                NOT EXISTS (SELECT 1 FROM dbo.Int_Popup_Roles WHERE Id_Popup = p.Id_Popup)
                OR EXISTS (
                    SELECT 1
                    FROM dbo.Int_Popup_Roles pr
                    INNER JOIN dbo.Int_Usuarios u ON u.Id_Rol = pr.Id_Rol
                    WHERE pr.Id_Popup = p.Id_Popup
                      AND u.Id_Usuario = @Id_Usuario))
        ORDER BY p.Id_Popup DESC;
    END

    IF @Action = 1
    BEGIN
        SELECT
            p.Id_Popup,
            p.Titulo,
            p.Descripcion,
            p.Imagen,
            p.Video,
            p.Url,
            CONVERT(NVARCHAR, p.Fecha_Creacion, 103) AS Fecha_Creacion,
            CONVERT(NVARCHAR, p.Fecha_Inicio, 103) AS Fecha_Inicio,
            CONVERT(NVARCHAR, p.Fecha_Fin, 103) AS Fecha_Fin,
            p.Estado,
            p.Tiempo_Visualizacion,
            (SELECT COUNT(*) FROM dbo.Int_Popup_Roles WHERE Id_Popup = p.Id_Popup) AS Cantidad_Roles,
            STUFF((
                SELECT ', ' + r.Nombre_Rol
                FROM dbo.Int_Popup_Roles pr
                INNER JOIN dbo.Int_Roles r ON pr.Id_Rol = r.Id_Rol
                WHERE pr.Id_Popup = p.Id_Popup
                ORDER BY r.Nombre_Rol
                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Roles_Asignados
        FROM dbo.Int_Popup p
        WHERE p.Estado = 1
        ORDER BY p.Fecha_Creacion DESC;
    END

    IF @Action = 2
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION;

            INSERT INTO dbo.Int_Popup
                (Titulo, Descripcion, Imagen, Video, Url, Fecha_Creacion,
                 Estado, Tiempo_Visualizacion, Fecha_Inicio, Fecha_Fin)
            VALUES
                (@Titulo, @Descripcion, @Imagen, @Video, ISNULL(@Url, ''),
                 GETDATE(), 1, ISNULL(@Tiempo_Visualizacion, 5), @Fecha_Inicio, @Fecha_Fin);

            DECLARE @NewPopupId INT = SCOPE_IDENTITY();

            IF @RolesIds IS NOT NULL AND @RolesIds <> ''
            BEGIN
                INSERT INTO dbo.Int_Popup_Roles (Id_Popup, Id_Rol)
                SELECT @NewPopupId, CAST(LTRIM(RTRIM(value)) AS INT)
                FROM STRING_SPLIT(@RolesIds, ',')
                WHERE LTRIM(RTRIM(value)) <> ''
                  AND ISNUMERIC(LTRIM(RTRIM(value))) = 1;
            END

            SELECT @NewPopupId AS Id_Popup;

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
            DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
            DECLARE @ErrorState INT = ERROR_STATE();
            RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
        END CATCH
    END

    IF @Action = 3
    BEGIN
        SELECT
            p.Id_Popup,
            p.Titulo,
            p.Descripcion,
            p.Imagen,
            p.Video,
            p.Url,
            CONVERT(NVARCHAR, p.Fecha_Creacion, 103) AS Fecha_Creacion,
            CONVERT(NVARCHAR, p.Fecha_Inicio, 23) AS Fecha_Inicio,
            CONVERT(NVARCHAR, p.Fecha_Fin, 23) AS Fecha_Fin,
            p.Estado,
            p.Tiempo_Visualizacion,
            STUFF((
                SELECT ',' + CAST(Id_Rol AS NVARCHAR)
                FROM dbo.Int_Popup_Roles
                WHERE Id_Popup = p.Id_Popup
                ORDER BY Id_Rol
                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS Roles_Ids
        FROM dbo.Int_Popup p
        WHERE p.Id_Popup = @Id_Popup;
    END

    IF @Action = 4
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION;

            UPDATE dbo.Int_Popup
            SET
                Titulo = @Titulo,
                Descripcion = @Descripcion,
                Imagen = ISNULL(@Imagen, Imagen),
                Video = ISNULL(@Video, Video),
                Url = ISNULL(@Url, Url),
                Tiempo_Visualizacion = ISNULL(@Tiempo_Visualizacion, Tiempo_Visualizacion),
                Fecha_Inicio = @Fecha_Inicio,
                Fecha_Fin = @Fecha_Fin,
                Estado = ISNULL(@Estado, Estado)
            WHERE Id_Popup = @Id_Popup;

            IF @RolesIds IS NOT NULL
            BEGIN
                DELETE FROM dbo.Int_Popup_Roles WHERE Id_Popup = @Id_Popup;

                IF @RolesIds <> ''
                BEGIN
                    INSERT INTO dbo.Int_Popup_Roles (Id_Popup, Id_Rol)
                    SELECT @Id_Popup, CAST(LTRIM(RTRIM(value)) AS INT)
                    FROM STRING_SPLIT(@RolesIds, ',')
                    WHERE LTRIM(RTRIM(value)) <> ''
                      AND ISNUMERIC(LTRIM(RTRIM(value))) = 1;
                END
            END

            SELECT @@ROWCOUNT AS FilasActualizadas;

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END

    IF @Action = 5
    BEGIN
        UPDATE dbo.Int_Popup
        SET Estado = @Estado
        WHERE Id_Popup = @Id_Popup;

        SELECT @@ROWCOUNT AS FilasActualizadas;
    END

    IF @Action = 6
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION;

            UPDATE dbo.Int_Popup
            SET Estado = 0
            WHERE Id_Popup = @Id_Popup;

            DELETE FROM dbo.Int_Popup_Vistas
            WHERE Id_Popup = @Id_Popup;

            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
            THROW;
        END CATCH
    END

    IF @Action = 7
    BEGIN
        IF EXISTS (
            SELECT 1 FROM dbo.Int_Popup_Vistas
            WHERE Id_Popup = @Id_Popup
              AND Id_Usuario = @Id_Usuario)
        BEGIN
            UPDATE dbo.Int_Popup_Vistas
            SET Interaccion = @Interaccion,
                Fecha_Vista = GETDATE()
            WHERE Id_Popup = @Id_Popup
              AND Id_Usuario = @Id_Usuario;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.Int_Popup_Vistas
                (Id_Popup, Id_Usuario, Fecha_Vista, Interaccion)
            VALUES
                (@Id_Popup, @Id_Usuario, GETDATE(), @Interaccion);
        END
    END

    IF @Action = 8
    BEGIN
        DECLARE @TotalVistas INT = (
            SELECT COUNT(*) FROM dbo.Int_Popup_Vistas WHERE Id_Popup = @Id_Popup);

        IF @TotalVistas > 0
        BEGIN
            SELECT
                ISNULL(Interaccion, 'Sin interacción') AS Tipo_Interaccion,
                COUNT(*) AS Cantidad,
                CAST(COUNT(*) * 100.0 / @TotalVistas AS DECIMAL(5,2)) AS Porcentaje
            FROM dbo.Int_Popup_Vistas
            WHERE Id_Popup = @Id_Popup
            GROUP BY Interaccion

            UNION ALL

            SELECT 'TOTAL', @TotalVistas, 100.00
            ORDER BY Cantidad DESC;
        END
        ELSE
        BEGIN
            SELECT 'Sin visualizaciones' AS Tipo_Interaccion, 0 AS Cantidad, 0.00 AS Porcentaje;
        END
    END

    IF @Action = 9
    BEGIN
        SELECT
            Id_Popup,
            Titulo,
            Descripcion,
            Imagen,
            Video,
            CONVERT(NVARCHAR, Fecha_Creacion, 103) AS Fecha_Creacion,
            CONVERT(NVARCHAR, Fecha_Inicio, 103) AS Fecha_Inicio,
            CONVERT(NVARCHAR, Fecha_Fin, 103) AS Fecha_Fin,
            Estado
        FROM dbo.Int_Popup
        WHERE Estado = 1
          AND (
                @Titulo IS NULL
                OR Titulo LIKE '%' + @Titulo + '%'
                OR Descripcion LIKE '%' + @Titulo + '%')
        ORDER BY Fecha_Creacion DESC;
    END

    IF @Action = 10
    BEGIN
        SELECT ISNULL(MAX(Id_Popup), 0) AS Id_Popup
        FROM dbo.Int_Popup;
    END

    IF @Action = 11
    BEGIN
        SELECT
            u.Id_Usuario,
            u.Usuario,
            r.Nombre_Rol,
            CONVERT(NVARCHAR, pv.Fecha_Vista, 120) AS Fecha_Vista,
            pv.Interaccion,
            CASE
                WHEN EXISTS (
                    SELECT 1 FROM dbo.Int_Popup_Roles pr
                    WHERE pr.Id_Popup = pv.Id_Popup
                      AND pr.Id_Rol = u.Id_Rol) THEN 'Sí'
                WHEN NOT EXISTS (
                    SELECT 1 FROM dbo.Int_Popup_Roles
                    WHERE Id_Popup = pv.Id_Popup) THEN 'Todos'
                ELSE 'No'
            END AS Tenia_Permiso
        FROM dbo.Int_Popup_Vistas pv
        INNER JOIN dbo.Int_Usuarios u ON pv.Id_Usuario = u.Id_Usuario
        LEFT JOIN dbo.Int_Roles r ON u.Id_Rol = r.Id_Rol
        WHERE pv.Id_Popup = @Id_Popup
        ORDER BY pv.Fecha_Vista DESC;
    END

    IF @Action = 12
    BEGIN
        DELETE pv
        FROM dbo.Int_Popup_Vistas pv
        INNER JOIN dbo.Int_Popup p ON pv.Id_Popup = p.Id_Popup
        WHERE p.Fecha_Fin < DATEADD(DAY, -90, GETDATE());

        SELECT @@ROWCOUNT AS RegistrosEliminados;
    END

    IF @Action = 13
    BEGIN
        SELECT
            (SELECT COUNT(*) FROM dbo.Int_Popup WHERE Estado = 1) AS Popups_Activos,
            (SELECT COUNT(*) FROM dbo.Int_Popup WHERE Estado = 1 AND GETDATE() >= Fecha_Inicio AND (Fecha_Fin IS NULL OR GETDATE() <= Fecha_Fin)) AS Popups_Vigentes,
            (SELECT COUNT(*) FROM dbo.Int_Popup_Vistas) AS Total_Visualizaciones,
            (SELECT COUNT(DISTINCT Id_Usuario) FROM dbo.Int_Popup_Vistas) AS Usuarios_Unicos,
            (SELECT COUNT(*) FROM dbo.Int_Popup_Vistas WHERE Interaccion = 'clic_url') AS Clics_En_URL,
            (SELECT COUNT(DISTINCT Id_Popup) FROM dbo.Int_Popup_Roles) AS Popups_Con_Segmentacion,
            (SELECT COUNT(*) FROM dbo.Int_Popup_Roles) AS Total_Asignaciones_Roles;
    END

    IF @Action = 14
    BEGIN
        SELECT
            r.Id_Rol,
            r.Nombre_Rol,
            CONVERT(NVARCHAR, pr.Id_Popup_Rol, 0) AS Id_Asignacion
        FROM dbo.Int_Popup_Roles pr
        INNER JOIN dbo.Int_Roles r ON pr.Id_Rol = r.Id_Rol
        WHERE pr.Id_Popup = @Id_Popup
        ORDER BY r.Nombre_Rol;
    END

    IF @Action = 15
    BEGIN
        SELECT Id_Rol, Nombre_Rol, Estado
        FROM dbo.Int_Roles
        WHERE Estado = 1
        ORDER BY Nombre_Rol;
    END

    IF @Action = 16
    BEGIN
        IF NOT EXISTS (
            SELECT 1 FROM dbo.Int_Popup_Roles
            WHERE Id_Popup = @Id_Popup AND Id_Rol = @Id_Rol)
        BEGIN
            INSERT INTO dbo.Int_Popup_Roles (Id_Popup, Id_Rol)
            VALUES (@Id_Popup, @Id_Rol);

            SELECT 1 AS Insertado;
        END
        ELSE
        BEGIN
            SELECT 0 AS Insertado, 'El rol ya está asignado' AS Mensaje;
        END
    END

    IF @Action = 17
    BEGIN
        DELETE FROM dbo.Int_Popup_Roles
        WHERE Id_Popup = @Id_Popup AND Id_Rol = @Id_Rol;

        SELECT @@ROWCOUNT AS FilasEliminadas;
    END

    IF @Action = 18
    BEGIN
        DECLARE @PuedeVer BIT = 0;
        DECLARE @Motivo NVARCHAR(200) = '';

        IF NOT EXISTS (SELECT 1 FROM dbo.Int_Popup WHERE Id_Popup = @Id_Popup AND Estado = 1)
        BEGIN
            SET @Motivo = 'Popup no existe o está inactivo';
        END
        ELSE IF NOT EXISTS (
            SELECT 1 FROM dbo.Int_Popup
            WHERE Id_Popup = @Id_Popup
              AND GETDATE() >= Fecha_Inicio
              AND (Fecha_Fin IS NULL OR GETDATE() <= Fecha_Fin))
        BEGIN
            SET @Motivo = 'Popup fuera del rango de fechas';
        END
        ELSE IF EXISTS (
            SELECT 1 FROM dbo.Int_Popup_Vistas
            WHERE Id_Popup = @Id_Popup
              AND Id_Usuario = @Id_Usuario)
        BEGIN
            SET @Motivo = 'Usuario ya visualizó este popup';
        END
        ELSE IF EXISTS (SELECT 1 FROM dbo.Int_Popup_Roles WHERE Id_Popup = @Id_Popup)
            AND NOT EXISTS (
                SELECT 1
                FROM dbo.Int_Popup_Roles pr
                INNER JOIN dbo.Int_Usuarios u ON u.Id_Rol = pr.Id_Rol
                WHERE pr.Id_Popup = @Id_Popup
                  AND u.Id_Usuario = @Id_Usuario)
        BEGIN
            SET @Motivo = 'Usuario no tiene rol asignado para ver este popup';
        END
        ELSE
        BEGIN
            SET @PuedeVer = 1;
            SET @Motivo = 'Usuario puede visualizar el popup';
        END

        SELECT @PuedeVer AS Puede_Ver, @Motivo AS Motivo;
    END
END
GO
