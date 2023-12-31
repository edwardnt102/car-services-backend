IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspCarModels_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspCarModels_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspCarModels_selectAll]
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
				cm.Id,
				cm.History,
				cm.Chat,
				cm.Subtitle,
				cm.[Description],
				cm.BrandId,
				b.Title AS BrandName,
				cm.ClassId,
				c.Title AS ClassName,
				cm.Note,
				cm.Long,
				cm.[High],
				cm.Heavy,
				cm.ReferencePrice,
				cm.AttachmentFileReName,
				cm.AttachmentFileOriginalName,
				cm.Title,
				cm.Width,
				cm.ModelImage

			FROM CarModels AS cm WITH(NOLOCK)
			LEFT JOIN Brands AS b ON b.Id = cm.BrandId AND b.IsDeleted = 0 
			LEFT JOIN Class AS c ON c.Id = cm.ClassId AND c.IsDeleted = 0 
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(cm.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND cm.IsDeleted = 0 
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



