IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspStaffs_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspStaffs_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspStaffs_selectAll]
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
		where CompanyId=@CompanyId and DataType='Staffs';

	    WITH TempResult AS 
		(
			SELECT
				s.Id,
				s.History,
				s.Chat,
				s.UserId,
				s.Title,
				s.Subtitle,
				s.[Description],
				s.AttachmentFileReName,
				s.AttachmentFileOriginalName,
				s.PlaceId,
				p.Title AS PlaceName,
				s.CurrentAccommodation,
				s.CurrentAgency,
				s.CurrentJob,
				s.DateRange,
				s.IdentificationNumber,
				s.ProvincialLevel,
				u.Email,
				u.PhoneNumber,
				u.PhoneNumberOther,
				u.[Address],
			    u.DateOfBirth,
			    u.UserName,
				u.Gender,
				u.FullName,
				u.ProvinceId,
				u.DistrictId,
				u.StreetId,
				u.WardId,
				u.Facebook,
				u.Zalo,
				u.GoogleId,
				u.PictureUrl,
				s.PictureOriginalName,
				u.Active
			FROM Staffs AS s WITH(NOLOCK)
			LEFT JOIN UserProfile AS u ON u.Id = s.UserId AND u.IsDeleted = 0 
			LEFT JOIN Places AS p ON p.Id = s.PlaceId AND p.IsDeleted = 0 
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(u.UserName))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%') OR
					dbo.ufnRemoveMark(TRIM(UPPER(u.FullName))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%') OR
					dbo.ufnRemoveMark(TRIM(UPPER(s.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND s.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
				  AND s.IsDeleted = 0 
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