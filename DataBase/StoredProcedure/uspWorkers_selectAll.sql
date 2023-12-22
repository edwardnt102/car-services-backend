IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspWorkers_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspWorkers_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspWorkers_selectAll]
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
		where CompanyId=@CompanyId and DataType='Workers';

	    WITH TempResult AS 
		(
			SELECT
				w.Id,
				w.History,
				w.Chat,				
				w.UserId,
				w.Title,
				w.Subtitle,
				w.[Description],
				w.AttachmentFileReName,
				w.AttachmentFileOriginalName,
				w.IdentificationNumber,
				w.DateRange,
				w.ProvincialLevel,
				w.CurrentJob,
				w.CurrentAgency,
				w.CurrentAccommodation,
				w.Official,
				w.MoneyInWallet,
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
				w.PictureOriginalName,
				u.Active
			FROM Workers AS w WITH(NOLOCK)
			LEFT JOIN UserProfile AS u ON u.Id = w.UserId AND u.IsDeleted = 0 
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(u.UserName))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%') OR
					dbo.ufnRemoveMark(TRIM(UPPER(u.FullName))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%') OR
					dbo.ufnRemoveMark(TRIM(UPPER(w.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND w.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
				  AND w.IsDeleted = 0 
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