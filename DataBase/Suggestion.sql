
    GO
    /****** Object:  StoredProcedure [dbo].[uspAreas_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Areas] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspAreas_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, MapOfTunnelsInTheArea as Value FROM [dbo].MapOfTunnelsInTheArea a where a.Title like N'%'+@key+'%'
            ORDER BY MapOfTunnelsInTheArea ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspBasements_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Basements] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspBasements_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Basements] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspBrands_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Brands] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspBrands_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, BrandName as Value FROM [dbo].[Brands] a where a.BrandName like N'%'+@key+'%'
            ORDER BY BrandName ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspCarModels_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[CarModels] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspCarModels_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, [Name] as Value FROM [dbo].[CarModels] a where a.[Name] like N'%'+@key+'%'
            ORDER BY [Name] ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspCars_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Cars] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspCars_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Cars] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspClass_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Class] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspClass_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, ClassName as Value FROM [dbo].[Class] a where a.ClassName like N'%'+@key+'%'
            ORDER BY ClassName ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspColumns_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Columns] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspColumns_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Columns] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspCustomers_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Customers] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspCustomers_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Subtitle as Value FROM [dbo].[Customers] a where a.Subtitle like N'%'+@key+'%'
            ORDER BY Subtitle ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspDistrict_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[District] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspDistrict_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT DistrictCode as Id, [Name] as Value FROM [dbo].[District] a where a.[Name] like N'%'+@key+'%'
            ORDER BY [Name] ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspJobs_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Jobs] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspJobs_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Jobs] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspPlaces_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Places] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspPlaces_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, PlaceName as Value FROM [dbo].[Places] a where a.PlaceName like N'%'+@key+'%'
            ORDER BY PlaceName ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspPrices_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Prices] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspPrices_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Prices] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspProject_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Project] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspProject_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT [ProjectCode] as Id, [Name] as Value FROM [dbo].[Project] a where a.[Name] like N'%'+@key+'%'
            ORDER BY [Name] ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspProvince_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Province] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspProvince_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT [ProvinceCode] as Id, [Name] as Value FROM [dbo].[Province] a where a.[Name] like N'%'+@key+'%'
            ORDER BY [Name] ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspRules_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Rules] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspRules_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Rules] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspSlots_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Slots] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspSlots_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Slots] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspStaffs_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Staffs] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspStaffs_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Staffs] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspStreet_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Street] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspStreet_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT StreetCode as Id, [Name] as Value FROM [dbo].[Street] a where a.[Name] like N'%'+@key+'%'
            ORDER BY [Name] ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspSubscriptions_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Subscriptions] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspSubscriptions_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Subscriptions] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspTeams_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Teams] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspTeams_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Teams] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspWard_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Ward] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspWard_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT WardCode as Id, [Name] as Value FROM [dbo].[Ward] a where a.[Name] like N'%'+@key+'%'
            ORDER BY [Name] ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspWithdraws_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Withdraws] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspWithdraws_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Withdraws] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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
    /****** Object:  StoredProcedure [dbo].[uspWorkers_Suggestion]    Script Date: 11/15/2020 11:48:40 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Workers] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspWorkers_Suggestion]
    (
	    @key NVARCHAR(100)
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
    
            SELECT Id, Title as Value FROM [dbo].[Workers] a where a.Title like N'%'+@key+'%'
            ORDER BY Title ASC;
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




