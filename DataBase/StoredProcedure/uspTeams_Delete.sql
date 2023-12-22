IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspTeams_delete'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspTeams_delete AS BEGIN SET NOCOUNT ON; END')
    END
GO
 ALTER PROCEDURE [dbo].[uspTeams_delete]
    (
	    @key BIGINT
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
    
           DELETE FROM TeamLead WHERE TeamId = @key
		   DELETE FROM TeamPlaces WHERE TeamId = @key
		   DELETE FROM TeamWorker WHERE TeamId = @key
		   DELETE FROM TeamZone WHERE TeamId = @key

        END TRY
        BEGIN CATCH
            SELECT
                @strErrorMessage = ERROR_MESSAGE()
                        + ' Line:'
                        + CONVERT(VARCHAR(5), ERROR_LINE()),
                @intErrorSeverity = ERROR_SEVERITY(),
                @intErrorState = ERROR_STATE();
            RAISERROR(
                    @strErrorMessage,   -- Message text.
                    @intErrorSeverity,  -- Severity.
                    @intErrorState      -- State.
            );
        END CATCH;
    END