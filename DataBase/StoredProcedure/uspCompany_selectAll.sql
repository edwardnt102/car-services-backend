IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspCompany_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspCompany_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspCompany_selectAll]
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

	WITH TempResult AS 
	(
		SELECT
			c.[Id]
		   ,c.[Title]
		   ,c.[Subtitle]
		   ,c.[Description]
		   ,c.[AttachmentFileReName]
		   ,c.[AttachmentFileOriginalName]
		   ,c.[History]
		   ,c.[Chat]
		   ,c.[Color]
		   ,c.Logo
		   ,c.Banner
		FROM Company AS c WITH(NOLOCK)
		WHERE (
				ISNULL(@key, '') = '' OR 
				dbo.ufnRemoveMark(TRIM(UPPER(c.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				)
				AND c.IsDeleted = 0 
	   )
    
	SELECT * FROM TempResult

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



