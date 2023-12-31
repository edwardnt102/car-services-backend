IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspTeams_selectByPlace'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspTeams_selectByPlace AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspTeams_selectByPlace]
(
@PlaceId BIGINT
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
    
SELECT dbo.Teams.Id, dbo.Teams.History, dbo.Teams.Chat, dbo.Teams.CreatedBy, dbo.Teams.CreatedDate, dbo.Teams.ModifiedBy, dbo.Teams.ModifiedDate, dbo.Teams.IsDeleted, dbo.Teams.Title, dbo.Teams.Subtitle, dbo.Teams.Description, 
                  dbo.Teams.ColorCode, dbo.Teams.AttachmentFileOriginalName, dbo.Teams.AttachmentFileReName
FROM     dbo.TeamPlaces INNER JOIN
                  dbo.Teams ON dbo.TeamPlaces.TeamId = dbo.Teams.Id
        WHERE dbo.TeamPlaces.PlaceId = @PlaceId
        
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
