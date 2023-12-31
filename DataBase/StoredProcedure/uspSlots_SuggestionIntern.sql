IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspSlots_SuggestionIntern'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspSlots_SuggestionIntern AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspSlots_SuggestionIntern]
    (
	    @key NVARCHAR(100),
		@WorkerType INT,
		@TeamId BIGINT,
		@day date,
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
		where CompanyId=@CompanyId and DataType='Slots';
						WITH TempResult AS 
		(
           Select s.id as Id, s.title as Value
                            From dbo.slots s 
                            Join dbo.workers w on s.WorkerId = w.id
                            Join dbo.TeamWorker tw on w.id = tw.workerId 
                            Where s.Title like N'%'+@key+'%'
                            and w.workertype = @WorkerType 
                            and tw.teamId = @TeamId
                            and s.day = @day
                            and w.IsDeleted = 0
                            and s.IsDeleted = 0
				AND (s.CompanyId = @CompanyId OR s.CompanyId IN (SELECT CAST(value AS BIGINT) FROM STRING_SPLIT(@company, ',')))
		)
       SELECT * FROM TempResult ORDER BY TempResult.[Value] ASC;

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
