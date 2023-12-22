IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspWithdraws_insert'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspWithdraws_insert AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspWithdraws_insert]
(
@Id BIGINT OUT
, @History NVARCHAR(256)
, @Chat NVARCHAR(256)
, @CreatedBy NVARCHAR(50)
, @CreatedDate DATETIME
, @ModifiedBy NVARCHAR(50)
, @ModifiedDate DATETIME
, @IsDeleted BIT
, @Title NVARCHAR(200)
, @Subtitle NVARCHAR(200)
, @Description NVARCHAR(256)
, @AttachmentFile NVARCHAR(MAX)
, @AmountOfWithdrawal NVARCHAR(13)
, @AmountBeforeWithdrawal NVARCHAR(13)
, @AmountAfterWithdrawal NVARCHAR(13)
, @TimeToWithdraw DATETIME
, @ReceivingTime DATETIME
, @WorkerId BIGINT
, @StaffId BIGINT
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
    
        INSERT INTO [dbo].[Withdraws]
        (
        [History]
        , [Chat]
        , [CreatedBy]
        , [CreatedDate]
        , [ModifiedBy]
        , [ModifiedDate]
        , [IsDeleted]
        , [Title]
        , [Subtitle]
        , [Description]
       -- , [AttachmentFile]
        , [AmountOfWithdrawal]
        , [AmountBeforeWithdrawal]
        , [AmountAfterWithdrawal]
        , [TimeToWithdraw]
        , [ReceivingTime]
        , [WorkerId]
        , [StaffId]
        )
	    VALUES 
        (
        @History
        , @Chat
        , @CreatedBy
        , @CreatedDate
        , @ModifiedBy
        , @ModifiedDate
        , @IsDeleted
        , @Title
        , @Subtitle
        , @Description
       -- , @AttachmentFile
        , @AmountOfWithdrawal
        , @AmountBeforeWithdrawal
        , @AmountAfterWithdrawal
        , @TimeToWithdraw
        , @ReceivingTime
        , @WorkerId
        , @StaffId
        )

        

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