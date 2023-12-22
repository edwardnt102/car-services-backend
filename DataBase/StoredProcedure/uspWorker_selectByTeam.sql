IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspWorker_selectByTeam'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspWorker_selectByTeam AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspWorker_selectByTeam]
(
@TeamID bigint
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
    
SELECT dbo.Workers.Id, dbo.Workers.History, dbo.Workers.Chat, dbo.Workers.CreatedDate, dbo.Workers.CreatedBy, dbo.Workers.ModifiedBy, dbo.Workers.ModifiedDate, dbo.Workers.IsDeleted, dbo.Workers.UserId, 
                  dbo.Workers.Title, dbo.Workers.Subtitle, dbo.Workers.Description, dbo.Workers.IdentificationNumber, dbo.Workers.DateRange, dbo.Workers.ProvincialLevel, dbo.Workers.CurrentJob, dbo.Workers.CurrentAgency, 
                  dbo.Workers.CurrentAccommodation, dbo.Workers.Official, dbo.Workers.MoneyInWallet, dbo.Workers.AttachmentFileOriginalName, dbo.Workers.AttachmentFileReName, dbo.Workers.PictureOriginalName, dbo.TeamWorker.TeamId
FROM     dbo.TeamWorker 
        INNER JOIN dbo.Workers ON dbo.TeamWorker.WorkerId = dbo.Workers.Id AND dbo.Workers.IsDeleted = 0 
        WHERE dbo.TeamWorker.TeamId = @TeamID
        
    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END