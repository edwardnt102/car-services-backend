IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspProvince_Suggestion'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspProvince_Suggestion AS BEGIN SET NOCOUNT ON; END')
    END
GO
    ALTER PROCEDURE [dbo].[uspProvince_Suggestion]
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
    
            SELECT [ProvinceCode] as Id, [Name] as [Value] 
            FROM [dbo].[Province] 
            WHERE @key IS NULL OR 
                  dbo.ufnRemoveMark(TRIM(UPPER([Name]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
            ORDER BY [Name] ASC;

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



