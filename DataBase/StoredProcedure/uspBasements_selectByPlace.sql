IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspBasements_selectByPlace'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspBasements_selectByPlace AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspBasements_selectByPlace]
(
@PlaceID BIGINT
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
        WHERE [PlaceId] = @PlaceID AND IsDeleted = 0
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END
