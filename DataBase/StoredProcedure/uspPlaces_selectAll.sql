IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspPlaces_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspPlaces_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspPlaces_selectAll]
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
	where CompanyId=@CompanyId and DataType='Places';

	WITH TempResult AS 
	(
		SELECT
			p.Id,
			p.History,
			p.Chat,
			p.Subtitle,
			p.[Description],
			p.AttachmentFileReName,
			p.AttachmentFileOriginalName,
			p.ProvinceId,
            p1.[Name] AS ProvinceName,
			p.DistrictId,
            d.[Name] AS DistrictName,
			p.WardId,
            w.[Name] AS WardName,
			s.[Name] AS StreetName,
			pr.[Name] AS ProjectName,
			p.[Address],
			p.Longitude,
			p.Latitude,
			p.RuleId,
			r.Title AS RuleName,
			p.PriceId,
			p.Title,
            pri.Title AS PriceName

		FROM Places AS p WITH(NOLOCK)
        LEFT JOIN Rules AS r ON r.Id = p.RuleId AND r.IsDeleted = 0 
        LEFT JOIN Prices AS pri ON pri.Id = p.PriceId AND pri.IsDeleted = 0 
        LEFT JOIN Province AS p1 ON p1.Id = p.ProvinceId
        LEFT JOIN District AS d ON d.Id = p.DistrictId
        LEFT JOIN Ward AS w ON w.WardCode = p.WardId and p.ProvinceId = w.ProvinceId and p.DistrictId = w.DistrictId
		LEFT JOIN Street AS s ON s.StreetCode = p.WardId and p.ProvinceId = s.ProvinceId and p.DistrictId = s.DistrictId
		LEFT JOIN Project AS pr ON pr.ProjectCode = p.WardId and p.ProvinceId = pr.ProvinceId and p.DistrictId = pr.DistrictId
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



