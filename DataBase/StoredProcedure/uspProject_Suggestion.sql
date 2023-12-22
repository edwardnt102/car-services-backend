IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspProject_Suggestion'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspProject_Suggestion AS BEGIN SET NOCOUNT ON; END')
    END
GO
	ALTER PROCEDURE [dbo].[uspProject_Suggestion]
(
	@key NVARCHAR(100),
	@provinceId BIGINT,
	@districtId BIGINT
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
    
        SELECT [ProjectCode] as Id, [Name] as [Value] 
        FROM [dbo].[Project] 
        WHERE (@key IS NULL OR (dbo.ufnRemoveMark([Name]) LIKE N'%' + dbo.ufnRemoveMark(@key) + '%')) 
              AND ProvinceId = @provinceId 
              AND DistrictId = @districtId
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

