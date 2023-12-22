IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspPrices_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspPrices_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspPrices_selectAll]
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
	where CompanyId=@CompanyId and DataType='Prices';

	WITH TempResult AS 
	(
		SELECT
			p.Id,
			p.History,
			p.Chat,
			p.Title,
			p.Subtitle,
			p.[Description],
			p.AttachmentFileReName,
			p.AttachmentFileOriginalName,
			p.PriceClassA,
			p.PriceClassB,
			p.PriceClassC,
			p.PriceClassD,
			p.PriceClassE,
			p.PriceClassF,
			p.PriceClassM,
			p.PriceClassS
		FROM Prices AS p WITH(NOLOCK)
		WHERE (
				ISNULL(@key, '') = '' OR 
				dbo.ufnRemoveMark(TRIM(UPPER(p.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				)
				AND p.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
				AND p.IsDeleted = 0 
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
