IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspCar_selectByZone'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspCar_selectByZone AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspCar_selectByZone]
(
@ZoneID bigint
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
    
SELECT DISTINCT dbo.ZoneColumn.ZoneId, dbo.[Cars].Id, dbo.[Cars].[LicensePlates]
FROM     dbo.[Columns] 
            INNER JOIN dbo.ZoneColumn ON dbo.[Columns].Id = dbo.ZoneColumn.ColumnId
			INNER JOIN dbo.[Jobs] ON dbo.[Columns].Id = dbo.[Jobs].ColumnId and dbo.[Jobs].IsDeleted = 0
			INNER JOIN dbo.[Cars] ON dbo.[Jobs].[CarId] = dbo.[Cars].Id and dbo.[Cars].IsDeleted = 0
        WHERE dbo.ZoneColumn.ZoneId = @ZoneID AND dbo.[Columns].IsDeleted = 0 
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END

