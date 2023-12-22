IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspSubscriptions_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspSubscriptions_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
    ALTER PROCEDURE [dbo].[uspSubscriptions_selectAll]
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
		where CompanyId=@CompanyId and DataType='Subscriptions';

	    WITH TempResult AS 
		(
			SELECT
			  s.[Id]
			  ,s.[Title]
			  ,s.[Subtitle]
			  ,s.[Description]
			  ,s.[AttachmentFileReName]
			  ,s.[AttachmentFileOriginalName]
			  ,s.[History]
			  ,s.[Chat]
			  ,s.[CarId]
			  ,c.LicensePlates
			  ,s.[PriceId]
			  ,p.Title AS PriceName
			  ,s.[NumberOfMonthsOfPurchase]
			  ,s.[DateOfPayment]
			  ,s.[Promotion]
			  ,s.[StartDate]
			  ,s.[EndDate]
			  ,s.[CompanyId]
			  ,s.[Amount]
			  ,s.[ArgentId]
			  ,u1.FullName AS ArgentName
			  ,s.[ClassId]
			  ,c1.Title AS ClassName
			  ,s.[PlaceId]
			  ,p1.Title AS PlaceName
			  ,s.[CustomerId]
			  ,u.FullName AS CustomerName
			FROM Subscriptions AS s WITH(NOLOCK)
            LEFT JOIN Cars AS c ON c.Id = s.CarId AND c.IsDeleted = 0 
            LEFT JOIN Prices AS p ON p.Id = s.PriceId AND p.IsDeleted = 0 
            LEFT JOIN Class AS c1 ON c1.Id = s.[ClassId] AND c1.IsDeleted = 0 
            LEFT JOIN Places AS p1 ON p1.Id = s.[PlaceId] AND p1.IsDeleted = 0 
            LEFT JOIN Customers AS c2 ON c2.Id = s.[CustomerId] AND c2.IsDeleted = 0 
            LEFT JOIN UserProfile AS u ON u.Id = c2.UserId AND u.Active = 1  AND u.IsDeleted = 0 
            LEFT JOIN Customers AS c3 ON c3.Id = s.[ArgentId] AND c3.IsDeleted = 0 
            LEFT JOIN UserProfile AS u1 ON u1.Id = c3.UserId AND u1.Active = 1  AND u1.IsDeleted = 0 
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(s.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND s.CompanyId  IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
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