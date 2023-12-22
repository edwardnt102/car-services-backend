IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspWithdraws_Suggestion'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspWithdraws_Suggestion AS BEGIN SET NOCOUNT ON; END')
    END
GO
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
    
            SELECT Id, Title as [Value] FROM [dbo].[Withdraws] 
            WHERE IsDeleted = 0
				  AND (@key IS NULL OR (dbo.ufnRemoveMark(Title) LIKE N'%' + dbo.ufnRemoveMark(@key) + '%'))
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