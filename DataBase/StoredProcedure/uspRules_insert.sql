IF NOT EXISTS ( SELECT  1
            FROM    sys.procedures
            WHERE   name = 'uspRules_insert'
                    AND SCHEMA_NAME(schema_id) = 'dbo' )
    BEGIN 
        exec('CREATE PROCEDURE [dbo].uspRules_insert AS BEGIN SET NOCOUNT ON; END')
    END
GO
ALTER PROCEDURE [dbo].[uspRules_insert]
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
    
        INSERT INTO [dbo].[Rules]
        (
History
,Chat
,CreatedBy
,CreatedDate
,ModifiedBy
,ModifiedDate
,IsDeleted	
,Title	
,Subtitle	
,[Description]
,[Day]
,SalarySupervisor
,MinimumQuantity
,WeatherCoefficient
,ContingencyCoefficient
,SignUpBonus
,MoldBonus	
,CancellationOfSchedulePenalty	
,PileRegistration
,DayPayroll
,PlaceId 
,LaborWages 
,VehicleSizeFactorA	
,VehicleSizeFactorB	
,VehicleSizeFactorC	
,VehicleSizeFactorD	
,VehicleSizeFactorE
,VehicleSizeFactorF	
,VehicleSizeFactorM	
,VehicleSizeFactorS
,AttachmentFileOriginalName
,AttachmentFileReName
        )
	    VALUES 
        (
@History
,@Chat
,@CreatedBy
,@CreatedDate
,@ModifiedBy
,@ModifiedDate
,@IsDeleted	
,@Title	
,@Subtitle	
,@Description
,@Day
,@SalarySupervisor
,@MinimumQuantity
,@WeatherCoefficient
,@ContingencyCoefficient
,@SignUpBonus
,@MoldBonus	
,@CancellationOfSchedulePenalty	
,@PileRegistration
,@DayPayroll
,@PlaceId 
,@LaborWages 
,@VehicleSizeFactorA	
,@VehicleSizeFactorB	
,@VehicleSizeFactorC	
,@VehicleSizeFactorD	
,@VehicleSizeFactorE
,@VehicleSizeFactorF	
,@VehicleSizeFactorM	
,@VehicleSizeFactorS
,@AttachmentFileOriginalName
,@AttachmentFileReName
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