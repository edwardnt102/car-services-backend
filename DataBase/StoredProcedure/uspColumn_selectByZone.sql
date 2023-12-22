IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspColumn_selectByZone'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspColumn_selectByZone AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspColumn_selectByZone]
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
    
SELECT dbo.[Columns].Id, dbo.[Columns].History, dbo.[Columns].Chat, dbo.[Columns].CreatedBy, dbo.[Columns].CreatedDate, dbo.[Columns].ModifiedBy, dbo.[Columns].IsDeleted, dbo.[Columns].ModifiedDate, dbo.[Columns].Title, dbo.[Columns].Subtitle, 
                  dbo.[Columns].Description, dbo.[Columns].BasementId, dbo.[Columns].AttachmentFileOriginalName, dbo.[Columns].AttachmentFileReName
FROM     dbo.[Columns] 
            INNER JOIN dbo.ZoneColumn ON dbo.[Columns].Id = dbo.ZoneColumn.ColumnId
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

