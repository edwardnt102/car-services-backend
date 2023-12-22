using AutoMapper;
using Entity.Model;
using ViewModel.RequestModel;
using ViewModel.RequestModel.Basements;
using ViewModel.RequestModel.Teams;
using ViewModel.RequestModel.Upload;
using ViewModel.RequestModel.Zone;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Teams, TeamsModel>();
            CreateMap<TeamsModel, Teams>();
            CreateMap<Zones, ZoneSaveModel>();
            CreateMap<ZoneSaveModel, Zones>();
            CreateMap<Basements, BasementSaveModel>();
            CreateMap<BasementSaveModel, Basements>();
            CreateMap<Places, Places>();
            CreateMap<BookJobRequestModel, Jobs>()
                .ForMember(d => d.Title, t => t.MapFrom(src => src.Title))
                .ForMember(d => d.Subtitle, t => t.MapFrom(src => src.SubTitle))
                .ForMember(d => d.BookJobDate, t => t.MapFrom(src => src.BookJobDate))
                .ForMember(d => d.SlotSupport, t => t.MapFrom(src => src.SlotSupport))
                .ForMember(d => d.SlotInCharge, t => t.MapFrom(src => src.SlotInCharge))
                .ForMember(d => d.ColumnId, t => t.MapFrom(src => src.ColumnId));
            CreateMap<B2BUpload, B2B>();
            CreateMap<BasementUpload, Basements>();
            CreateMap<AppUserUpload, AppUser>();
            CreateMap<BrandUpload, Brands>();
            CreateMap<CarModelUpload, CarModels>();
            CreateMap<CarUpload, Cars>();
            CreateMap<ClassUpload, Class>();
            CreateMap<ColorCodeUpload, ColorCode>();
            CreateMap<ColumnUpload, Columns>();
            CreateMap<CompanyUpload, Company>();
            CreateMap<CustomerUpload, Customers>();
            CreateMap<DataTypeUpload, DataType>();
            CreateMap<JobUpload, Jobs>();
            CreateMap<PlaceUpload, Places>();
            CreateMap<PriceUpload, Prices>();
            CreateMap<RuleUpload, Rules>();
            CreateMap<SlotUpload, Slots>();
            CreateMap<StaffUpload, Staffs>();
            CreateMap<StaffPlaceUpload, StaffPlace>();
            CreateMap<SubscriptionUpload, Subscriptions>();
            CreateMap<TeamLeadUpload, TeamLead>();
            CreateMap<TeamPlaceUpload, TeamPlaces>();
            CreateMap<TeamUpload, Teams>();
            CreateMap<TeamWorkerUpload, TeamWorker>();
            CreateMap<TeamZoneUpload, TeamZone>();
            CreateMap<WithdrawUpload, Withdraws>();
            CreateMap<WorkerPlaceUpload, WorkerPlace>();
            CreateMap<WorkerUpload, Workers>();
            CreateMap<ZoneColumnUpload, ZoneColumn>();
            CreateMap<ZoneUpload, Zones>();
        }
    }
}
