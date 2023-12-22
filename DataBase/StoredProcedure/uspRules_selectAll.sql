IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspRules_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspRules_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspRules_selectAll]
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
		where CompanyId=@CompanyId and DataType='Rules';

	    WITH TempResult AS 
		(
			SELECT
				 r.[Id]
				,r.[History]
				,r.[Chat]
				,r.[Title]
				,r.[Subtitle]
				,r.[Description]
				,r.[Day]
				,r.[SalarySupervisor]
				,r.[MinimumQuantity]
				,r.[WeatherCoefficient]
				,r.[ContingencyCoefficient]
				,r.[SignUpBonus]
				,r.[MoldBonus]
				,r.[CancellationOfSchedulePenalty]
				,r.[PileRegistration]
				,r.[DayPayroll]
				,r.[PlaceId]
				,p.Title AS PlaceName
				,r.[LaborWages]
				,r.[VehicleSizeFactorA]
				,r.[VehicleSizeFactorB]
				,r.[VehicleSizeFactorC]
				,r.[VehicleSizeFactorD]
				,r.[VehicleSizeFactorE]
				,r.[VehicleSizeFactorF]
				,r.[VehicleSizeFactorM]
				,r.[VehicleSizeFactorS]
				,r.[AttachmentFileOriginalName]
				,r.[AttachmentFileReName]
			FROM Rules AS r WITH(NOLOCK)
			LEFT JOIN Places AS p ON p.Id = r.PlaceId AND p.IsDeleted = 0
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(r.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND r.CompanyId  IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
				  AND r.IsDeleted = 0 
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