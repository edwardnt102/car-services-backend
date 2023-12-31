IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspCar_SuggestionByPlace'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspCar_SuggestionByPlace AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspCar_SuggestionByPlace]
(
	@key NVARCHAR(100),
	@PlaceId BIGINT,
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
		where CompanyId=@CompanyId and DataType='Cars';
				WITH TempResult AS 
		(
		SELECT dbo.Cars.Id AS Id , dbo.Cars.LicensePlates AS [Value] 
		FROM dbo.Cars INNER JOIN dbo.Customers 
		ON dbo.Cars.CustomerId = dbo.Customers.Id 
		AND dbo.Customers.PlaceId = @PlaceId 
		AND dbo.Customers.IsDeleted = 0 
		WHERE ( ISNULL(@key, '') = '' OR dbo.ufnRemoveMark(TRIM(UPPER(dbo.Cars.LicensePlates))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%') ) 
		AND dbo.Cars.IsDeleted = 0
				AND (dbo.Cars.CompanyId = @CompanyId OR dbo.Cars.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ',')))
		)
       SELECT * FROM TempResult ORDER BY TempResult.[Value] ASC;

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

