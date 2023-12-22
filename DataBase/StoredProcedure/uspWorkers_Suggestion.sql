IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspWorkers_Suggestion'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspWorkers_Suggestion AS BEGIN SET NOCOUNT ON; END')
    END
GO
 ALTER PROCEDURE [dbo].[uspWorkers_Suggestion]
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
            SELECT c.Id, u.FullName AS [Value] 
			FROM [dbo].Workers AS c 
			LEFT JOIN UserProfile AS u ON u.Id = c.UserId AND ISNULL(u.IsDeleted,0) = 0 AND ISNULL(u.Active, 0) = 1
			LEFT JOIN UserRoles AS ur ON ur.UserId = c.UserId
			LEFT JOIN Roles AS r ON r.Id = ur.RoleId AND r.[Name] = 'Worker'
			WHERE c.IsDeleted = 0 
				  AND (
                        ISNULL(@key, '') = '' OR 
                        dbo.ufnRemoveMark(TRIM(UPPER(u.UserName))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@key))),'%')
                      )
			AND (c.CompanyId = @CompanyId OR c.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ',')))
		)
    
		SELECT * FROM TempResult ORDER BY TempResult.Value ASC

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