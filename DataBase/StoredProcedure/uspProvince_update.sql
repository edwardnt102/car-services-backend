IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspProvince_update'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspProvince_update AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspProvince_update]
(
@Id BIGINT
, @ProvinceCode BIGINT
, @Name NVARCHAR(200)
, @Code NVARCHAR(200)
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
    
        UPDATE [dbo].[Province]
        SET 
        [ProvinceCode] = @ProvinceCode
        , [Name] = @Name
        , [Code] = @Code
        WHERE 
        [Id] = @Id
        
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



