IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspTeams_selectAll'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspTeams_selectAll AS BEGIN SET NOCOUNT ON; END')
    END
GO
  ALTER PROCEDURE [dbo].[uspTeams_selectAll]
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
		where CompanyId=@CompanyId and DataType='Teams';

	    WITH TempResult AS 
		(
			SELECT
				 t.Id
				,t.History
				,t.Chat
				,t.Title
				,t.Subtitle
				,t.[Description]
				,t.ColorCode
				,t.AttachmentFileOriginalName
				,t.AttachmentFileReName

			FROM Teams AS t WITH(NOLOCK)
			WHERE (
					ISNULL(@key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(t.Title))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
				  )
				  AND t.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ','))
				  AND t.IsDeleted = 0 
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