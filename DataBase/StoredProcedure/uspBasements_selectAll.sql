IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspBasements_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspBasements_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspBasements_selectAll]
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
	
		DECLARE @company NVARCHAR(100)
		select TOP 1 @company = CompanyShareds from B2B 
		where CompanyId=@CompanyId and DataType='Basements';

	    WITH TempResult AS 
		(
			SELECT
				b.Id,
				b.History,
				b.Chat,
				b.Title,
				b.Subtitle,
				b.[Description],
				b.AttachmentFileReName,
				b.AttachmentFileOriginalName,
				b.PlaceId,
				p.Title AS PlaceName,
				b.DiagramAttachmentReName,
				b.DiagramAttachmentOriginalName

			FROM Basements AS b WITH(NOLOCK)
			LEFT JOIN Places AS p ON p.Id = b.PlaceId AND p.IsDeleted = 0 
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(b.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND b.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
				  AND b.IsDeleted = 0 
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



