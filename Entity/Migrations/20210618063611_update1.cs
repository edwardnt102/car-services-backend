using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entity.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basements",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PlaceId = table.Column<long>(nullable: true),
                    DiagramAttachmentReName = table.Column<string>(maxLength: 256, nullable: true),
                    DiagramAttachmentOriginalName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LinkRefer = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BrandId = table.Column<long>(nullable: true),
                    ClassId = table.Column<long>(nullable: true),
                    Note = table.Column<string>(maxLength: 256, nullable: true),
                    Long = table.Column<string>(maxLength: 10, nullable: true),
                    Width = table.Column<string>(maxLength: 10, nullable: true),
                    High = table.Column<string>(maxLength: 10, nullable: true),
                    Heavy = table.Column<string>(maxLength: 10, nullable: true),
                    ReferencePrice = table.Column<string>(maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true),
                    BrandId = table.Column<long>(nullable: true),
                    CarModelId = table.Column<long>(nullable: true),
                    YearOfManufacture = table.Column<long>(nullable: true),
                    CarColor = table.Column<string>(maxLength: 15, nullable: true),
                    LicensePlates = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Columns",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BasementId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(maxLength: 200, nullable: true),
                    PlaceId = table.Column<long>(nullable: true),
                    RoomNumber = table.Column<string>(maxLength: 150, nullable: true),
                    IsAgency = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true),
                    PictureOriginalName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictCode = table.Column<long>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BookJobDate = table.Column<DateTime>(nullable: true),
                    PhotoScheduling = table.Column<string>(nullable: true),
                    ColumnId = table.Column<long>(nullable: true),
                    SubscriptionId = table.Column<long>(nullable: true),
                    CarId = table.Column<long>(nullable: true),
                    SlotInCharge = table.Column<long>(nullable: true),
                    SlotSupport = table.Column<long>(nullable: true),
                    StartingTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    WorkingStep = table.Column<string>(nullable: true),
                    ChemicalUsed = table.Column<string>(maxLength: 256, nullable: true),
                    MaterialsUsed = table.Column<string>(maxLength: 256, nullable: true),
                    MainPhotoBeforeWiping = table.Column<string>(nullable: true),
                    TheSecondaryPhotoBeforeWiping = table.Column<string>(nullable: true),
                    MainPhotoAfterWiping = table.Column<string>(nullable: true),
                    SubPhotoAfterWiping = table.Column<string>(nullable: true),
                    StaffId = table.Column<long>(nullable: true),
                    StaffAssessment = table.Column<string>(maxLength: 256, nullable: true),
                    StaffScore = table.Column<int>(nullable: false),
                    TeamLeadAssessment = table.Column<string>(maxLength: 256, nullable: true),
                    TeamLeadScore = table.Column<int>(nullable: false),
                    CustomerAssessment = table.Column<string>(maxLength: 256, nullable: true),
                    CustomerScore = table.Column<int>(nullable: false),
                    JobStatus = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    WardId = table.Column<long>(nullable: true),
                    RuleId = table.Column<long>(nullable: true),
                    PriceId = table.Column<long>(nullable: true),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    Longitude = table.Column<string>(maxLength: 256, nullable: true),
                    Latitude = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PriceClassA = table.Column<decimal>(nullable: true),
                    PriceClassB = table.Column<decimal>(nullable: true),
                    PriceClassC = table.Column<decimal>(nullable: true),
                    PriceClassD = table.Column<decimal>(nullable: true),
                    PriceClassE = table.Column<decimal>(nullable: true),
                    PriceClassF = table.Column<decimal>(nullable: true),
                    PriceClassM = table.Column<decimal>(nullable: true),
                    PriceClassS = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<long>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    Lat = table.Column<double>(nullable: true),
                    Lng = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceCode = table.Column<long>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Day = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlaceId = table.Column<long>(nullable: true),
                    LaborWages = table.Column<decimal>(nullable: true),
                    SalarySupervisor = table.Column<decimal>(nullable: true),
                    MinimumQuantity = table.Column<decimal>(nullable: true),
                    VehicleSizeFactorA = table.Column<decimal>(nullable: true),
                    VehicleSizeFactorB = table.Column<decimal>(nullable: true),
                    VehicleSizeFactorC = table.Column<decimal>(nullable: true),
                    VehicleSizeFactorD = table.Column<decimal>(nullable: true),
                    VehicleSizeFactorE = table.Column<decimal>(nullable: true),
                    VehicleSizeFactorF = table.Column<decimal>(nullable: true),
                    VehicleSizeFactorM = table.Column<decimal>(nullable: true),
                    VehicleSizeFactorS = table.Column<decimal>(nullable: true),
                    WeatherCoefficient = table.Column<decimal>(nullable: true),
                    ContingencyCoefficient = table.Column<decimal>(nullable: true),
                    SignUpBonus = table.Column<string>(nullable: true),
                    MoldBonus = table.Column<string>(nullable: true),
                    CancellationOfSchedulePenalty = table.Column<string>(nullable: true),
                    PileRegistration = table.Column<decimal>(nullable: true),
                    DayPayroll = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Day = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlaceId = table.Column<long>(nullable: true),
                    TeamId = table.Column<long>(nullable: true),
                    WorkerId = table.Column<long>(nullable: true),
                    RuleId = table.Column<long>(nullable: true),
                    AutomaticallyGetWorkBeforeDate = table.Column<bool>(nullable: false),
                    AutomaticallyAcceptWorkWithinTheDay = table.Column<bool>(nullable: false),
                    BookStatus = table.Column<string>(maxLength: 15, nullable: true),
                    ReasonCancel = table.Column<string>(maxLength: 256, nullable: true),
                    TimeToCome = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeToGoHome = table.Column<DateTime>(type: "datetime", nullable: true),
                    CheckInTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CheckOutTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CheckInImage = table.Column<string>(maxLength: 256, nullable: true),
                    CheckInImageOriginalName = table.Column<string>(maxLength: 256, nullable: true),
                    CheckOutImage = table.Column<string>(maxLength: 256, nullable: true),
                    CheckOutImageOriginalName = table.Column<string>(maxLength: 256, nullable: true),
                    NumberOfRegisteredVehicles = table.Column<int>(nullable: true),
                    NumberOfVehiclesReRegistered = table.Column<int>(nullable: true),
                    NumberOfBonuses = table.Column<int>(nullable: true),
                    SuppliedMaterials = table.Column<string>(maxLength: 256, nullable: true),
                    SuppliesReturned = table.Column<string>(maxLength: 256, nullable: true),
                    ChemicalLevel = table.Column<string>(maxLength: 256, nullable: true),
                    ChemicalReturns = table.Column<string>(maxLength: 256, nullable: true),
                    UnexpectedBonus = table.Column<decimal>(nullable: true),
                    UnexpectedPenalty = table.Column<decimal>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: true),
                    AmountTransferred = table.Column<decimal>(nullable: true),
                    PileRegistration = table.Column<decimal>(nullable: true),
                    IncomeDeadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    MoneyTransferDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(maxLength: 200, nullable: true),
                    PlaceId = table.Column<long>(nullable: true),
                    IdentificationNumber = table.Column<string>(maxLength: 15, nullable: true),
                    DateRange = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProvincialLevel = table.Column<string>(maxLength: 256, nullable: true),
                    CurrentJob = table.Column<string>(maxLength: 256, nullable: true),
                    CurrentAgency = table.Column<string>(maxLength: 256, nullable: true),
                    CurrentAccommodation = table.Column<string>(maxLength: 256, nullable: true),
                    PictureOriginalName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Street",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetCode = table.Column<long>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Prefix = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CarId = table.Column<long>(nullable: true),
                    StaffId = table.Column<long>(nullable: true),
                    PriceId = table.Column<long>(nullable: true),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumberOfMonthsOfPurchase = table.Column<long>(nullable: true),
                    DateOfPayment = table.Column<DateTime>(type: "datetime", nullable: true),
                    Promotion = table.Column<string>(maxLength: 256, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamLead",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<long>(nullable: false),
                    TeamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlaces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceId = table.Column<long>(nullable: false),
                    TeamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ColorCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamWorker",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<long>(nullable: false),
                    TeamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamWorker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamZone",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneId = table.Column<long>(nullable: false),
                    TeamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamZone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    StreetId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    WardId = table.Column<long>(nullable: true),
                    Gender = table.Column<string>(maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Facebook = table.Column<string>(maxLength: 50, nullable: true),
                    Zalo = table.Column<string>(maxLength: 50, nullable: true),
                    GoogleId = table.Column<string>(maxLength: 255, nullable: true),
                    PictureUrl = table.Column<string>(maxLength: 255, nullable: true),
                    PhoneNumberOther = table.Column<string>(maxLength: 11, nullable: true),
                    FullName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ward",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardCode = table.Column<long>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Prefix = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ward", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Withdraws",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AmountOfWithdrawal = table.Column<string>(maxLength: 13, nullable: true),
                    AmountBeforeWithdrawal = table.Column<string>(maxLength: 13, nullable: true),
                    AmountAfterWithdrawal = table.Column<string>(maxLength: 13, nullable: true),
                    TimeToWithdraw = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceivingTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    WorkerId = table.Column<long>(nullable: true),
                    StaffId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerPlace",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<long>(nullable: false),
                    PlaceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerPlace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(maxLength: 200, nullable: true),
                    IdentificationNumber = table.Column<string>(maxLength: 15, nullable: true),
                    DateRange = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProvincialLevel = table.Column<string>(maxLength: 256, nullable: true),
                    CurrentJob = table.Column<string>(maxLength: 256, nullable: true),
                    CurrentAgency = table.Column<string>(maxLength: 256, nullable: true),
                    CurrentAccommodation = table.Column<string>(maxLength: 256, nullable: true),
                    Official = table.Column<bool>(nullable: false),
                    MoneyInWallet = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    PictureOriginalName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoneColumn",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneId = table.Column<long>(nullable: false),
                    ColumnId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneColumn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Subtitle = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    AttachmentFileReName = table.Column<string>(nullable: true),
                    AttachmentFileOriginalName = table.Column<string>(nullable: true),
                    History = table.Column<string>(maxLength: 256, nullable: true),
                    Chat = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PlaceId = table.Column<long>(nullable: true),
                    BasementId = table.Column<long>(nullable: true),
                    MapOfTunnelsInTheArea = table.Column<string>(nullable: true),
                    ColorCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_UserProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_UserProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UserProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_UserProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "UserProfile",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "UserProfile",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basements");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Street");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "TeamLead");

            migrationBuilder.DropTable(
                name: "TeamPlaces");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "TeamWorker");

            migrationBuilder.DropTable(
                name: "TeamZone");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Ward");

            migrationBuilder.DropTable(
                name: "Withdraws");

            migrationBuilder.DropTable(
                name: "WorkerPlace");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "ZoneColumn");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserProfile");
        }
    }
}
