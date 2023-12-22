IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspJobs_selectByPlaceDate'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspJobs_selectByPlaceDate AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspJobs_selectByPlaceDate]
(
@PlaceID bigint,
@Day date
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
    
        SELECT dbo.Jobs.Id, dbo.Jobs.History, dbo.Jobs.Chat, dbo.Jobs.CreatedBy, dbo.Jobs.CreatedDate, dbo.Jobs.ModifiedBy, dbo.Jobs.ModifiedDate, dbo.Jobs.IsDeleted, dbo.Jobs.Title, dbo.Jobs.Subtitle, dbo.Jobs.Description, 
                  dbo.Jobs.BookJobDate, dbo.Jobs.PhotoScheduling, dbo.Jobs.ColumnId, dbo.Jobs.SubscriptionId, dbo.Jobs.CarId, dbo.Jobs.SlotInCharge, dbo.Jobs.SlotSupport, dbo.Jobs.ChemicalUsed, dbo.Jobs.MaterialsUsed, 
                  dbo.Jobs.MainPhotoBeforeWiping, dbo.Jobs.MainPhotoAfterWiping, dbo.Jobs.TheSecondaryPhotoBeforeWiping, dbo.Jobs.SubPhotoAfterWiping, dbo.Jobs.StaffAssessment, dbo.Jobs.StaffId, dbo.Jobs.StaffScore, 
                  dbo.Jobs.TeamLeadAssessment, dbo.Jobs.TeamLeadScore, dbo.Jobs.CustomerAssessment, dbo.Jobs.CustomerScore, dbo.Jobs.StartingTime, dbo.Jobs.EndTime, dbo.Jobs.WorkingStep, dbo.Jobs.AttachmentFileOriginalName, 
                  dbo.Jobs.AttachmentFileReName, dbo.Jobs.JobStatus
FROM     dbo.Jobs 
            INNER JOIN dbo.Slots ON dbo.Jobs.SlotInCharge = dbo.Slots.Id AND dbo.Slots.IsDeleted = 0 
        WHERE dbo.Slots.[PlaceId] = @PlaceID AND dbo.Slots.[Day] >= @Day AND dbo.Slots.[Day] < dateadd(day,1,@Day) AND dbo.Jobs.IsDeleted = 0 
        
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

