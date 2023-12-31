IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspColumn_BulkInsert'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspColumn_BulkInsert AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspColumn_BulkInsert]
(
	@key [dbo].[udtColumn] READONLY
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
    
        INSERT INTO [dbo].[Columns]
			([History]
			,[Chat]
			,[CreatedBy]
			,[CreatedDate]
			,[ModifiedBy]
			,[ModifiedDate]
			,[IsDeleted]
			,[Title]
			,[Subtitle]
			,[Description]
			,[BasementId]
			,[AttachmentFileOriginalName]
			,[AttachmentFileReName])
        SELECT 
			 History
			,Chat
			,CreatedBy
			,GETDATE()
			,ModifiedBy
			,GETDATE()
			,0
			,Title
			,Subtitle
			,[Description]
			,BasementId
			,AttachmentFileOriginalName
			,AttachmentFileReName
	    FROM @key

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

