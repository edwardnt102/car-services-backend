IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspZone_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspZone_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspZone_selectAll]
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
		where CompanyId=@CompanyId and DataType='Zones';

	    WITH TempResult AS 
		(
			SELECT
				z.[Id]
			  ,z.[Title]
			  ,z.[Subtitle]
			  ,z.[Description]
			  ,z.[History]
			  ,z.[Chat]
			  ,z.[PlaceId]
              ,p.[Title] AS PlaceName
			  ,z.[BasementId]
              ,b.[Title] AS BasementName
			  ,z.[MapOfTunnelsInTheArea]
			  ,z.[ColorCode]
			  ,z.[AttachmentFileOriginalName]
			  ,z.[AttachmentFileReName]

			FROM Zones AS z WITH(NOLOCK)
			LEFT JOIN Places AS p ON p.Id = z.PlaceId AND p.IsDeleted = 0 
			LEFT JOIN Basements AS b ON b.Id = z.BasementId AND b.IsDeleted = 0 
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(z.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND z.CompanyId  IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
				  AND z.IsDeleted = 0 
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