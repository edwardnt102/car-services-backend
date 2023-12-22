IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspDistrict_insert'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspDistrict_insert AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspDistrict_insert]
(
@Id BIGINT OUT
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
    
        INSERT INTO [dbo].[District]
        (
        [DistrictCode]
        , [Name]
        , [ProvinceId]
        , [Prefix]
        )
	    VALUES 
        (
        @DistrictCode
        , @Name
        , @ProvinceId
        , @Prefix
        )

        

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

