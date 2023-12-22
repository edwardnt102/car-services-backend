
    GO
    /****** Object:  StoredProcedure [dbo].[uspDistrict_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[District] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspDistrict_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[District]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[District] a where a.Name like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspAreas_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Areas] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspAreas_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Areas]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Areas] a where a.History like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspBasements_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Basements] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspBasements_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Basements]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Basements] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspBrands_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Brands] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspBrands_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Brands]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Brands] a where a.Subtitle like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspCarModels_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[CarModels] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspCarModels_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[CarModels]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[CarModels] a where a.Subtitle like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspCars_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Cars] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspCars_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Cars]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Cars] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspClass_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Class] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspClass_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Class]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Class] a where a.Subtitle like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspColumns_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Columns] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspColumns_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Columns]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Columns] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspCustomers_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Customers] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspCustomers_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Customers]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Customers] a where a.Subtitle like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspJobs_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Jobs] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspJobs_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Jobs]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Jobs] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspPlaces_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Places] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspPlaces_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Places]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Places] a where a.Address like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspPrices_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Prices] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspPrices_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Prices]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Prices] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspProject_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Project] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspProject_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Project]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Project] a where a.Name like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspProvince_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Province] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspProvince_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Province]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Province] a where a.Name like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspRules_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Rules] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspRules_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Rules]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Rules] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspSlots_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Slots] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspSlots_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Slots]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Slots] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspStaffs_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Staffs] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspStaffs_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Staffs]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Staffs] a where a.AttachmentFile like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspStreet_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Street] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspStreet_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Street]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Street] a where a.Name like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspSubscriptions_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Subscriptions] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspSubscriptions_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Subscriptions]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Subscriptions] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspTeams_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Teams] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspTeams_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Teams]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Teams] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspWard_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Ward] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspWard_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Ward]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Ward] a where a.Name like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspWithdraws_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Withdraws] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspWithdraws_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Withdraws]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Withdraws] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
    /****** Object:  StoredProcedure [dbo].[uspWorkers_selectAll]    Script Date: 11/15/2020 11:45:28 PM ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO




 
    -- =================================================================
    -- Author: MSSQL-Dapper Generator
    -- Description:	Select All Procedure for the table [dbo].[Workers] 
    -- =================================================================

    ALTER PROCEDURE [dbo].[uspWorkers_selectAll]
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
		    IF @key = ''
		    BEGIN
			    SELECT * FROM [dbo].[Workers]
		    END
		    ELSE
		    BEGIN
			    SELECT * FROM [dbo].[Workers] a where a.Title like N'%'+@key+'%'
		    END
        
        
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
