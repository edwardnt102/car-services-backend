IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspColumns_Delete'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspColumns_Delete AS BEGIN SET NOCOUNT ON; END')
    END
GO
    ALTER PROCEDURE [dbo].[uspColumns_Delete]
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
    
           UPDATE dbo.Columns SET dbo.Columns.IsDeleted = 1 WHERE dbo.Columns.[BasementId] = @key

        END TRY
        BEGIN CATCH
            SELECT
                @strErrorMessage = ERROR_MESSAGE()
                        + ' Line:'
                        + CONVERT(VARCHAR(5), ERROR_LINE()),
                @intErrorSeverity = ERROR_SEVERITY(),
                @intErrorState = ERROR_STATE();
            RAISERROR(
                    @strErrorMessage,   -- Message text.
                    @intErrorSeverity,  -- Severity.
                    @intErrorState      -- State.
            );
        END CATCH;
    END



