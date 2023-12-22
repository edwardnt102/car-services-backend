IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspGetInformationForCustomer'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspGetInformationForCustomer AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspGetInformationForCustomer]
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
    
            WITH TempResult AS 
			(
				SELECT
					c.Id,
					c.History,
					c.Chat,
					c.UserId,
					c.Subtitle,
					c.[Description],
					c.PlaceId,
					c.Title,
					c.CustomerId,
					c.IsAgency,
					c.RoomNumber,
					up.UserName,
					up.Email,
					up.PhoneNumber,
					up.[Address],
					up.ProvinceId,
					up.StreetId,
					up.DistrictId,
					up.WardId,
					up.Gender,
					up.DateOfBirth,
					up.Facebook,
					up.Zalo,
					up.GoogleId,
					up.PictureUrl,
					up.PhoneNumberOther,
					up.FullName AS CustomerName
				FROM Customers AS c WITH(NOLOCK)
				LEFT JOIN UserProfile AS up ON up.Id = c.UserId AND up.IsDeleted = 0  AND up.Active = 1
				WHERE up.UserName = @key AND c.IsDeleted = 0 
			)
    
			SELECT * FROM TempResult

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



