IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspDataType_Suggestion'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspDataType_Suggestion AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspDataType_Suggestion]
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
    
        SELECT Title AS Code , Title AS [Value] 
		FROM [dbo].DataType
		WHERE (
                  ISNULL(@key, '') = '' OR 
                  dbo.ufnRemoveMark(TRIM(UPPER(Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
               ) 
		ORDER BY Title ASC;

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



