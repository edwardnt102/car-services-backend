 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Areas] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspAreas_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @PlaceId BIGINT
, @BasementId BIGINT
, @ColumnId BIGINT
, @MapOfTunnelsInTheArea NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Areas]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [PlaceId]
        , [BasementId]
        , [ColumnId]
        , [MapOfTunnelsInTheArea]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @PlaceId
        , @BasementId
        , @ColumnId
        , @MapOfTunnelsInTheArea
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Areas] 
-- =================================================================

CREATE TYPE [dbo].[udtAreas] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [PlaceId] BIGINT
, [BasementId] BIGINT
, [ColumnId] BIGINT
, [MapOfTunnelsInTheArea] NVARCHAR(MAX)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Areas] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspAreas_bulkInsert]
(
@items [dbo].[udtAreas] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Areas]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [PlaceId]
        , [BasementId]
        , [ColumnId]
        , [MapOfTunnelsInTheArea]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Areas] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspAreas_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @PlaceId BIGINT
, @BasementId BIGINT
, @ColumnId BIGINT
, @MapOfTunnelsInTheArea NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Areas]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [PlaceId] = @PlaceId
        , [BasementId] = @BasementId
        , [ColumnId] = @ColumnId
        , [MapOfTunnelsInTheArea] = @MapOfTunnelsInTheArea
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Areas] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspAreas_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Areas]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Areas] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspAreas_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Areas]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Areas] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspAreas_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Areas]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Areas] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspAreas_selectByPKList]
(
    @pk_list [dbo].[udtAreas_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Areas] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtAreas_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Basements] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBasements_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PlaceId BIGINT
, @ColumnDiagram NVARCHAR(256)
, @TextDiagrams NVARCHAR(256)
, @ColorScheme NVARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Basements]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PlaceId]
        , [ColumnDiagram]
        , [TextDiagrams]
        , [ColorScheme]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @PlaceId
        , @ColumnDiagram
        , @TextDiagrams
        , @ColorScheme
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Basements] 
-- =================================================================

CREATE TYPE [dbo].[udtBasements] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [PlaceId] BIGINT
, [ColumnDiagram] NVARCHAR(256)
, [TextDiagrams] NVARCHAR(256)
, [ColorScheme] NVARCHAR(256)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Basements] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBasements_bulkInsert]
(
@items [dbo].[udtBasements] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Basements]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PlaceId]
        , [ColumnDiagram]
        , [TextDiagrams]
        , [ColorScheme]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Basements] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBasements_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PlaceId BIGINT
, @ColumnDiagram NVARCHAR(256)
, @TextDiagrams NVARCHAR(256)
, @ColorScheme NVARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Basements]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [PlaceId] = @PlaceId
        , [ColumnDiagram] = @ColumnDiagram
        , [TextDiagrams] = @TextDiagrams
        , [ColorScheme] = @ColorScheme
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Basements] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBasements_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Basements]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Basements] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBasements_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Basements]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Basements] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBasements_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Basements]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Basements] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBasements_selectByPKList]
(
    @pk_list [dbo].[udtBasements_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Basements] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtBasements_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Brands] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBrands_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @BrandName NVARCHAR(MAX)
, @Subtitle NVARCHAR(MAX)
, @Description NVARCHAR(MAX)
, @Logo NVARCHAR(MAX)
, @LinkRefer NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Brands]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [BrandName]
        , [Subtitle]
        , [Description]
        , [Logo]
        , [LinkRefer]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @BrandName
        , @Subtitle
        , @Description
        , @Logo
        , @LinkRefer
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Brands] 
-- =================================================================

CREATE TYPE [dbo].[udtBrands] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [BrandName] NVARCHAR(MAX)
, [Subtitle] NVARCHAR(MAX)
, [Description] NVARCHAR(MAX)
, [Logo] NVARCHAR(MAX)
, [LinkRefer] NVARCHAR(MAX)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Brands] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBrands_bulkInsert]
(
@items [dbo].[udtBrands] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Brands]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [BrandName]
        , [Subtitle]
        , [Description]
        , [Logo]
        , [LinkRefer]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Brands] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBrands_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @BrandName NVARCHAR(MAX)
, @Subtitle NVARCHAR(MAX)
, @Description NVARCHAR(MAX)
, @Logo NVARCHAR(MAX)
, @LinkRefer NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Brands]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [BrandName] = @BrandName
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [Logo] = @Logo
        , [LinkRefer] = @LinkRefer
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Brands] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBrands_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Brands]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Brands] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBrands_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Brands]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Brands] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBrands_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Brands]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Brands] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspBrands_selectByPKList]
(
    @pk_list [dbo].[udtBrands_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Brands] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtBrands_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[CarModels] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCarModels_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Name NVARCHAR(50)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @Image NVARCHAR(MAX)
, @BrandId BIGINT
, @ClassId BIGINT
, @Note NVARCHAR(256)
, @Long NVARCHAR(10)
, @Wide NVARCHAR(10)
, @High NVARCHAR(10)
, @Heavy NVARCHAR(10)
, @ReferencePrice NVARCHAR(13)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[CarModels]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Name]
        , [Subtitle]
        , [Description]
        , [Image]
        , [BrandId]
        , [ClassId]
        , [Note]
        , [Long]
        , [Wide]
        , [High]
        , [Heavy]
        , [ReferencePrice]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Name
        , @Subtitle
        , @Description
        , @Image
        , @BrandId
        , @ClassId
        , @Note
        , @Long
        , @Wide
        , @High
        , @Heavy
        , @ReferencePrice
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[CarModels] 
-- =================================================================

CREATE TYPE [dbo].[udtCarModels] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Name] NVARCHAR(50)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [Image] NVARCHAR(MAX)
, [BrandId] BIGINT
, [ClassId] BIGINT
, [Note] NVARCHAR(256)
, [Long] NVARCHAR(10)
, [Wide] NVARCHAR(10)
, [High] NVARCHAR(10)
, [Heavy] NVARCHAR(10)
, [ReferencePrice] NVARCHAR(13)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[CarModels] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCarModels_bulkInsert]
(
@items [dbo].[udtCarModels] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[CarModels]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Name]
        , [Subtitle]
        , [Description]
        , [Image]
        , [BrandId]
        , [ClassId]
        , [Note]
        , [Long]
        , [Wide]
        , [High]
        , [Heavy]
        , [ReferencePrice]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[CarModels] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCarModels_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Name NVARCHAR(50)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @Image NVARCHAR(MAX)
, @BrandId BIGINT
, @ClassId BIGINT
, @Note NVARCHAR(256)
, @Long NVARCHAR(10)
, @Wide NVARCHAR(10)
, @High NVARCHAR(10)
, @Heavy NVARCHAR(10)
, @ReferencePrice NVARCHAR(13)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[CarModels]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Name] = @Name
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [Image] = @Image
        , [BrandId] = @BrandId
        , [ClassId] = @ClassId
        , [Note] = @Note
        , [Long] = @Long
        , [Wide] = @Wide
        , [High] = @High
        , [Heavy] = @Heavy
        , [ReferencePrice] = @ReferencePrice
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[CarModels] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCarModels_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[CarModels]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[CarModels] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCarModels_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[CarModels]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[CarModels] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCarModels_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[CarModels]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[CarModels] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCarModels_selectByPKList]
(
    @pk_list [dbo].[udtCarModels_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[CarModels] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtCarModels_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Cars] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCars_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @CustomerId BIGINT
, @BrandId BIGINT
, @CarModelId BIGINT
, @YearOfManufacture BIGINT
, @CarColor NVARCHAR(15)
, @LicensePlates NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Cars]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [CustomerId]
        , [BrandId]
        , [CarModelId]
        , [YearOfManufacture]
        , [CarColor]
        , [LicensePlates]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @CustomerId
        , @BrandId
        , @CarModelId
        , @YearOfManufacture
        , @CarColor
        , @LicensePlates
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Cars] 
-- =================================================================

CREATE TYPE [dbo].[udtCars] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [CustomerId] BIGINT
, [BrandId] BIGINT
, [CarModelId] BIGINT
, [YearOfManufacture] BIGINT
, [CarColor] NVARCHAR(15)
, [LicensePlates] NVARCHAR(25)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Cars] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCars_bulkInsert]
(
@items [dbo].[udtCars] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Cars]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [CustomerId]
        , [BrandId]
        , [CarModelId]
        , [YearOfManufacture]
        , [CarColor]
        , [LicensePlates]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Cars] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCars_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @CustomerId BIGINT
, @BrandId BIGINT
, @CarModelId BIGINT
, @YearOfManufacture BIGINT
, @CarColor NVARCHAR(15)
, @LicensePlates NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Cars]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [CustomerId] = @CustomerId
        , [BrandId] = @BrandId
        , [CarModelId] = @CarModelId
        , [YearOfManufacture] = @YearOfManufacture
        , [CarColor] = @CarColor
        , [LicensePlates] = @LicensePlates
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Cars] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCars_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Cars]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Cars] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCars_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Cars]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Cars] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCars_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Cars]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Cars] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCars_selectByPKList]
(
    @pk_list [dbo].[udtCars_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Cars] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtCars_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Class] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspClass_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @ClassName NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Class]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [ClassName]
        , [Subtitle]
        , [Description]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @ClassName
        , @Subtitle
        , @Description
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Class] 
-- =================================================================

CREATE TYPE [dbo].[udtClass] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [ClassName] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Class] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspClass_bulkInsert]
(
@items [dbo].[udtClass] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Class]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [ClassName]
        , [Subtitle]
        , [Description]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Class] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspClass_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @ClassName NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Class]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [ClassName] = @ClassName
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Class] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspClass_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Class]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Class] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspClass_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Class]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Class] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspClass_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Class]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Class] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspClass_selectByPKList]
(
    @pk_list [dbo].[udtClass_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Class] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtClass_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Columns] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspColumns_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @BasementId BIGINT
, @ColumnDiagram NVARCHAR(256)
, @TextDiagrams NVARCHAR(256)
, @ColorScheme NVARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Columns]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [BasementId]
        , [ColumnDiagram]
        , [TextDiagrams]
        , [ColorScheme]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @BasementId
        , @ColumnDiagram
        , @TextDiagrams
        , @ColorScheme
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Columns] 
-- =================================================================

CREATE TYPE [dbo].[udtColumns] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [BasementId] BIGINT
, [ColumnDiagram] NVARCHAR(256)
, [TextDiagrams] NVARCHAR(256)
, [ColorScheme] NVARCHAR(256)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Columns] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspColumns_bulkInsert]
(
@items [dbo].[udtColumns] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Columns]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [BasementId]
        , [ColumnDiagram]
        , [TextDiagrams]
        , [ColorScheme]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Columns] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspColumns_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @BasementId BIGINT
, @ColumnDiagram NVARCHAR(256)
, @TextDiagrams NVARCHAR(256)
, @ColorScheme NVARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Columns]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [BasementId] = @BasementId
        , [ColumnDiagram] = @ColumnDiagram
        , [TextDiagrams] = @TextDiagrams
        , [ColorScheme] = @ColorScheme
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Columns] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspColumns_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Columns]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Columns] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspColumns_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Columns]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Columns] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspColumns_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Columns]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Columns] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspColumns_selectByPKList]
(
    @pk_list [dbo].[udtColumns_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Columns] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtColumns_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Customers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCustomers_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @UserId NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PlaceId BIGINT
, @RoomAndOutNumber NVARCHAR(150)
, @StaffId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Customers]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [UserId]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PlaceId]
        , [RoomAndOutNumber]
        , [StaffId]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @UserId
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @PlaceId
        , @RoomAndOutNumber
        , @StaffId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Customers] 
-- =================================================================

CREATE TYPE [dbo].[udtCustomers] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [UserId] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [PlaceId] BIGINT
, [RoomAndOutNumber] NVARCHAR(150)
, [StaffId] BIGINT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Customers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCustomers_bulkInsert]
(
@items [dbo].[udtCustomers] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Customers]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [UserId]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PlaceId]
        , [RoomAndOutNumber]
        , [StaffId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Customers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCustomers_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @UserId NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PlaceId BIGINT
, @RoomAndOutNumber NVARCHAR(150)
, @StaffId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Customers]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [UserId] = @UserId
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [PlaceId] = @PlaceId
        , [RoomAndOutNumber] = @RoomAndOutNumber
        , [StaffId] = @StaffId
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Customers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCustomers_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Customers]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Customers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCustomers_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Customers]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Customers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCustomers_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Customers]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Customers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspCustomers_selectByPKList]
(
    @pk_list [dbo].[udtCustomers_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Customers] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtCustomers_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[District] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspDistrict_insert]
(
@Id BIGINT OUT
, @DistrictCode BIGINT
, @Name NVARCHAR(200)
, @ProvinceId BIGINT
, @Prefix NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[District]
        (
        [DistrictCode]
        , [Name]
        , [ProvinceId]
        , [Prefix]
        )
	    VALUES 
        (
        @DistrictCode
        , @Name
        , @ProvinceId
        , @Prefix
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[District] 
-- =================================================================

CREATE TYPE [dbo].[udtDistrict] AS TABLE
(
[Id] BIGINT
, [DistrictCode] BIGINT
, [Name] NVARCHAR(200)
, [ProvinceId] BIGINT
, [Prefix] NVARCHAR(MAX)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[District] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspDistrict_bulkInsert]
(
@items [dbo].[udtDistrict] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[District]
        SELECT [DistrictCode]
        , [Name]
        , [ProvinceId]
        , [Prefix]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[District] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspDistrict_update]
(
@Id BIGINT
, @DistrictCode BIGINT
, @Name NVARCHAR(200)
, @ProvinceId BIGINT
, @Prefix NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[District]
        SET 
        [DistrictCode] = @DistrictCode
        , [Name] = @Name
        , [ProvinceId] = @ProvinceId
        , [Prefix] = @Prefix
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[District] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspDistrict_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[District]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[District] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspDistrict_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[District]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[District] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspDistrict_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[District]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[District] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspDistrict_selectByPKList]
(
    @pk_list [dbo].[udtDistrict_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[District] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtDistrict_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Jobs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspJobs_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @BookJobDate DATETIME2
, @AttachmentFile NVARCHAR(MAX)
, @PhotoScheduling NVARCHAR(MAX)
, @ColumnId BIGINT
, @SubscriptionId BIGINT
, @CarId BIGINT
, @SlotInCharge BIGINT
, @SlotSupport BIGINT
, @CheckoutTime DATETIME2
, @ChemicalUsed NVARCHAR(MAX)
, @MaterialsUsed NVARCHAR(MAX)
, @MainPhotoBeforeWiping NVARCHAR(MAX)
, @TheSecondaryPhotoBeforeWiping NVARCHAR(MAX)
, @MainPhotoAfterWiping NVARCHAR(MAX)
, @SubPhotoAfterWiping NVARCHAR(MAX)
, @StaffId BIGINT
, @StaffAssessment NVARCHAR(MAX)
, @StaffScore INT
, @TeamLeadAssessment NVARCHAR(MAX)
, @TeamLeadScore INT
, @CustomerAssessment NVARCHAR(MAX)
, @CustomerScore INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Jobs]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [BookJobDate]
        , [AttachmentFile]
        , [PhotoScheduling]
        , [ColumnId]
        , [SubscriptionId]
        , [CarId]
        , [SlotInCharge]
        , [SlotSupport]
        , [CheckoutTime]
        , [ChemicalUsed]
        , [MaterialsUsed]
        , [MainPhotoBeforeWiping]
        , [TheSecondaryPhotoBeforeWiping]
        , [MainPhotoAfterWiping]
        , [SubPhotoAfterWiping]
        , [StaffId]
        , [StaffAssessment]
        , [StaffScore]
        , [TeamLeadAssessment]
        , [TeamLeadScore]
        , [CustomerAssessment]
        , [CustomerScore]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @BookJobDate
        , @AttachmentFile
        , @PhotoScheduling
        , @ColumnId
        , @SubscriptionId
        , @CarId
        , @SlotInCharge
        , @SlotSupport
        , @CheckoutTime
        , @ChemicalUsed
        , @MaterialsUsed
        , @MainPhotoBeforeWiping
        , @TheSecondaryPhotoBeforeWiping
        , @MainPhotoAfterWiping
        , @SubPhotoAfterWiping
        , @StaffId
        , @StaffAssessment
        , @StaffScore
        , @TeamLeadAssessment
        , @TeamLeadScore
        , @CustomerAssessment
        , @CustomerScore
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Jobs] 
-- =================================================================

CREATE TYPE [dbo].[udtJobs] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [BookJobDate] DATETIME2
, [AttachmentFile] NVARCHAR(MAX)
, [PhotoScheduling] NVARCHAR(MAX)
, [ColumnId] BIGINT
, [SubscriptionId] BIGINT
, [CarId] BIGINT
, [SlotInCharge] BIGINT
, [SlotSupport] BIGINT
, [CheckoutTime] DATETIME2
, [ChemicalUsed] NVARCHAR(MAX)
, [MaterialsUsed] NVARCHAR(MAX)
, [MainPhotoBeforeWiping] NVARCHAR(MAX)
, [TheSecondaryPhotoBeforeWiping] NVARCHAR(MAX)
, [MainPhotoAfterWiping] NVARCHAR(MAX)
, [SubPhotoAfterWiping] NVARCHAR(MAX)
, [StaffId] BIGINT
, [StaffAssessment] NVARCHAR(MAX)
, [StaffScore] INT
, [TeamLeadAssessment] NVARCHAR(MAX)
, [TeamLeadScore] INT
, [CustomerAssessment] NVARCHAR(MAX)
, [CustomerScore] INT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Jobs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspJobs_bulkInsert]
(
@items [dbo].[udtJobs] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Jobs]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [BookJobDate]
        , [AttachmentFile]
        , [PhotoScheduling]
        , [ColumnId]
        , [SubscriptionId]
        , [CarId]
        , [SlotInCharge]
        , [SlotSupport]
        , [CheckoutTime]
        , [ChemicalUsed]
        , [MaterialsUsed]
        , [MainPhotoBeforeWiping]
        , [TheSecondaryPhotoBeforeWiping]
        , [MainPhotoAfterWiping]
        , [SubPhotoAfterWiping]
        , [StaffId]
        , [StaffAssessment]
        , [StaffScore]
        , [TeamLeadAssessment]
        , [TeamLeadScore]
        , [CustomerAssessment]
        , [CustomerScore]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Jobs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspJobs_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @BookJobDate DATETIME2
, @AttachmentFile NVARCHAR(MAX)
, @PhotoScheduling NVARCHAR(MAX)
, @ColumnId BIGINT
, @SubscriptionId BIGINT
, @CarId BIGINT
, @SlotInCharge BIGINT
, @SlotSupport BIGINT
, @CheckoutTime DATETIME2
, @ChemicalUsed NVARCHAR(MAX)
, @MaterialsUsed NVARCHAR(MAX)
, @MainPhotoBeforeWiping NVARCHAR(MAX)
, @TheSecondaryPhotoBeforeWiping NVARCHAR(MAX)
, @MainPhotoAfterWiping NVARCHAR(MAX)
, @SubPhotoAfterWiping NVARCHAR(MAX)
, @StaffId BIGINT
, @StaffAssessment NVARCHAR(MAX)
, @StaffScore INT
, @TeamLeadAssessment NVARCHAR(MAX)
, @TeamLeadScore INT
, @CustomerAssessment NVARCHAR(MAX)
, @CustomerScore INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Jobs]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [BookJobDate] = @BookJobDate
        , [AttachmentFile] = @AttachmentFile
        , [PhotoScheduling] = @PhotoScheduling
        , [ColumnId] = @ColumnId
        , [SubscriptionId] = @SubscriptionId
        , [CarId] = @CarId
        , [SlotInCharge] = @SlotInCharge
        , [SlotSupport] = @SlotSupport
        , [CheckoutTime] = @CheckoutTime
        , [ChemicalUsed] = @ChemicalUsed
        , [MaterialsUsed] = @MaterialsUsed
        , [MainPhotoBeforeWiping] = @MainPhotoBeforeWiping
        , [TheSecondaryPhotoBeforeWiping] = @TheSecondaryPhotoBeforeWiping
        , [MainPhotoAfterWiping] = @MainPhotoAfterWiping
        , [SubPhotoAfterWiping] = @SubPhotoAfterWiping
        , [StaffId] = @StaffId
        , [StaffAssessment] = @StaffAssessment
        , [StaffScore] = @StaffScore
        , [TeamLeadAssessment] = @TeamLeadAssessment
        , [TeamLeadScore] = @TeamLeadScore
        , [CustomerAssessment] = @CustomerAssessment
        , [CustomerScore] = @CustomerScore
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Jobs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspJobs_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Jobs]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Jobs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspJobs_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Jobs]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Jobs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspJobs_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Jobs]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Jobs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspJobs_selectByPKList]
(
    @pk_list [dbo].[udtJobs_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Jobs] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtJobs_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Places] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPlaces_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @PlaceName NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @ProvinceId BIGINT
, @DistrictId BIGINT
, @WardId BIGINT
, @Address NVARCHAR(256)
, @Longitude NVARCHAR(256)
, @Latitude NVARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Places]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [PlaceName]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [ProvinceId]
        , [DistrictId]
        , [WardId]
        , [Address]
        , [Longitude]
        , [Latitude]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @PlaceName
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @ProvinceId
        , @DistrictId
        , @WardId
        , @Address
        , @Longitude
        , @Latitude
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Places] 
-- =================================================================

CREATE TYPE [dbo].[udtPlaces] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [PlaceName] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [ProvinceId] BIGINT
, [DistrictId] BIGINT
, [WardId] BIGINT
, [Address] NVARCHAR(256)
, [Longitude] NVARCHAR(256)
, [Latitude] NVARCHAR(256)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Places] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPlaces_bulkInsert]
(
@items [dbo].[udtPlaces] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Places]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [PlaceName]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [ProvinceId]
        , [DistrictId]
        , [WardId]
        , [Address]
        , [Longitude]
        , [Latitude]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Places] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPlaces_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @PlaceName NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @ProvinceId BIGINT
, @DistrictId BIGINT
, @WardId BIGINT
, @Address NVARCHAR(256)
, @Longitude NVARCHAR(256)
, @Latitude NVARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Places]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [PlaceName] = @PlaceName
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [ProvinceId] = @ProvinceId
        , [DistrictId] = @DistrictId
        , [WardId] = @WardId
        , [Address] = @Address
        , [Longitude] = @Longitude
        , [Latitude] = @Latitude
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Places] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPlaces_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Places]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Places] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPlaces_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Places]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Places] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPlaces_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Places]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Places] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPlaces_selectByPKList]
(
    @pk_list [dbo].[udtPlaces_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Places] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtPlaces_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Prices] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPrices_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PriceClassA DECIMAL
, @PriceClassB DECIMAL
, @PriceClassC DECIMAL
, @PriceClassD DECIMAL
, @PriceClassE DECIMAL
, @PlaceId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Prices]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PriceClassA]
        , [PriceClassB]
        , [PriceClassC]
        , [PriceClassD]
        , [PriceClassE]
        , [PlaceId]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @PriceClassA
        , @PriceClassB
        , @PriceClassC
        , @PriceClassD
        , @PriceClassE
        , @PlaceId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Prices] 
-- =================================================================

CREATE TYPE [dbo].[udtPrices] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [PriceClassA] DECIMAL
, [PriceClassB] DECIMAL
, [PriceClassC] DECIMAL
, [PriceClassD] DECIMAL
, [PriceClassE] DECIMAL
, [PlaceId] BIGINT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Prices] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPrices_bulkInsert]
(
@items [dbo].[udtPrices] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Prices]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PriceClassA]
        , [PriceClassB]
        , [PriceClassC]
        , [PriceClassD]
        , [PriceClassE]
        , [PlaceId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Prices] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPrices_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PriceClassA DECIMAL
, @PriceClassB DECIMAL
, @PriceClassC DECIMAL
, @PriceClassD DECIMAL
, @PriceClassE DECIMAL
, @PlaceId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Prices]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [PriceClassA] = @PriceClassA
        , [PriceClassB] = @PriceClassB
        , [PriceClassC] = @PriceClassC
        , [PriceClassD] = @PriceClassD
        , [PriceClassE] = @PriceClassE
        , [PlaceId] = @PlaceId
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Prices] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPrices_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Prices]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Prices] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPrices_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Prices]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Prices] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPrices_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Prices]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Prices] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspPrices_selectByPKList]
(
    @pk_list [dbo].[udtPrices_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Prices] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtPrices_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Project] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProject_insert]
(
@Id BIGINT OUT
, @ProjectCode BIGINT
, @Name NVARCHAR(200)
, @ProvinceId BIGINT
, @DistrictId BIGINT
, @Lat FLOAT
, @Lng FLOAT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Project]
        (
        [ProjectCode]
        , [Name]
        , [ProvinceId]
        , [DistrictId]
        , [Lat]
        , [Lng]
        )
	    VALUES 
        (
        @ProjectCode
        , @Name
        , @ProvinceId
        , @DistrictId
        , @Lat
        , @Lng
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Project] 
-- =================================================================

CREATE TYPE [dbo].[udtProject] AS TABLE
(
[Id] BIGINT
, [ProjectCode] BIGINT
, [Name] NVARCHAR(200)
, [ProvinceId] BIGINT
, [DistrictId] BIGINT
, [Lat] FLOAT
, [Lng] FLOAT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Project] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProject_bulkInsert]
(
@items [dbo].[udtProject] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Project]
        SELECT [ProjectCode]
        , [Name]
        , [ProvinceId]
        , [DistrictId]
        , [Lat]
        , [Lng]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Project] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProject_update]
(
@Id BIGINT
, @ProjectCode BIGINT
, @Name NVARCHAR(200)
, @ProvinceId BIGINT
, @DistrictId BIGINT
, @Lat FLOAT
, @Lng FLOAT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Project]
        SET 
        [ProjectCode] = @ProjectCode
        , [Name] = @Name
        , [ProvinceId] = @ProvinceId
        , [DistrictId] = @DistrictId
        , [Lat] = @Lat
        , [Lng] = @Lng
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Project] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProject_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Project]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Project] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProject_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Project]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Project] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProject_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Project]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Project] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProject_selectByPKList]
(
    @pk_list [dbo].[udtProject_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Project] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtProject_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Province] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProvince_insert]
(
@Id BIGINT OUT
, @ProvinceCode BIGINT
, @Name NVARCHAR(200)
, @Code NVARCHAR(200)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Province]
        (
        [ProvinceCode]
        , [Name]
        , [Code]
        )
	    VALUES 
        (
        @ProvinceCode
        , @Name
        , @Code
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Province] 
-- =================================================================

CREATE TYPE [dbo].[udtProvince] AS TABLE
(
[Id] BIGINT
, [ProvinceCode] BIGINT
, [Name] NVARCHAR(200)
, [Code] NVARCHAR(200)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Province] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProvince_bulkInsert]
(
@items [dbo].[udtProvince] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Province]
        SELECT [ProvinceCode]
        , [Name]
        , [Code]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Province] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProvince_update]
(
@Id BIGINT
, @ProvinceCode BIGINT
, @Name NVARCHAR(200)
, @Code NVARCHAR(200)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Province]
        SET 
        [ProvinceCode] = @ProvinceCode
        , [Name] = @Name
        , [Code] = @Code
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Province] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProvince_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Province]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Province] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProvince_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Province]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Province] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProvince_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Province]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Province] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspProvince_selectByPKList]
(
    @pk_list [dbo].[udtProvince_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Province] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtProvince_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[RoleClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoleClaims_insert]
(
@Id INT OUT
, @RoleId NVARCHAR(450)
, @ClaimType NVARCHAR(MAX)
, @ClaimValue NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[RoleClaims]
        (
        [RoleId]
        , [ClaimType]
        , [ClaimValue]
        )
	    VALUES 
        (
        @RoleId
        , @ClaimType
        , @ClaimValue
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[RoleClaims] 
-- =================================================================

CREATE TYPE [dbo].[udtRoleClaims] AS TABLE
(
[Id] INT
, [RoleId] NVARCHAR(450)
, [ClaimType] NVARCHAR(MAX)
, [ClaimValue] NVARCHAR(MAX)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[RoleClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoleClaims_bulkInsert]
(
@items [dbo].[udtRoleClaims] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[RoleClaims]
        SELECT [RoleId]
        , [ClaimType]
        , [ClaimValue]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[RoleClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoleClaims_update]
(
@Id INT
, @RoleId NVARCHAR(450)
, @ClaimType NVARCHAR(MAX)
, @ClaimValue NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[RoleClaims]
        SET 
        [RoleId] = @RoleId
        , [ClaimType] = @ClaimType
        , [ClaimValue] = @ClaimValue
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[RoleClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoleClaims_delete]
(
@Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[RoleClaims]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[RoleClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoleClaims_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[RoleClaims]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[RoleClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoleClaims_selectById]
(
@Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[RoleClaims]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[RoleClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoleClaims_selectByPKList]
(
    @pk_list [dbo].[udtRoleClaims_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[RoleClaims] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtRoleClaims_PK] AS TABLE
(
	[Id] INT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Roles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoles_insert]
(
@Id NVARCHAR(450)
, @Name NVARCHAR(256)
, @NormalizedName NVARCHAR(256)
, @ConcurrencyStamp NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Roles]
        (
        [Id]
        , [Name]
        , [NormalizedName]
        , [ConcurrencyStamp]
        )
	    VALUES 
        (
        @Id
        , @Name
        , @NormalizedName
        , @ConcurrencyStamp
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Roles] 
-- =================================================================

CREATE TYPE [dbo].[udtRoles] AS TABLE
(
[Id] NVARCHAR(450)
, [Name] NVARCHAR(256)
, [NormalizedName] NVARCHAR(256)
, [ConcurrencyStamp] NVARCHAR(MAX)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Roles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoles_bulkInsert]
(
@items [dbo].[udtRoles] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Roles]
        SELECT [Id]
        , [Name]
        , [NormalizedName]
        , [ConcurrencyStamp]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Roles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoles_update]
(
@Id NVARCHAR(450)
, @Name NVARCHAR(256)
, @NormalizedName NVARCHAR(256)
, @ConcurrencyStamp NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Roles]
        SET 
        [Id] = @Id
        , [Name] = @Name
        , [NormalizedName] = @NormalizedName
        , [ConcurrencyStamp] = @ConcurrencyStamp
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Roles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoles_delete]
(
@Id NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Roles]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Roles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoles_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Roles]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Roles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoles_selectById]
(
@Id NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Roles]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Roles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRoles_selectByPKList]
(
    @pk_list [dbo].[udtRoles_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Roles] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtRoles_PK] AS TABLE
(
	[Id] NVARCHAR(450)
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Rules] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRules_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @Day DATETIME
, @PlaceId BIGINT
, @SalarySupervisor DECIMAL
, @MinimumQuantity DECIMAL
, @VehicleSizeFactor DECIMAL
, @WeatherCoefficient DECIMAL
, @ContingencyCoefficient DECIMAL
, @SignUpBonus DECIMAL
, @MoldBonus DECIMAL
, @CancellationOfSchedulePenalty DECIMAL
, @PileRegistration DECIMAL
, @DayPayroll NVARCHAR(MAX)
, @LaborWagesA DECIMAL
, @LaborWagesB DECIMAL
, @LaborWagesC DECIMAL
, @LaborWagesD DECIMAL
, @LaborWagesE DECIMAL
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Rules]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [Day]
        , [PlaceId]
        , [SalarySupervisor]
        , [MinimumQuantity]
        , [VehicleSizeFactor]
        , [WeatherCoefficient]
        , [ContingencyCoefficient]
        , [SignUpBonus]
        , [MoldBonus]
        , [CancellationOfSchedulePenalty]
        , [PileRegistration]
        , [DayPayroll]
        , [LaborWagesA]
        , [LaborWagesB]
        , [LaborWagesC]
        , [LaborWagesD]
        , [LaborWagesE]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @Day
        , @PlaceId
        , @SalarySupervisor
        , @MinimumQuantity
        , @VehicleSizeFactor
        , @WeatherCoefficient
        , @ContingencyCoefficient
        , @SignUpBonus
        , @MoldBonus
        , @CancellationOfSchedulePenalty
        , @PileRegistration
        , @DayPayroll
        , @LaborWagesA
        , @LaborWagesB
        , @LaborWagesC
        , @LaborWagesD
        , @LaborWagesE
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Rules] 
-- =================================================================

CREATE TYPE [dbo].[udtRules] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [Day] DATETIME
, [PlaceId] BIGINT
, [SalarySupervisor] DECIMAL
, [MinimumQuantity] DECIMAL
, [VehicleSizeFactor] DECIMAL
, [WeatherCoefficient] DECIMAL
, [ContingencyCoefficient] DECIMAL
, [SignUpBonus] DECIMAL
, [MoldBonus] DECIMAL
, [CancellationOfSchedulePenalty] DECIMAL
, [PileRegistration] DECIMAL
, [DayPayroll] NVARCHAR(MAX)
, [LaborWagesA] DECIMAL
, [LaborWagesB] DECIMAL
, [LaborWagesC] DECIMAL
, [LaborWagesD] DECIMAL
, [LaborWagesE] DECIMAL
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Rules] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRules_bulkInsert]
(
@items [dbo].[udtRules] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Rules]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [Day]
        , [PlaceId]
        , [SalarySupervisor]
        , [MinimumQuantity]
        , [VehicleSizeFactor]
        , [WeatherCoefficient]
        , [ContingencyCoefficient]
        , [SignUpBonus]
        , [MoldBonus]
        , [CancellationOfSchedulePenalty]
        , [PileRegistration]
        , [DayPayroll]
        , [LaborWagesA]
        , [LaborWagesB]
        , [LaborWagesC]
        , [LaborWagesD]
        , [LaborWagesE]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Rules] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRules_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @Day DATETIME
, @PlaceId BIGINT
, @SalarySupervisor DECIMAL
, @MinimumQuantity DECIMAL
, @VehicleSizeFactor DECIMAL
, @WeatherCoefficient DECIMAL
, @ContingencyCoefficient DECIMAL
, @SignUpBonus DECIMAL
, @MoldBonus DECIMAL
, @CancellationOfSchedulePenalty DECIMAL
, @PileRegistration DECIMAL
, @DayPayroll NVARCHAR(MAX)
, @LaborWagesA DECIMAL
, @LaborWagesB DECIMAL
, @LaborWagesC DECIMAL
, @LaborWagesD DECIMAL
, @LaborWagesE DECIMAL
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Rules]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [Day] = @Day
        , [PlaceId] = @PlaceId
        , [SalarySupervisor] = @SalarySupervisor
        , [MinimumQuantity] = @MinimumQuantity
        , [VehicleSizeFactor] = @VehicleSizeFactor
        , [WeatherCoefficient] = @WeatherCoefficient
        , [ContingencyCoefficient] = @ContingencyCoefficient
        , [SignUpBonus] = @SignUpBonus
        , [MoldBonus] = @MoldBonus
        , [CancellationOfSchedulePenalty] = @CancellationOfSchedulePenalty
        , [PileRegistration] = @PileRegistration
        , [DayPayroll] = @DayPayroll
        , [LaborWagesA] = @LaborWagesA
        , [LaborWagesB] = @LaborWagesB
        , [LaborWagesC] = @LaborWagesC
        , [LaborWagesD] = @LaborWagesD
        , [LaborWagesE] = @LaborWagesE
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Rules] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRules_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Rules]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Rules] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRules_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Rules]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Rules] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRules_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Rules]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Rules] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspRules_selectByPKList]
(
    @pk_list [dbo].[udtRules_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Rules] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtRules_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Slots] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSlots_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @Day DATETIME
, @RuleId BIGINT
, @WorkerId BIGINT
, @AutomaticallyGetWorkBeforeDate BIT
, @AutomaticallyAcceptWorkWithinTheDay BIT
, @BookStatus NVARCHAR(10)
, @ReasonCancel NVARCHAR(256)
, @TimeToCome DATETIME
, @TimeToGoHome DATETIME
, @CheckInTime DATETIME
, @CheckOutTime DATETIME
, @CheckInImage NVARCHAR(MAX)
, @CheckOutImage NVARCHAR(MAX)
, @NumberOfRegisteredVehicles INT
, @NumberOfVehiclesReRegistered INT
, @NumberOfBonuses INT
, @SuppliedMaterials NVARCHAR(256)
, @SuppliesReturned NVARCHAR(256)
, @ChemicalLevel NVARCHAR(256)
, @ChemicalReturns NVARCHAR(256)
, @UnexpectedBonus DECIMAL
, @UnexpectedPenalty DECIMAL
, @TotalAmount DECIMAL
, @AmountTransferred DECIMAL
, @MoneyTransferDate DATETIME
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Slots]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [Day]
        , [RuleId]
        , [WorkerId]
        , [AutomaticallyGetWorkBeforeDate]
        , [AutomaticallyAcceptWorkWithinTheDay]
        , [BookStatus]
        , [ReasonCancel]
        , [TimeToCome]
        , [TimeToGoHome]
        , [CheckInTime]
        , [CheckOutTime]
        , [CheckInImage]
        , [CheckOutImage]
        , [NumberOfRegisteredVehicles]
        , [NumberOfVehiclesReRegistered]
        , [NumberOfBonuses]
        , [SuppliedMaterials]
        , [SuppliesReturned]
        , [ChemicalLevel]
        , [ChemicalReturns]
        , [UnexpectedBonus]
        , [UnexpectedPenalty]
        , [TotalAmount]
        , [AmountTransferred]
        , [MoneyTransferDate]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @Day
        , @RuleId
        , @WorkerId
        , @AutomaticallyGetWorkBeforeDate
        , @AutomaticallyAcceptWorkWithinTheDay
        , @BookStatus
        , @ReasonCancel
        , @TimeToCome
        , @TimeToGoHome
        , @CheckInTime
        , @CheckOutTime
        , @CheckInImage
        , @CheckOutImage
        , @NumberOfRegisteredVehicles
        , @NumberOfVehiclesReRegistered
        , @NumberOfBonuses
        , @SuppliedMaterials
        , @SuppliesReturned
        , @ChemicalLevel
        , @ChemicalReturns
        , @UnexpectedBonus
        , @UnexpectedPenalty
        , @TotalAmount
        , @AmountTransferred
        , @MoneyTransferDate
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Slots] 
-- =================================================================

CREATE TYPE [dbo].[udtSlots] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [Day] DATETIME
, [RuleId] BIGINT
, [WorkerId] BIGINT
, [AutomaticallyGetWorkBeforeDate] BIT
, [AutomaticallyAcceptWorkWithinTheDay] BIT
, [BookStatus] NVARCHAR(10)
, [ReasonCancel] NVARCHAR(256)
, [TimeToCome] DATETIME
, [TimeToGoHome] DATETIME
, [CheckInTime] DATETIME
, [CheckOutTime] DATETIME
, [CheckInImage] NVARCHAR(MAX)
, [CheckOutImage] NVARCHAR(MAX)
, [NumberOfRegisteredVehicles] INT
, [NumberOfVehiclesReRegistered] INT
, [NumberOfBonuses] INT
, [SuppliedMaterials] NVARCHAR(256)
, [SuppliesReturned] NVARCHAR(256)
, [ChemicalLevel] NVARCHAR(256)
, [ChemicalReturns] NVARCHAR(256)
, [UnexpectedBonus] DECIMAL
, [UnexpectedPenalty] DECIMAL
, [TotalAmount] DECIMAL
, [AmountTransferred] DECIMAL
, [MoneyTransferDate] DATETIME
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Slots] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSlots_bulkInsert]
(
@items [dbo].[udtSlots] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Slots]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [Day]
        , [RuleId]
        , [WorkerId]
        , [AutomaticallyGetWorkBeforeDate]
        , [AutomaticallyAcceptWorkWithinTheDay]
        , [BookStatus]
        , [ReasonCancel]
        , [TimeToCome]
        , [TimeToGoHome]
        , [CheckInTime]
        , [CheckOutTime]
        , [CheckInImage]
        , [CheckOutImage]
        , [NumberOfRegisteredVehicles]
        , [NumberOfVehiclesReRegistered]
        , [NumberOfBonuses]
        , [SuppliedMaterials]
        , [SuppliesReturned]
        , [ChemicalLevel]
        , [ChemicalReturns]
        , [UnexpectedBonus]
        , [UnexpectedPenalty]
        , [TotalAmount]
        , [AmountTransferred]
        , [MoneyTransferDate]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Slots] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSlots_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @Day DATETIME
, @RuleId BIGINT
, @WorkerId BIGINT
, @AutomaticallyGetWorkBeforeDate BIT
, @AutomaticallyAcceptWorkWithinTheDay BIT
, @BookStatus NVARCHAR(10)
, @ReasonCancel NVARCHAR(256)
, @TimeToCome DATETIME
, @TimeToGoHome DATETIME
, @CheckInTime DATETIME
, @CheckOutTime DATETIME
, @CheckInImage NVARCHAR(MAX)
, @CheckOutImage NVARCHAR(MAX)
, @NumberOfRegisteredVehicles INT
, @NumberOfVehiclesReRegistered INT
, @NumberOfBonuses INT
, @SuppliedMaterials NVARCHAR(256)
, @SuppliesReturned NVARCHAR(256)
, @ChemicalLevel NVARCHAR(256)
, @ChemicalReturns NVARCHAR(256)
, @UnexpectedBonus DECIMAL
, @UnexpectedPenalty DECIMAL
, @TotalAmount DECIMAL
, @AmountTransferred DECIMAL
, @MoneyTransferDate DATETIME
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Slots]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [Day] = @Day
        , [RuleId] = @RuleId
        , [WorkerId] = @WorkerId
        , [AutomaticallyGetWorkBeforeDate] = @AutomaticallyGetWorkBeforeDate
        , [AutomaticallyAcceptWorkWithinTheDay] = @AutomaticallyAcceptWorkWithinTheDay
        , [BookStatus] = @BookStatus
        , [ReasonCancel] = @ReasonCancel
        , [TimeToCome] = @TimeToCome
        , [TimeToGoHome] = @TimeToGoHome
        , [CheckInTime] = @CheckInTime
        , [CheckOutTime] = @CheckOutTime
        , [CheckInImage] = @CheckInImage
        , [CheckOutImage] = @CheckOutImage
        , [NumberOfRegisteredVehicles] = @NumberOfRegisteredVehicles
        , [NumberOfVehiclesReRegistered] = @NumberOfVehiclesReRegistered
        , [NumberOfBonuses] = @NumberOfBonuses
        , [SuppliedMaterials] = @SuppliedMaterials
        , [SuppliesReturned] = @SuppliesReturned
        , [ChemicalLevel] = @ChemicalLevel
        , [ChemicalReturns] = @ChemicalReturns
        , [UnexpectedBonus] = @UnexpectedBonus
        , [UnexpectedPenalty] = @UnexpectedPenalty
        , [TotalAmount] = @TotalAmount
        , [AmountTransferred] = @AmountTransferred
        , [MoneyTransferDate] = @MoneyTransferDate
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Slots] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSlots_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Slots]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Slots] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSlots_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Slots]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Slots] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSlots_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Slots]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Slots] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSlots_selectByPKList]
(
    @pk_list [dbo].[udtSlots_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Slots] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtSlots_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Staffs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStaffs_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @UserId NVARCHAR(200)
, @Title NVARCHAR(50)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PlaceId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Staffs]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [UserId]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PlaceId]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @UserId
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @PlaceId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Staffs] 
-- =================================================================

CREATE TYPE [dbo].[udtStaffs] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [UserId] NVARCHAR(200)
, [Title] NVARCHAR(50)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [PlaceId] BIGINT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Staffs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStaffs_bulkInsert]
(
@items [dbo].[udtStaffs] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Staffs]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [UserId]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PlaceId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Staffs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStaffs_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @UserId NVARCHAR(200)
, @Title NVARCHAR(50)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PlaceId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Staffs]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [UserId] = @UserId
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [PlaceId] = @PlaceId
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Staffs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStaffs_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Staffs]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Staffs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStaffs_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Staffs]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Staffs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStaffs_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Staffs]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Staffs] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStaffs_selectByPKList]
(
    @pk_list [dbo].[udtStaffs_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Staffs] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtStaffs_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Street] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStreet_insert]
(
@Id BIGINT OUT
, @StreetCode BIGINT
, @Name NVARCHAR(100)
, @Prefix NVARCHAR(20)
, @ProvinceId BIGINT
, @DistrictId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Street]
        (
        [StreetCode]
        , [Name]
        , [Prefix]
        , [ProvinceId]
        , [DistrictId]
        )
	    VALUES 
        (
        @StreetCode
        , @Name
        , @Prefix
        , @ProvinceId
        , @DistrictId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Street] 
-- =================================================================

CREATE TYPE [dbo].[udtStreet] AS TABLE
(
[Id] BIGINT
, [StreetCode] BIGINT
, [Name] NVARCHAR(100)
, [Prefix] NVARCHAR(20)
, [ProvinceId] BIGINT
, [DistrictId] BIGINT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Street] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStreet_bulkInsert]
(
@items [dbo].[udtStreet] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Street]
        SELECT [StreetCode]
        , [Name]
        , [Prefix]
        , [ProvinceId]
        , [DistrictId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Street] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStreet_update]
(
@Id BIGINT
, @StreetCode BIGINT
, @Name NVARCHAR(100)
, @Prefix NVARCHAR(20)
, @ProvinceId BIGINT
, @DistrictId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Street]
        SET 
        [StreetCode] = @StreetCode
        , [Name] = @Name
        , [Prefix] = @Prefix
        , [ProvinceId] = @ProvinceId
        , [DistrictId] = @DistrictId
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Street] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStreet_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Street]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Street] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStreet_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Street]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Street] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStreet_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Street]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Street] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspStreet_selectByPKList]
(
    @pk_list [dbo].[udtStreet_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Street] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtStreet_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Subscriptions] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSubscriptions_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @CarId BIGINT
, @StaffId BIGINT
, @PriceId BIGINT
, @DateOfPurchase DATETIME
, @ExpirationDate DATETIME
, @NumberOfMonthsOfPurchase BIGINT
, @DateOfPayment DATETIME
, @AttachmentFile NVARCHAR(MAX)
, @ExtraGifts BIT
, @StartDate DATETIME
, @EndDate DATETIME
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Subscriptions]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [CarId]
        , [StaffId]
        , [PriceId]
        , [DateOfPurchase]
        , [ExpirationDate]
        , [NumberOfMonthsOfPurchase]
        , [DateOfPayment]
        , [AttachmentFile]
        , [ExtraGifts]
        , [StartDate]
        , [EndDate]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @CarId
        , @StaffId
        , @PriceId
        , @DateOfPurchase
        , @ExpirationDate
        , @NumberOfMonthsOfPurchase
        , @DateOfPayment
        , @AttachmentFile
        , @ExtraGifts
        , @StartDate
        , @EndDate
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Subscriptions] 
-- =================================================================

CREATE TYPE [dbo].[udtSubscriptions] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [CarId] BIGINT
, [StaffId] BIGINT
, [PriceId] BIGINT
, [DateOfPurchase] DATETIME
, [ExpirationDate] DATETIME
, [NumberOfMonthsOfPurchase] BIGINT
, [DateOfPayment] DATETIME
, [AttachmentFile] NVARCHAR(MAX)
, [ExtraGifts] BIT
, [StartDate] DATETIME
, [EndDate] DATETIME
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Subscriptions] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSubscriptions_bulkInsert]
(
@items [dbo].[udtSubscriptions] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Subscriptions]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [CarId]
        , [StaffId]
        , [PriceId]
        , [DateOfPurchase]
        , [ExpirationDate]
        , [NumberOfMonthsOfPurchase]
        , [DateOfPayment]
        , [AttachmentFile]
        , [ExtraGifts]
        , [StartDate]
        , [EndDate]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Subscriptions] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSubscriptions_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @CarId BIGINT
, @StaffId BIGINT
, @PriceId BIGINT
, @DateOfPurchase DATETIME
, @ExpirationDate DATETIME
, @NumberOfMonthsOfPurchase BIGINT
, @DateOfPayment DATETIME
, @AttachmentFile NVARCHAR(MAX)
, @ExtraGifts BIT
, @StartDate DATETIME
, @EndDate DATETIME
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Subscriptions]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [CarId] = @CarId
        , [StaffId] = @StaffId
        , [PriceId] = @PriceId
        , [DateOfPurchase] = @DateOfPurchase
        , [ExpirationDate] = @ExpirationDate
        , [NumberOfMonthsOfPurchase] = @NumberOfMonthsOfPurchase
        , [DateOfPayment] = @DateOfPayment
        , [AttachmentFile] = @AttachmentFile
        , [ExtraGifts] = @ExtraGifts
        , [StartDate] = @StartDate
        , [EndDate] = @EndDate
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Subscriptions] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSubscriptions_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Subscriptions]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Subscriptions] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSubscriptions_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Subscriptions]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Subscriptions] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSubscriptions_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Subscriptions]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Subscriptions] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspSubscriptions_selectByPKList]
(
    @pk_list [dbo].[udtSubscriptions_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Subscriptions] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtSubscriptions_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Teams] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspTeams_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PlaceId BIGINT
, @AreaId BIGINT
, @CaptainWorkerId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Teams]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PlaceId]
        , [AreaId]
        , [CaptainWorkerId]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @PlaceId
        , @AreaId
        , @CaptainWorkerId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Teams] 
-- =================================================================

CREATE TYPE [dbo].[udtTeams] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [PlaceId] BIGINT
, [AreaId] BIGINT
, [CaptainWorkerId] BIGINT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Teams] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspTeams_bulkInsert]
(
@items [dbo].[udtTeams] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Teams]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [PlaceId]
        , [AreaId]
        , [CaptainWorkerId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Teams] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspTeams_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @PlaceId BIGINT
, @AreaId BIGINT
, @CaptainWorkerId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Teams]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [PlaceId] = @PlaceId
        , [AreaId] = @AreaId
        , [CaptainWorkerId] = @CaptainWorkerId
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Teams] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspTeams_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Teams]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Teams] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspTeams_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Teams]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Teams] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspTeams_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Teams]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Teams] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspTeams_selectByPKList]
(
    @pk_list [dbo].[udtTeams_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Teams] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtTeams_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[UserClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserClaims_insert]
(
@Id INT OUT
, @UserId NVARCHAR(450)
, @ClaimType NVARCHAR(MAX)
, @ClaimValue NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserClaims]
        (
        [UserId]
        , [ClaimType]
        , [ClaimValue]
        )
	    VALUES 
        (
        @UserId
        , @ClaimType
        , @ClaimValue
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[UserClaims] 
-- =================================================================

CREATE TYPE [dbo].[udtUserClaims] AS TABLE
(
[Id] INT
, [UserId] NVARCHAR(450)
, [ClaimType] NVARCHAR(MAX)
, [ClaimValue] NVARCHAR(MAX)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[UserClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserClaims_bulkInsert]
(
@items [dbo].[udtUserClaims] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserClaims]
        SELECT [UserId]
        , [ClaimType]
        , [ClaimValue]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[UserClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserClaims_update]
(
@Id INT
, @UserId NVARCHAR(450)
, @ClaimType NVARCHAR(MAX)
, @ClaimValue NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[UserClaims]
        SET 
        [UserId] = @UserId
        , [ClaimType] = @ClaimType
        , [ClaimValue] = @ClaimValue
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[UserClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserClaims_delete]
(
@Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[UserClaims]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[UserClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserClaims_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserClaims]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[UserClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserClaims_selectById]
(
@Id INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserClaims]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[UserClaims] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserClaims_selectByPKList]
(
    @pk_list [dbo].[udtUserClaims_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[UserClaims] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtUserClaims_PK] AS TABLE
(
	[Id] INT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[UserLogins] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserLogins_insert]
(
@LoginProvider NVARCHAR(450)
, @ProviderKey NVARCHAR(450)
, @ProviderDisplayName NVARCHAR(MAX)
, @UserId NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserLogins]
        (
        [LoginProvider]
        , [ProviderKey]
        , [ProviderDisplayName]
        , [UserId]
        )
	    VALUES 
        (
        @LoginProvider
        , @ProviderKey
        , @ProviderDisplayName
        , @UserId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[UserLogins] 
-- =================================================================

CREATE TYPE [dbo].[udtUserLogins] AS TABLE
(
[LoginProvider] NVARCHAR(450)
, [ProviderKey] NVARCHAR(450)
, [ProviderDisplayName] NVARCHAR(MAX)
, [UserId] NVARCHAR(450)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[UserLogins] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserLogins_bulkInsert]
(
@items [dbo].[udtUserLogins] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserLogins]
        SELECT [LoginProvider]
        , [ProviderKey]
        , [ProviderDisplayName]
        , [UserId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[UserLogins] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserLogins_update]
(
@LoginProvider NVARCHAR(450)
, @ProviderKey NVARCHAR(450)
, @ProviderDisplayName NVARCHAR(MAX)
, @UserId NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[UserLogins]
        SET 
        [LoginProvider] = @LoginProvider
        , [ProviderKey] = @ProviderKey
        , [ProviderDisplayName] = @ProviderDisplayName
        , [UserId] = @UserId
        WHERE 
        [LoginProvider] = @LoginProvider AND [ProviderKey] = @ProviderKey
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[UserLogins] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserLogins_delete]
(
@LoginProvider NVARCHAR(450)
, @ProviderKey NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[UserLogins]
	    WHERE [LoginProvider] = @LoginProvider AND [ProviderKey] = @ProviderKey

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[UserLogins] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserLogins_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserLogins]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[UserLogins] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserLogins_selectByLoginProviderAndProviderKey]
(
@LoginProvider NVARCHAR(450)
, @ProviderKey NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserLogins]
        WHERE [LoginProvider] = @LoginProvider AND [ProviderKey] = @ProviderKey
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[UserLogins] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserLogins_selectByPKList]
(
    @pk_list [dbo].[udtUserLogins_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[UserLogins] [A]
        INNER JOIN @pk_list [B] ON [A].[LoginProvider] = [B].[LoginProvider]
        AND INNER JOIN @pk_list [B] ON [A].[ProviderKey] = [B].[ProviderKey]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtUserLogins_PK] AS TABLE
(
	[LoginProvider] NVARCHAR(450)
, [ProviderKey] NVARCHAR(450)
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[UserProfile] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserProfile_insert]
(
@Id NVARCHAR(450)
, @UserName NVARCHAR(256)
, @NormalizedUserName NVARCHAR(256)
, @Email NVARCHAR(256)
, @NormalizedEmail NVARCHAR(256)
, @EmailConfirmed BIT
, @PasswordHash NVARCHAR(MAX)
, @SecurityStamp NVARCHAR(MAX)
, @ConcurrencyStamp NVARCHAR(MAX)
, @PhoneNumber NVARCHAR(MAX)
, @PhoneNumberConfirmed BIT
, @TwoFactorEnabled BIT
, @LockoutEnd DATETIMEOFFSET
, @LockoutEnabled BIT
, @AccessFailedCount INT
, @IsDeleted BIT
, @CreatedDate DATETIME2
, @Address NVARCHAR(255)
, @ProvinceId BIGINT
, @StreetId BIGINT
, @DistrictId BIGINT
, @WardId BIGINT
, @Gender NVARCHAR(255)
, @DateOfBirth DATETIME2
, @Active BIT
, @Facebook NVARCHAR(50)
, @Zalo NVARCHAR(50)
, @GoogleId NVARCHAR(255)
, @PictureUrl NVARCHAR(255)
, @PhoneNumberOther NVARCHAR(11)
, @FullName NVARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserProfile]
        (
        [Id]
        , [UserName]
        , [NormalizedUserName]
        , [Email]
        , [NormalizedEmail]
        , [EmailConfirmed]
        , [PasswordHash]
        , [SecurityStamp]
        , [ConcurrencyStamp]
        , [PhoneNumber]
        , [PhoneNumberConfirmed]
        , [TwoFactorEnabled]
        , [LockoutEnd]
        , [LockoutEnabled]
        , [AccessFailedCount]
        , [IsDeleted]
        , [CreatedDate]
        , [Address]
        , [ProvinceId]
        , [StreetId]
        , [DistrictId]
        , [WardId]
        , [Gender]
        , [DateOfBirth]
        , [Active]
        , [Facebook]
        , [Zalo]
        , [GoogleId]
        , [PictureUrl]
        , [PhoneNumberOther]
        , [FullName]
        )
	    VALUES 
        (
        @Id
        , @UserName
        , @NormalizedUserName
        , @Email
        , @NormalizedEmail
        , @EmailConfirmed
        , @PasswordHash
        , @SecurityStamp
        , @ConcurrencyStamp
        , @PhoneNumber
        , @PhoneNumberConfirmed
        , @TwoFactorEnabled
        , @LockoutEnd
        , @LockoutEnabled
        , @AccessFailedCount
        , @IsDeleted
        , @CreatedDate
        , @Address
        , @ProvinceId
        , @StreetId
        , @DistrictId
        , @WardId
        , @Gender
        , @DateOfBirth
        , @Active
        , @Facebook
        , @Zalo
        , @GoogleId
        , @PictureUrl
        , @PhoneNumberOther
        , @FullName
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[UserProfile] 
-- =================================================================

CREATE TYPE [dbo].[udtUserProfile] AS TABLE
(
[Id] NVARCHAR(450)
, [UserName] NVARCHAR(256)
, [NormalizedUserName] NVARCHAR(256)
, [Email] NVARCHAR(256)
, [NormalizedEmail] NVARCHAR(256)
, [EmailConfirmed] BIT
, [PasswordHash] NVARCHAR(MAX)
, [SecurityStamp] NVARCHAR(MAX)
, [ConcurrencyStamp] NVARCHAR(MAX)
, [PhoneNumber] NVARCHAR(MAX)
, [PhoneNumberConfirmed] BIT
, [TwoFactorEnabled] BIT
, [LockoutEnd] DATETIMEOFFSET
, [LockoutEnabled] BIT
, [AccessFailedCount] INT
, [IsDeleted] BIT
, [CreatedDate] DATETIME2
, [Address] NVARCHAR(255)
, [ProvinceId] BIGINT
, [StreetId] BIGINT
, [DistrictId] BIGINT
, [WardId] BIGINT
, [Gender] NVARCHAR(255)
, [DateOfBirth] DATETIME2
, [Active] BIT
, [Facebook] NVARCHAR(50)
, [Zalo] NVARCHAR(50)
, [GoogleId] NVARCHAR(255)
, [PictureUrl] NVARCHAR(255)
, [PhoneNumberOther] NVARCHAR(11)
, [FullName] NVARCHAR(255)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[UserProfile] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserProfile_bulkInsert]
(
@items [dbo].[udtUserProfile] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserProfile]
        SELECT [Id]
        , [UserName]
        , [NormalizedUserName]
        , [Email]
        , [NormalizedEmail]
        , [EmailConfirmed]
        , [PasswordHash]
        , [SecurityStamp]
        , [ConcurrencyStamp]
        , [PhoneNumber]
        , [PhoneNumberConfirmed]
        , [TwoFactorEnabled]
        , [LockoutEnd]
        , [LockoutEnabled]
        , [AccessFailedCount]
        , [IsDeleted]
        , [CreatedDate]
        , [Address]
        , [ProvinceId]
        , [StreetId]
        , [DistrictId]
        , [WardId]
        , [Gender]
        , [DateOfBirth]
        , [Active]
        , [Facebook]
        , [Zalo]
        , [GoogleId]
        , [PictureUrl]
        , [PhoneNumberOther]
        , [FullName]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[UserProfile] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserProfile_update]
(
@Id NVARCHAR(450)
, @UserName NVARCHAR(256)
, @NormalizedUserName NVARCHAR(256)
, @Email NVARCHAR(256)
, @NormalizedEmail NVARCHAR(256)
, @EmailConfirmed BIT
, @PasswordHash NVARCHAR(MAX)
, @SecurityStamp NVARCHAR(MAX)
, @ConcurrencyStamp NVARCHAR(MAX)
, @PhoneNumber NVARCHAR(MAX)
, @PhoneNumberConfirmed BIT
, @TwoFactorEnabled BIT
, @LockoutEnd DATETIMEOFFSET
, @LockoutEnabled BIT
, @AccessFailedCount INT
, @IsDeleted BIT
, @CreatedDate DATETIME2
, @Address NVARCHAR(255)
, @ProvinceId BIGINT
, @StreetId BIGINT
, @DistrictId BIGINT
, @WardId BIGINT
, @Gender NVARCHAR(255)
, @DateOfBirth DATETIME2
, @Active BIT
, @Facebook NVARCHAR(50)
, @Zalo NVARCHAR(50)
, @GoogleId NVARCHAR(255)
, @PictureUrl NVARCHAR(255)
, @PhoneNumberOther NVARCHAR(11)
, @FullName NVARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[UserProfile]
        SET 
        [Id] = @Id
        , [UserName] = @UserName
        , [NormalizedUserName] = @NormalizedUserName
        , [Email] = @Email
        , [NormalizedEmail] = @NormalizedEmail
        , [EmailConfirmed] = @EmailConfirmed
        , [PasswordHash] = @PasswordHash
        , [SecurityStamp] = @SecurityStamp
        , [ConcurrencyStamp] = @ConcurrencyStamp
        , [PhoneNumber] = @PhoneNumber
        , [PhoneNumberConfirmed] = @PhoneNumberConfirmed
        , [TwoFactorEnabled] = @TwoFactorEnabled
        , [LockoutEnd] = @LockoutEnd
        , [LockoutEnabled] = @LockoutEnabled
        , [AccessFailedCount] = @AccessFailedCount
        , [IsDeleted] = @IsDeleted
        , [CreatedDate] = @CreatedDate
        , [Address] = @Address
        , [ProvinceId] = @ProvinceId
        , [StreetId] = @StreetId
        , [DistrictId] = @DistrictId
        , [WardId] = @WardId
        , [Gender] = @Gender
        , [DateOfBirth] = @DateOfBirth
        , [Active] = @Active
        , [Facebook] = @Facebook
        , [Zalo] = @Zalo
        , [GoogleId] = @GoogleId
        , [PictureUrl] = @PictureUrl
        , [PhoneNumberOther] = @PhoneNumberOther
        , [FullName] = @FullName
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[UserProfile] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserProfile_delete]
(
@Id NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[UserProfile]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[UserProfile] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserProfile_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserProfile]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[UserProfile] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserProfile_selectById]
(
@Id NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserProfile]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[UserProfile] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserProfile_selectByPKList]
(
    @pk_list [dbo].[udtUserProfile_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[UserProfile] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtUserProfile_PK] AS TABLE
(
	[Id] NVARCHAR(450)
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[UserRoles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserRoles_insert]
(
@UserId NVARCHAR(450)
, @RoleId NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserRoles]
        (
        [UserId]
        , [RoleId]
        )
	    VALUES 
        (
        @UserId
        , @RoleId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[UserRoles] 
-- =================================================================

CREATE TYPE [dbo].[udtUserRoles] AS TABLE
(
[UserId] NVARCHAR(450)
, [RoleId] NVARCHAR(450)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[UserRoles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserRoles_bulkInsert]
(
@items [dbo].[udtUserRoles] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserRoles]
        SELECT [UserId]
        , [RoleId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[UserRoles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserRoles_update]
(
@UserId NVARCHAR(450)
, @RoleId NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[UserRoles]
        SET 
        [UserId] = @UserId
        , [RoleId] = @RoleId
        WHERE 
        [UserId] = @UserId AND [RoleId] = @RoleId
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[UserRoles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserRoles_delete]
(
@UserId NVARCHAR(450)
, @RoleId NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[UserRoles]
	    WHERE [UserId] = @UserId AND [RoleId] = @RoleId

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[UserRoles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserRoles_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserRoles]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[UserRoles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserRoles_selectByUserIdAndRoleId]
(
@UserId NVARCHAR(450)
, @RoleId NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserRoles]
        WHERE [UserId] = @UserId AND [RoleId] = @RoleId
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[UserRoles] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserRoles_selectByPKList]
(
    @pk_list [dbo].[udtUserRoles_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[UserRoles] [A]
        INNER JOIN @pk_list [B] ON [A].[UserId] = [B].[UserId]
        AND INNER JOIN @pk_list [B] ON [A].[RoleId] = [B].[RoleId]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtUserRoles_PK] AS TABLE
(
	[UserId] NVARCHAR(450)
, [RoleId] NVARCHAR(450)
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[UserTokens] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserTokens_insert]
(
@UserId NVARCHAR(450)
, @LoginProvider NVARCHAR(450)
, @Name NVARCHAR(450)
, @Value NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserTokens]
        (
        [UserId]
        , [LoginProvider]
        , [Name]
        , [Value]
        )
	    VALUES 
        (
        @UserId
        , @LoginProvider
        , @Name
        , @Value
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[UserTokens] 
-- =================================================================

CREATE TYPE [dbo].[udtUserTokens] AS TABLE
(
[UserId] NVARCHAR(450)
, [LoginProvider] NVARCHAR(450)
, [Name] NVARCHAR(450)
, [Value] NVARCHAR(MAX)
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[UserTokens] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserTokens_bulkInsert]
(
@items [dbo].[udtUserTokens] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[UserTokens]
        SELECT [UserId]
        , [LoginProvider]
        , [Name]
        , [Value]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[UserTokens] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserTokens_update]
(
@UserId NVARCHAR(450)
, @LoginProvider NVARCHAR(450)
, @Name NVARCHAR(450)
, @Value NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[UserTokens]
        SET 
        [UserId] = @UserId
        , [LoginProvider] = @LoginProvider
        , [Name] = @Name
        , [Value] = @Value
        WHERE 
        [UserId] = @UserId AND [LoginProvider] = @LoginProvider AND [Name] = @Name
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[UserTokens] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserTokens_delete]
(
@UserId NVARCHAR(450)
, @LoginProvider NVARCHAR(450)
, @Name NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[UserTokens]
	    WHERE [UserId] = @UserId AND [LoginProvider] = @LoginProvider AND [Name] = @Name

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[UserTokens] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserTokens_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserTokens]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[UserTokens] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserTokens_selectByUserIdAndLoginProviderAndName]
(
@UserId NVARCHAR(450)
, @LoginProvider NVARCHAR(450)
, @Name NVARCHAR(450)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[UserTokens]
        WHERE [UserId] = @UserId AND [LoginProvider] = @LoginProvider AND [Name] = @Name
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[UserTokens] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspUserTokens_selectByPKList]
(
    @pk_list [dbo].[udtUserTokens_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[UserTokens] [A]
        INNER JOIN @pk_list [B] ON [A].[UserId] = [B].[UserId]
        AND INNER JOIN @pk_list [B] ON [A].[LoginProvider] = [B].[LoginProvider]
        AND INNER JOIN @pk_list [B] ON [A].[Name] = [B].[Name]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtUserTokens_PK] AS TABLE
(
	[UserId] NVARCHAR(450)
, [LoginProvider] NVARCHAR(450)
, [Name] NVARCHAR(450)
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Ward] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWard_insert]
(
@Id BIGINT OUT
, @WardCode BIGINT
, @Name NVARCHAR(100)
, @Prefix NVARCHAR(20)
, @ProvinceId BIGINT
, @DistrictId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Ward]
        (
        [WardCode]
        , [Name]
        , [Prefix]
        , [ProvinceId]
        , [DistrictId]
        )
	    VALUES 
        (
        @WardCode
        , @Name
        , @Prefix
        , @ProvinceId
        , @DistrictId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Ward] 
-- =================================================================

CREATE TYPE [dbo].[udtWard] AS TABLE
(
[Id] BIGINT
, [WardCode] BIGINT
, [Name] NVARCHAR(100)
, [Prefix] NVARCHAR(20)
, [ProvinceId] BIGINT
, [DistrictId] BIGINT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Ward] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWard_bulkInsert]
(
@items [dbo].[udtWard] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Ward]
        SELECT [WardCode]
        , [Name]
        , [Prefix]
        , [ProvinceId]
        , [DistrictId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Ward] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWard_update]
(
@Id BIGINT
, @WardCode BIGINT
, @Name NVARCHAR(100)
, @Prefix NVARCHAR(20)
, @ProvinceId BIGINT
, @DistrictId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Ward]
        SET 
        [WardCode] = @WardCode
        , [Name] = @Name
        , [Prefix] = @Prefix
        , [ProvinceId] = @ProvinceId
        , [DistrictId] = @DistrictId
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Ward] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWard_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Ward]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Ward] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWard_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Ward]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Ward] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWard_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Ward]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Ward] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWard_selectByPKList]
(
    @pk_list [dbo].[udtWard_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Ward] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtWard_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Withdraws] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWithdraws_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @AmountOfWithdrawal NVARCHAR(13)
, @AmountBeforeWithdrawal NVARCHAR(13)
, @AmountAfterWithdrawal NVARCHAR(13)
, @TimeToWithdraw DATETIME
, @ReceivingTime DATETIME
, @WorkerId BIGINT
, @StaffId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Withdraws]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [AmountOfWithdrawal]
        , [AmountBeforeWithdrawal]
        , [AmountAfterWithdrawal]
        , [TimeToWithdraw]
        , [ReceivingTime]
        , [WorkerId]
        , [StaffId]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @AmountOfWithdrawal
        , @AmountBeforeWithdrawal
        , @AmountAfterWithdrawal
        , @TimeToWithdraw
        , @ReceivingTime
        , @WorkerId
        , @StaffId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Withdraws] 
-- =================================================================

CREATE TYPE [dbo].[udtWithdraws] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [AmountOfWithdrawal] NVARCHAR(13)
, [AmountBeforeWithdrawal] NVARCHAR(13)
, [AmountAfterWithdrawal] NVARCHAR(13)
, [TimeToWithdraw] DATETIME
, [ReceivingTime] DATETIME
, [WorkerId] BIGINT
, [StaffId] BIGINT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Withdraws] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWithdraws_bulkInsert]
(
@items [dbo].[udtWithdraws] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Withdraws]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [AmountOfWithdrawal]
        , [AmountBeforeWithdrawal]
        , [AmountAfterWithdrawal]
        , [TimeToWithdraw]
        , [ReceivingTime]
        , [WorkerId]
        , [StaffId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Withdraws] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWithdraws_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @AmountOfWithdrawal NVARCHAR(13)
, @AmountBeforeWithdrawal NVARCHAR(13)
, @AmountAfterWithdrawal NVARCHAR(13)
, @TimeToWithdraw DATETIME
, @ReceivingTime DATETIME
, @WorkerId BIGINT
, @StaffId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Withdraws]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [AmountOfWithdrawal] = @AmountOfWithdrawal
        , [AmountBeforeWithdrawal] = @AmountBeforeWithdrawal
        , [AmountAfterWithdrawal] = @AmountAfterWithdrawal
        , [TimeToWithdraw] = @TimeToWithdraw
        , [ReceivingTime] = @ReceivingTime
        , [WorkerId] = @WorkerId
        , [StaffId] = @StaffId
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Withdraws] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWithdraws_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Withdraws]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Withdraws] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWithdraws_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Withdraws]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Withdraws] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWithdraws_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Withdraws]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Withdraws] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWithdraws_selectByPKList]
(
    @pk_list [dbo].[udtWithdraws_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Withdraws] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtWithdraws_PK] AS TABLE
(
	[Id] BIGINT
)

GO


 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Insert Procedure for the table [dbo].[Workers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWorkers_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @UserId NVARCHAR(200)
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @IdentificationNumber NVARCHAR(15)
, @DateRange DATETIME
, @ProvincialLevel NVARCHAR(MAX)
, @CurrentJob NVARCHAR(256)
, @CurrentAgency NVARCHAR(256)
, @CurrentAccommodation NVARCHAR(256)
, @Official NVARCHAR(256)
, @PlaceId BIGINT
, @MoneyInWallet NVARCHAR(13)
, @TeamId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Workers]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [UserId]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [IdentificationNumber]
        , [DateRange]
        , [ProvincialLevel]
        , [CurrentJob]
        , [CurrentAgency]
        , [CurrentAccommodation]
        , [Official]
        , [PlaceId]
        , [MoneyInWallet]
        , [TeamId]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @UserId
        , @Title
        , @Subtitle
        , @Description
        , @AttachmentFile
        , @IdentificationNumber
        , @DateRange
        , @ProvincialLevel
        , @CurrentJob
        , @CurrentAgency
        , @CurrentAccommodation
        , @Official
        , @PlaceId
        , @MoneyInWallet
        , @TeamId
        )

        SET @id = SCOPE_IDENTITY()

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Type declaration for table [dbo].[Workers] 
-- =================================================================

CREATE TYPE [dbo].[udtWorkers] AS TABLE
(
[Id] BIGINT
, [History] NVARCHAR(256)
, [Chat] NVARCHAR(256)
, [CreatedBy] NVARCHAR(50)
, [CreatedDate] DATETIME
, [ModifiedBy] NVARCHAR(50)
, [ModifiedDate] DATETIME
, [IsDeleted] BIT
, [UserId] NVARCHAR(200)
, [Title] NVARCHAR(200)
, [Subtitle] NVARCHAR(200)
, [Description] NVARCHAR(256)
, [AttachmentFile] NVARCHAR(MAX)
, [IdentificationNumber] NVARCHAR(15)
, [DateRange] DATETIME
, [ProvincialLevel] NVARCHAR(MAX)
, [CurrentJob] NVARCHAR(256)
, [CurrentAgency] NVARCHAR(256)
, [CurrentAccommodation] NVARCHAR(256)
, [Official] NVARCHAR(256)
, [PlaceId] BIGINT
, [MoneyInWallet] NVARCHAR(13)
, [TeamId] BIGINT
)

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Bulk Insert Procedure for the table [dbo].[Workers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWorkers_bulkInsert]
(
@items [dbo].[udtWorkers] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        INSERT INTO [dbo].[Workers]
        SELECT [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [UserId]
        , [Title]
        , [Subtitle]
        , [Description]
        , [AttachmentFile]
        , [IdentificationNumber]
        , [DateRange]
        , [ProvincialLevel]
        , [CurrentJob]
        , [CurrentAgency]
        , [CurrentAccommodation]
        , [Official]
        , [PlaceId]
        , [MoneyInWallet]
        , [TeamId]
	    FROM @items

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Update Procedure for the table [dbo].[Workers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWorkers_update]
(
@Id BIGINT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @UserId NVARCHAR(200)
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @IdentificationNumber NVARCHAR(15)
, @DateRange DATETIME
, @ProvincialLevel NVARCHAR(MAX)
, @CurrentJob NVARCHAR(256)
, @CurrentAgency NVARCHAR(256)
, @CurrentAccommodation NVARCHAR(256)
, @Official NVARCHAR(256)
, @PlaceId BIGINT
, @MoneyInWallet NVARCHAR(13)
, @TeamId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        UPDATE [dbo].[Workers]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [UserId] = @UserId
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
        , [AttachmentFile] = @AttachmentFile
        , [IdentificationNumber] = @IdentificationNumber
        , [DateRange] = @DateRange
        , [ProvincialLevel] = @ProvincialLevel
        , [CurrentJob] = @CurrentJob
        , [CurrentAgency] = @CurrentAgency
        , [CurrentAccommodation] = @CurrentAccommodation
        , [Official] = @Official
        , [PlaceId] = @PlaceId
        , [MoneyInWallet] = @MoneyInWallet
        , [TeamId] = @TeamId
        WHERE 
        [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Delete Procedure for the table [dbo].[Workers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWorkers_delete]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        DELETE FROM [dbo].[Workers]
	    WHERE [Id] = @Id

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select All Procedure for the table [dbo].[Workers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWorkers_selectAll]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Workers]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PK Procedure for the table [dbo].[Workers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWorkers_selectById]
(
@Id BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT * FROM [dbo].[Workers]
        WHERE [Id] = @Id
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




 
-- =================================================================
-- Author: MSSQL-Dapper Generator
-- Description:	Select By PKList Procedure for the table [dbo].[Workers] 
-- =================================================================

CREATE PROCEDURE [dbo].[uspWorkers_selectByPKList]
(
    @pk_list [dbo].[udtWorkers_PK] READONLY
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY
    
        SELECT [A].*
        FROM [dbo].[Workers] [A]
        INNER JOIN @pk_list [B] ON [A].[Id] = [B].[Id]
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
GO




CREATE TYPE [dbo].[udtWorkers_PK] AS TABLE
(
	[Id] BIGINT
)

GO


