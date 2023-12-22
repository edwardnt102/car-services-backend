IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspB2B_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspB2B_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspB2B_selectAll]
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
			   b.[Id]
			  ,b.[Title]
			  ,b.[Subtitle]
			  ,b.[Description]
			  ,b.[AttachmentFileReName]
			  ,b.[AttachmentFileOriginalName]
			  ,b.[History]
			  ,b.[Chat]
			  ,b.[CompanyId]
			  ,c.Title AS CompanyName
			  ,b.CompanyShareds
			  ,b.[DataType]
			FROM B2B AS b WITH(NOLOCK)
			LEFT JOIN Company AS c ON c.Id = b.[CompanyId] AND c.IsDeleted = 0
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(b.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
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



