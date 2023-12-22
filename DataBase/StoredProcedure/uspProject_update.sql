IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspProject_update'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspProject_update AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspProject_update]
(
@Id BIGINT
, @ProjectCode BIGINT
, @Name NVARCHAR(200)
, @ProvinceId BIGINT
, @DistrictId BIGINT
, @Lat FLOAT
, @Lng FLOAT
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
    
        UPDATE [dbo].[Project]
        SET 
        [ProjectCode] = @ProjectCode
        , [Name] = @Name
        , [ProvinceId] = @ProvinceId
        , [DistrictId] = @DistrictId
        , [Lat] = @Lat
        , [Lng] = @Lng
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

