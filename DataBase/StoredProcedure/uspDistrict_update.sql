IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspDistrict_update'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspDistrict_update AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspDistrict_update]
(
@Id BIGINT
, @DistrictCode BIGINT
, @Name NVARCHAR(200)
, @ProvinceId BIGINT
, @Prefix NVARCHAR(MAX)
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
    
        UPDATE [dbo].[District]
        SET 
        [DistrictCode] = @DistrictCode
        , [Name] = @Name
        , [ProvinceId] = @ProvinceId
        , [Prefix] = @Prefix
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

