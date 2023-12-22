using Entity.Model;

namespace Repository.Implements
{
    #region  Workers

    /// <summary>
    /// Repository interface for Workers
    /// </summary>
    public interface IWorkersRepository : IRepository<Workers>
    {
    }
    /// <summary>
    /// Repository class for Workers
    /// </summary>
    public class WorkersRepository : Repository<Workers>, IWorkersRepository
    {
        public WorkersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

    #region  Customers

    /// <summary>
    /// Repository interface for Customers
    /// </summary>
    public interface ICustomersRepository : IRepository<Customers>
    {
    }
    /// <summary>
    /// Repository class for Customers
    /// </summary>
    public class CustomersRepository : Repository<Customers>, ICustomersRepository
    {
        public CustomersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

    #region  Staffs

    /// <summary>
    /// Repository interface for Staffs
    /// </summary>
    public interface IStaffsRepository : IRepository<Staffs>
    {
    }
    /// <summary>
    /// Repository class for Staffs
    /// </summary>
    public class StaffsRepository : Repository<Staffs>, IStaffsRepository
    {
        public StaffsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

    #region  Areas

    /// <summary>
    /// Repository interface for Areas
    /// </summary>
    public interface IZonesRepository : IRepository<Zones>
    {
    }
    /// <summary>
    /// Repository class for Areas
    /// </summary>
    public class ZonesRepository : Repository<Zones>, IZonesRepository
    {
        public ZonesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Basements

    /// <summary>
    /// Repository interface for Basements
    /// </summary>
    public interface IBasementsRepository : IRepository<Basements>
    {
    }
    /// <summary>
    /// Repository class for Basements
    /// </summary>
    public class BasementsRepository : Repository<Basements>, IBasementsRepository
    {
        public BasementsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Brands

    /// <summary>
    /// Repository interface for Brands
    /// </summary>
    public interface IBrandsRepository : IRepository<Brands>
    {
    }
    /// <summary>
    /// Repository class for Brands
    /// </summary>
    public class BrandsRepository : Repository<Brands>, IBrandsRepository
    {
        public BrandsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  CarModels

    /// <summary>
    /// Repository interface for CarModels
    /// </summary>
    public interface ICarModelsRepository : IRepository<CarModels>
    {
    }
    /// <summary>
    /// Repository class for CarModels
    /// </summary>
    public class CarModelsRepository : Repository<CarModels>, ICarModelsRepository
    {
        public CarModelsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Cars

    /// <summary>
    /// Repository interface for Cars
    /// </summary>
    public interface ICarsRepository : IRepository<Cars>
    {
    }
    /// <summary>
    /// Repository class for Cars
    /// </summary>
    public class CarsRepository : Repository<Cars>, ICarsRepository
    {
        public CarsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Class

    /// <summary>
    /// Repository interface for Class
    /// </summary>
    public interface IClassRepository : IRepository<Class>
    {
    }
    /// <summary>
    /// Repository class for Class
    /// </summary>
    public class ClassRepository : Repository<Class>, IClassRepository
    {
        public ClassRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Columns

    /// <summary>
    /// Repository interface for Columns
    /// </summary>
    public interface IColumnsRepository : IRepository<Columns>
    {
    }
    /// <summary>
    /// Repository class for Columns
    /// </summary>
    public class ColumnsRepository : Repository<Columns>, IColumnsRepository
    {
        public ColumnsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  District

    /// <summary>
    /// Repository interface for District
    /// </summary>
    public interface IDistrictRepository : IRepository<District>
    {
    }
    /// <summary>
    /// Repository class for District
    /// </summary>
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        public DistrictRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Jobs

    /// <summary>
    /// Repository interface for Jobs
    /// </summary>
    public interface IJobsRepository : IRepository<Jobs>
    {
    }
    /// <summary>
    /// Repository class for Jobs
    /// </summary>
    public class JobsRepository : Repository<Jobs>, IJobsRepository
    {
        public JobsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Places

    /// <summary>
    /// Repository interface for Places
    /// </summary>
    public interface IPlacesRepository : IRepository<Places>
    {
    }
    /// <summary>
    /// Repository class for Places
    /// </summary>
    public class PlacesRepository : Repository<Places>, IPlacesRepository
    {
        public PlacesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Prices

    /// <summary>
    /// Repository interface for Prices
    /// </summary>
    public interface IPricesRepository : IRepository<Prices>
    {
    }
    /// <summary>
    /// Repository class for Prices
    /// </summary>
    public class PricesRepository : Repository<Prices>, IPricesRepository
    {
        public PricesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Project

    /// <summary>
    /// Repository interface for Project
    /// </summary>
    public interface IProjectRepository : IRepository<Project>
    {
    }
    /// <summary>
    /// Repository class for Project
    /// </summary>
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Province

    /// <summary>
    /// Repository interface for Province
    /// </summary>
    public interface IProvinceRepository : IRepository<Province>
    {
    }
    /// <summary>
    /// Repository class for Province
    /// </summary>
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        public ProvinceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Rules

    /// <summary>
    /// Repository interface for Rules
    /// </summary>
    public interface IRulesRepository : IRepository<Rules>
    {
    }
    /// <summary>
    /// Repository class for Rules
    /// </summary>
    public class RulesRepository : Repository<Rules>, IRulesRepository
    {
        public RulesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Slots

    /// <summary>
    /// Repository interface for Slots
    /// </summary>
    public interface ISlotsRepository : IRepository<Slots>
    {
    }
    /// <summary>
    /// Repository class for Slots
    /// </summary>
    public class SlotsRepository : Repository<Slots>, ISlotsRepository
    {
        public SlotsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Street

    /// <summary>
    /// Repository interface for Street
    /// </summary>
    public interface IStreetRepository : IRepository<Street>
    {
    }
    /// <summary>
    /// Repository class for Street
    /// </summary>
    public class StreetRepository : Repository<Street>, IStreetRepository
    {
        public StreetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Subscriptions

    /// <summary>
    /// Repository interface for Subscriptions
    /// </summary>
    public interface ISubscriptionsRepository : IRepository<Subscriptions>
    {
    }
    /// <summary>
    /// Repository class for Subscriptions
    /// </summary>
    public class SubscriptionsRepository : Repository<Subscriptions>, ISubscriptionsRepository
    {
        public SubscriptionsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Teams

    /// <summary>
    /// Repository interface for Teams
    /// </summary>
    public interface ITeamsRepository : IRepository<Teams>
    {
    }
    /// <summary>
    /// Repository class for Teams
    /// </summary>
    public class TeamsRepository : Repository<Teams>, ITeamsRepository
    {
        public TeamsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Ward

    /// <summary>
    /// Repository interface for Ward
    /// </summary>
    public interface IWardRepository : IRepository<Ward>
    {
    }
    /// <summary>
    /// Repository class for Ward
    /// </summary>
    public class WardRepository : Repository<Ward>, IWardRepository
    {
        public WardRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion


    #region  Withdraws

    /// <summary>
    /// Repository interface for Withdraws
    /// </summary>
    public interface IWithdrawsRepository : IRepository<Withdraws>
    {
    }
    /// <summary>
    /// Repository class for Withdraws
    /// </summary>
    public class WithdrawsRepository : Repository<Withdraws>, IWithdrawsRepository
    {
        public WithdrawsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    #endregion

    public interface IWorkerPlaceRepository : IRepository<WorkerPlace>
    {
    }

    public class WorkerPlaceRepository : Repository<WorkerPlace>, IWorkerPlaceRepository
    {
        public WorkerPlaceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface ITeamPlacesRepository : IRepository<TeamPlaces>
    {
    }

    public class TeamPlacesRepository : Repository<TeamPlaces>, ITeamPlacesRepository
    {
        public TeamPlacesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface ITeamZoneRepository : IRepository<TeamZone>
    {
    }

    public class TeamZoneRepository : Repository<TeamZone>, ITeamZoneRepository
    {
        public TeamZoneRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface ITeamWorkerRepository : IRepository<TeamWorker>
    {
    }

    public class TeamWorkerRepository : Repository<TeamWorker>, ITeamWorkerRepository
    {
        public TeamWorkerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface ITeamPlaceRepository : IRepository<TeamPlaces>
    {
    }
    /// <summary>
    /// Repository class for TeamZoneValue
    /// </summary>
    public class TeamPlaceValueRepository : Repository<TeamPlaces>, ITeamPlaceRepository
    {
        public TeamPlaceValueRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface IZoneColumnRepository : IRepository<ZoneColumn>
    {
    }

    public class ZoneColumnRepository : Repository<ZoneColumn>, IZoneColumnRepository
    {
        public ZoneColumnRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface ITeamLeadRepository : IRepository<TeamLead>
    {
    }

    public class TeamLeadRepository : Repository<TeamLead>, ITeamLeadRepository
    {
        public TeamLeadRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface ICompanyRepository : IRepository<Company>
    {
    }

    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface IB2BRepository : IRepository<B2B>
    {
    }

    public class B2BRepository : Repository<B2B>, IB2BRepository
    {
        public B2BRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface IColorCodeRepository : IRepository<ColorCode>
    {
    }

    public class ColorCodeRepository : Repository<ColorCode>, IColorCodeRepository
    {
        public ColorCodeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface IUserProfileRepository : IRepository<AppUser>
    {
    }

    public class UserProfileRepository : Repository<AppUser>, IUserProfileRepository
    {
        public UserProfileRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface IDataTypeRepository : IRepository<DataType>
    {
    }

    public class DataTypeRepository : Repository<DataType>, IDataTypeRepository
    {
        public DataTypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public interface IStaffPlaceRepository : IRepository<StaffPlace>
    {
    }

    public class StaffPlaceRepository : Repository<StaffPlace>, IStaffPlaceRepository
    {
        public StaffPlaceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

}
