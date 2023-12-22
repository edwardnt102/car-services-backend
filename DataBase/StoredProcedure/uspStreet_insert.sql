IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspStreet_insert'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspStreet_insert AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspStreet_insert]
(
@Id BIGINT OUT
, @StreetCode BIGINT
, @Name NVARCHAR(100)
, @Prefix NVARCHAR(20)
, @ProvinceId BIGINT
, @DistrictId BIGINT
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
    
        INSERT INTO [dbo].[Street]
        (
        [StreetCode]
        , [Name]
        , [Prefix]
        , [ProvinceId]
        , [DistrictId]
        )
	    VALUES 
        (
        @StreetCode
        , @Name
        , @Prefix
        , @ProvinceId
        , @DistrictId
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