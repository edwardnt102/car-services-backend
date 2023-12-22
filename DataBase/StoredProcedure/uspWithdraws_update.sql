IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspWithdraws_update'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspWithdraws_update AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspWithdraws_update]
(
@Id BIGINT
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
    
        UPDATE [dbo].[Withdraws]
        SET 
        [History] = @History
        , [Chat] = @Chat
        , [CreatedBy] = @CreatedBy
        , [CreatedDate] = @CreatedDate
        , [ModifiedBy] = @ModifiedBy
        , [ModifiedDate] = @ModifiedDate
        , [IsDeleted] = @IsDeleted
        , [Title] = @Title
        , [Subtitle] = @Subtitle
        , [Description] = @Description
       -- , [AttachmentFile] = @AttachmentFile
        , [AmountOfWithdrawal] = @AmountOfWithdrawal
        , [AmountBeforeWithdrawal] = @AmountBeforeWithdrawal
        , [AmountAfterWithdrawal] = @AmountAfterWithdrawal
        , [TimeToWithdraw] = @TimeToWithdraw
        , [ReceivingTime] = @ReceivingTime
        , [WorkerId] = @WorkerId
        , [StaffId] = @StaffId
        WHERE 
        [Id] = @Id
        
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