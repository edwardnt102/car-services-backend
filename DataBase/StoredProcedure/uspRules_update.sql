IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspRules_update'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspRules_update AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspRules_update]
(
@Id BIGINT OUT
,@History	nvarchar(256)
,@Chat	nvarchar(256)
,@CreatedBy	nvarchar(50)
,@CreatedDate	datetime
,@ModifiedBy	nvarchar(50)
,@ModifiedDate	datetime
,@IsDeleted	bit
,@Title	nvarchar(200)
,@Subtitle	nvarchar(200)
,@Description	nvarchar(256)
,@Day	datetime
,@SalarySupervisor	decimal(18, 2)
,@MinimumQuantity	decimal(18, 2)
,@WeatherCoefficient	decimal(18, 2)
,@ContingencyCoefficient	decimal(18, 2)
,@SignUpBonus	nvarchar(MAX)
,@MoldBonus	nvarchar(MAX)
,@CancellationOfSchedulePenalty	nvarchar(MAX)
,@PileRegistration	decimal(18, 2)
,@DayPayroll	nvarchar(MAX)
,@PlaceId bigint
,@LaborWages decimal(18, 2)
,@VehicleSizeFactorA	decimal(18, 2)
,@VehicleSizeFactorB	decimal(18, 2)
,@VehicleSizeFactorC	decimal(18, 2)
,@VehicleSizeFactorD	decimal(18, 2)
,@VehicleSizeFactorE	decimal(18, 2)
,@VehicleSizeFactorF	decimal(18, 2)
,@VehicleSizeFactorM	decimal(18, 2)
,@VehicleSizeFactorS	decimal(18, 2)
,@AttachmentFileOriginalName nvarchar(MAX)
,@AttachmentFileReName nvarchar(MAX)
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
    
        UPDATE [dbo].[Rules]
        SET 
History = @History
,Chat = @Chat
,CreatedBy = @CreatedBy
,CreatedDate = @CreatedDate
,ModifiedBy = @ModifiedBy
,ModifiedDate = @ModifiedDate
,IsDeleted	 = @IsDeleted
,Title	 = @Title
,Subtitle	 = @Subtitle
,[Description] = @Description
,[Day] = @Day
,SalarySupervisor = @SalarySupervisor
,MinimumQuantity = @MinimumQuantity
,WeatherCoefficient = @WeatherCoefficient
,ContingencyCoefficient = @ContingencyCoefficient
,SignUpBonus = @SignUpBonus
,MoldBonus	 = @MoldBonus
,CancellationOfSchedulePenalty = @CancellationOfSchedulePenalty	
,PileRegistration = @PileRegistration
,DayPayroll = @DayPayroll
,PlaceId  = @PlaceId
,LaborWages  = @LaborWages
,VehicleSizeFactorA	 = @VehicleSizeFactorA
,VehicleSizeFactorB	 = @VehicleSizeFactorB
,VehicleSizeFactorC	 = @VehicleSizeFactorC
,VehicleSizeFactorD	 = @VehicleSizeFactorD
,VehicleSizeFactorE = @VehicleSizeFactorE
,VehicleSizeFactorF	 = @VehicleSizeFactorF
,VehicleSizeFactorM	 = @VehicleSizeFactorM
,VehicleSizeFactorS = @VehicleSizeFactorS
,AttachmentFileOriginalName = @AttachmentFileOriginalName
,AttachmentFileReName = @AttachmentFileReName
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