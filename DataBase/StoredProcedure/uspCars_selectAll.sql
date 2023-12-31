IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspCars_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspCars_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspCars_selectAll]
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

	    WITH TempResult AS 
		(
			SELECT
				c.Id,
				c.History,
				c.Chat,
				c.Title,
				c.Subtitle,
				c.[Description],
				c.AttachmentFileReName,
				c.AttachmentFileOriginalName,
				c.CustomerId,
				up.Fullname AS CustomerName,
				c.BrandId,
				b.Title AS BrandName,
				c.CarModelId,
				cm.Title AS CarModelName,
				c.YearOfManufacture,
				c.CarColor,
				c.LicensePlates,
				c.CarImage

			FROM Cars AS c WITH(NOLOCK)
			LEFT JOIN Customers AS cu ON cu.Id = c.CustomerId AND cu.IsDeleted = 0 
			LEFT JOIN UserProfile AS up ON up.Id = cu.UserId AND up.IsDeleted = 0 
			LEFT JOIN Brands AS b ON b.Id = c.BrandId AND b.IsDeleted = 0 
			LEFT JOIN CarModels AS cm ON cm.Id = c.CarModelId AND cm.IsDeleted = 0 
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(c.LicensePlates))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND c.CompanyId = @CompanyId
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



