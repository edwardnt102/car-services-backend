IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspProvince_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspProvince_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
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



