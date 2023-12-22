IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspColumns_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspColumns_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspColumns_selectAll]
(
	@key NVARCHAR(100),
	@CompanyId BIGINT
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
			c.Id
			,c.History
			,c.Chat
			,c.Title
			,c.Subtitle
			,c.[Description]
			,c.BasementId
			,b.Title AS BasementName
			,c.AttachmentFileOriginalName
			,c.AttachmentFileReName
			,p.Title AS PlaceName
		FROM [Columns] AS c WITH(NOLOCK)
		LEFT JOIN Basements AS b ON b.Id = c.BasementId AND b.IsDeleted = 0 
		LEFT JOIN Places AS p ON p.Id = b.PlaceId AND p.IsDeleted = 0 
		WHERE (
				ISNULL(@key, '') = '' OR 
				dbo.ufnRemoveMark(TRIM(UPPER(c.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				)
				AND c.CompanyId = @CompanyId
				AND c.IsDeleted = 0 
	)
    
	SELECT * FROM TempResult ORDER BY BasementName ASC

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



