-- =============================================
-- SOLUCIÓN INTRANET MIETIB - MÓDULO POPUP
-- =============================================
-- Script: Creación de Tablas para Sistema de Pop-ups
-- Autor: Daniel Rincon
-- Fecha: 18/10/2025
-- Descripción: Crea las tablas necesarias para el módulo de Pop-ups administrables
-- =============================================

USE [Intranet_V2_NE]
GO

-- =============================================
-- TABLA 1: Int_Popup (Tabla Principal)
-- Almacena la información principal de cada pop-up con control temporal
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Int_Popup]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Int_Popup](
        [Id_Popup] [int] IDENTITY(1,1) NOT NULL,
        [Titulo] [nvarchar](200) NOT NULL,
        [Descripcion] [nvarchar](max) NOT NULL,
        [Imagen] [nvarchar](max) NULL,
        [Video] [nvarchar](max) NULL,
        [Fecha_Creacion] [datetime] NOT NULL DEFAULT(GETDATE()),
        [Estado] [bit] NOT NULL DEFAULT(1),
        [Url] [nvarchar](max) NULL,
        [Tiempo_Visualizacion] [int] NULL DEFAULT(5),
        [Fecha_Inicio] [datetime] NOT NULL,
        [Fecha_Fin] [datetime] NULL,
        
        CONSTRAINT [PK_Int_Popup] PRIMARY KEY CLUSTERED ([Id_Popup] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
                  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
        
        CONSTRAINT [CK_Int_Popup_Fechas] CHECK ([Fecha_Fin] IS NULL OR [Fecha_Inicio] < [Fecha_Fin]),
        CONSTRAINT [CK_Int_Popup_Tiempo] CHECK ([Tiempo_Visualizacion] IS NULL OR [Tiempo_Visualizacion] > 0)
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

    PRINT '✅ Tabla Int_Popup creada exitosamente'
END
ELSE
BEGIN
    PRINT 'ℹ️ La tabla Int_Popup ya existe'
END
GO

-- =============================================
-- TABLA 2: Int_Popup_Vistas (Tabla de Visualizaciones)
-- Registra trazabilidad de visualizaciones y comportamiento del usuario
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Int_Popup_Vistas]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Int_Popup_Vistas](
        [Id_Popup_Vistas] [int] IDENTITY(1,1) NOT NULL,
        [Id_Popup] [int] NOT NULL,
        [Id_Usuario] [int] NOT NULL,
        [Fecha_Vista] [datetime] NULL DEFAULT(GETDATE()),
        [Interaccion] [nvarchar](100) NULL,
        
        CONSTRAINT [PK_Int_Popup_Vistas] PRIMARY KEY CLUSTERED ([Id_Popup_Vistas] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
                  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
        
        CONSTRAINT [FK_Int_Popup_Vistas_Popup] FOREIGN KEY([Id_Popup])
            REFERENCES [dbo].[Int_Popup] ([Id_Popup])
            ON DELETE CASCADE,
        
        CONSTRAINT [FK_Int_Popup_Vistas_Usuario] FOREIGN KEY([Id_Usuario])
            REFERENCES [dbo].[Int_Usuarios] ([Id_Usuario]),
        
        CONSTRAINT [CK_Int_Popup_Vistas_Interaccion] CHECK (
            [Interaccion] IS NULL OR 
            [Interaccion] IN ('visto', 'cerrado_manual', 'auto_cerrado', 'clic_url')
        )
    ) ON [PRIMARY]

    PRINT '✅ Tabla Int_Popup_Vistas creada exitosamente'
END
ELSE
BEGIN
    PRINT 'ℹ️ La tabla Int_Popup_Vistas ya existe'
END
GO

-- =============================================
-- TABLA 3: Int_Popup_Roles (Tabla de Segmentación N:M)
-- Define qué roles tienen permiso para visualizar cada pop-up
-- Relación muchos a muchos entre Int_Popup e Int_Roles
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Int_Popup_Roles]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Int_Popup_Roles](
        [Id_Popup_Rol] [int] IDENTITY(1,1) NOT NULL,
        [Id_Popup] [int] NOT NULL,
        [Id_Rol] [int] NOT NULL,
        
        CONSTRAINT [PK_Int_Popup_Roles] PRIMARY KEY CLUSTERED ([Id_Popup_Rol] ASC)
            WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
                  ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
        
        CONSTRAINT [FK_Int_Popup_Roles_Popup] FOREIGN KEY([Id_Popup])
            REFERENCES [dbo].[Int_Popup] ([Id_Popup])
            ON DELETE CASCADE,
        
        CONSTRAINT [FK_Int_Popup_Roles_Rol] FOREIGN KEY([Id_Rol])
            REFERENCES [dbo].[Int_Roles] ([Id_Rol]),
        
        CONSTRAINT [UQ_Int_Popup_Roles] UNIQUE ([Id_Popup], [Id_Rol])
    ) ON [PRIMARY]

    PRINT '✅ Tabla Int_Popup_Roles creada exitosamente'
END
ELSE
BEGIN
    PRINT 'ℹ️ La tabla Int_Popup_Roles ya existe'
END
GO

-- =============================================
-- ÍNDICES PARA OPTIMIZACIÓN
-- =============================================

-- Índice 1: Búsqueda de popups activos y vigentes
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Int_Popup_Estado_Fechas')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Int_Popup_Estado_Fechas]
    ON [dbo].[Int_Popup] ([Estado], [Fecha_Inicio], [Fecha_Fin])
    INCLUDE ([Id_Popup], [Titulo], [Descripcion], [Imagen], [Video], [Url], [Tiempo_Visualizacion])
    
    PRINT '✅ Índice IX_Int_Popup_Estado_Fechas creado'
END
GO

-- Índice 2: Consultas de vistas por usuario
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Int_Popup_Vistas_Usuario')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Int_Popup_Vistas_Usuario]
    ON [dbo].[Int_Popup_Vistas] ([Id_Usuario], [Id_Popup])
    INCLUDE ([Fecha_Vista], [Interaccion])
    
    PRINT '✅ Índice IX_Int_Popup_Vistas_Usuario creado'
END
GO

-- Índice 3: Verificación de "ya visto"
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Int_Popup_Vistas_Popup_Usuario')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Int_Popup_Vistas_Popup_Usuario]
    ON [dbo].[Int_Popup_Vistas] ([Id_Popup], [Id_Usuario])
    INCLUDE ([Fecha_Vista])
    
    PRINT '✅ Índice IX_Int_Popup_Vistas_Popup_Usuario creado'
END
GO

-- Índice 4: Búsqueda de roles por popup
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Int_Popup_Roles_Popup')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Int_Popup_Roles_Popup]
    ON [dbo].[Int_Popup_Roles] ([Id_Popup])
    INCLUDE ([Id_Rol])
    
    PRINT '✅ Índice IX_Int_Popup_Roles_Popup creado'
END
GO

-- Índice 5: Búsqueda de popups por rol
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Int_Popup_Roles_Rol')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Int_Popup_Roles_Rol]
    ON [dbo].[Int_Popup_Roles] ([Id_Rol])
    INCLUDE ([Id_Popup])
    
    PRINT '✅ Índice IX_Int_Popup_Roles_Rol creado'
END
GO

-- =============================================
-- RESUMEN FINAL
-- =============================================
PRINT ''
PRINT '========================================='
PRINT '🎉 TABLAS CREADAS EXITOSAMENTE 🎉'
PRINT '========================================='
PRINT 'Tablas creadas: 3'
PRINT '  - Int_Popup (Principal)'
PRINT '  - Int_Popup_Vistas (Auditoría)'
PRINT '  - Int_Popup_Roles (Segmentación N:M)'
PRINT ''
PRINT 'Índices creados: 5'
PRINT 'Foreign Keys: 4'
PRINT '  - Int_Popup_Vistas → Int_Popup (CASCADE)'
PRINT '  - Int_Popup_Vistas → Int_Usuarios'
PRINT '  - Int_Popup_Roles → Int_Popup (CASCADE)'
PRINT '  - Int_Popup_Roles → Int_Roles'
PRINT ''
PRINT 'Restricciones CHECK: 3'
PRINT '  - Fechas válidas (Inicio < Fin)'
PRINT '  - Tiempo visualización positivo'
PRINT '  - Interacciones válidas'
PRINT ''
PRINT 'Restricciones UNIQUE: 1'
PRINT '  - Popup-Rol único'
PRINT '========================================='
PRINT ''
PRINT '📌 PRÓXIMOS PASOS:'
PRINT '1. Crear Stored Procedure SP_Int_Popup'
PRINT '2. Implementar frontend de administración'
PRINT '3. Implementar visualización de popups'
PRINT '========================================='
GO

-- =============================================
-- VERIFICACIÓN DE ESTRUCTURA
-- =============================================
PRINT ''
PRINT '🔎 VERIFICANDO ESTRUCTURA...'
PRINT ''

-- Verificar tablas
SELECT 
    'Tablas Creadas' AS Tipo,
    name AS Nombre,
    create_date AS Fecha_Creacion
FROM sys.tables
WHERE name IN ('Int_Popup', 'Int_Popup_Vistas', 'Int_Popup_Roles')
ORDER BY name

-- Verificar FKs
SELECT 
    'Foreign Keys' AS Tipo,
    fk.name AS Nombre,
    OBJECT_NAME(fk.parent_object_id) AS Tabla_Origen,
    OBJECT_NAME(fk.referenced_object_id) AS Tabla_Destino
FROM sys.foreign_keys fk
WHERE OBJECT_NAME(fk.parent_object_id) IN ('Int_Popup_Vistas', 'Int_Popup_Roles')
ORDER BY Tabla_Origen, fk.name

-- Verificar Índices
SELECT 
    'Índices' AS Tipo,
    i.name AS Nombre,
    OBJECT_NAME(i.object_id) AS Tabla,
    i.type_desc AS Tipo_Indice
FROM sys.indexes i
WHERE OBJECT_NAME(i.object_id) IN ('Int_Popup', 'Int_Popup_Vistas', 'Int_Popup_Roles')
  AND i.name IS NOT NULL
  AND i.name NOT LIKE 'PK_%'
ORDER BY Tabla, i.name

PRINT ''
PRINT '✅ VERIFICACIÓN COMPLETADA'
GO
