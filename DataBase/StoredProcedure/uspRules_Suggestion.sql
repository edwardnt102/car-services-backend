IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspRules_Suggestion'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspRules_Suggestion AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspRules_Suggestion]
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
		where CompanyId=@CompanyId and DataType='Rules';
				WITH TempResult AS 
		(
            SELECT Id, Title AS [Value] 
			FROM [dbo].Rules
			WHERE IsDeleted = 0 AND
                    (
                        ISNULL(@key, '') = '' OR 
                        dbo.ufnRemoveMark(TRIM(UPPER(Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
                    ) 
								AND (CompanyId = @CompanyId OR CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ',')))
		)
    
		SELECT * FROM TempResult ORDER BY TempResult.Value ASC

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