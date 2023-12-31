IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspCustomers_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspCustomers_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspCustomers_selectAll]
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
		where CompanyId=@CompanyId and DataType='Customers';

	    WITH TempResult AS 
		(
			SELECT
				c.Id,
				c.History,
				c.Chat,
				c.UserId,
				c.Subtitle,
				c.[Description],
				c.AttachmentFileReName,
				c.AttachmentFileOriginalName,
				c.PlaceId,
				p.Title AS PlaceName,
				c.Title,
				c.CustomerId,
				u1.FullName AS CustomerName,
				c.IsAgency,
				c.RoomNumber,
				u.Email,
				u.PhoneNumber,
				u.PhoneNumberOther,
				u.[Address],
			    u.DateOfBirth,
			    u.UserName,
				u.Gender,
				u.FullName,
				u.ProvinceId,
				p1.[Name] AS ProvinceName,
				u.DistrictId,
				d.[Name] AS DistrictName,
				u.StreetId,
				u.WardId,
				w.[Name] AS WardName,
				u.Facebook,
				u.Zalo,
				u.GoogleId,
				u.PictureUrl,
				c.PictureOriginalName,
				u.Active
			FROM Customers AS c WITH(NOLOCK)
			LEFT JOIN UserProfile AS u ON u.Id = c.UserId AND u.IsDeleted = 0 
			LEFT JOIN Customers AS c1 ON c1.Id = c.CustomerId AND c1.IsDeleted = 0 
			LEFT JOIN UserProfile AS u1 ON u1.Id = c1.UserId AND u1.IsDeleted = 0 
			LEFT JOIN Places AS p ON p.Id = c.PlaceId AND p.IsDeleted = 0 
			LEFT JOIN Province AS p1 ON p1.Id = u.ProvinceId
			LEFT JOIN District AS d ON d.Id = u.DistrictId
			LEFT JOIN Ward AS w ON w.Id = u.WardId
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(u.UserName))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%') OR
					dbo.ufnRemoveMark(TRIM(UPPER(u.FullName))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%') OR
					dbo.ufnRemoveMark(TRIM(UPPER(c.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND c.CompanyId  IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
				  AND c.IsDeleted = 0 
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



