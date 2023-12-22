IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspJobColumns_Suggestion'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspJobColumns_Suggestion AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspJobColumns_Suggestion]
(
	@key NVARCHAR(100),
    @PlaceId BIGINT,
    @BasementId BIGINT,
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
        IF @PlaceId = 0 AND @BasementId = 0
        BEGIN
		    			DECLARE @companyc NVARCHAR(100)
		select TOP 1 @companyc = CompanyShareds from B2B 
		where CompanyId=@CompanyId and DataType='Columns';

		WITH TempResult AS 
		(
            SELECT Id, Title AS [Value] 
            FROM [dbo].[Columns]
            WHERE IsDeleted = 0 AND
                (
                    ISNULL(@key, '') = '' OR 
                    dbo.ufnRemoveMark(TRIM(UPPER(Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
                ) 
				AND (CompanyId = @CompanyId OR CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@companyc, ',')))
				)
            SELECT * FROM TempResult ORDER BY TempResult.[Value] ASC;
        END
            
        IF @PlaceId > 0 AND @BasementId = 0
        BEGIN
				    			DECLARE @companyp NVARCHAR(100)
		select TOP 1 @companyp = CompanyShareds from B2B 
		where CompanyId=@CompanyId and DataType='Places';
		WITH TempResult AS 
		(
            SELECT Id, Title AS [Value] 
            FROM [dbo].[Places]
            WHERE IsDeleted = 0 AND
                (
                    ISNULL(@key, '') = '' OR 
                    dbo.ufnRemoveMark(TRIM(UPPER(Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
                ) 
                AND Id = @PlaceId
				AND (CompanyId = @CompanyId OR CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@companyp, ',')))
         				)
            SELECT * FROM TempResult ORDER BY TempResult.[Value] ASC;
        END
            
        IF @PlaceId = 0 AND @BasementId > 0
        BEGIN
		DECLARE @companyb NVARCHAR(100)
		select TOP 1 @companyb = CompanyShareds from B2B 
		where CompanyId=@CompanyId and DataType='Basements';
		WITH TempResult AS 
		(
            SELECT Id, Title AS [Value] 
            FROM [dbo].[Basements]
            WHERE IsDeleted = 0 AND
                (
                    ISNULL(@key, '') = '' OR 
                    dbo.ufnRemoveMark(TRIM(UPPER(Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
                ) 
                AND Id = @BasementId
				AND (CompanyId = @CompanyId OR CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@companyb, ',')))
            				)
            SELECT * FROM TempResult ORDER BY TempResult.[Value] ASC;
        END

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