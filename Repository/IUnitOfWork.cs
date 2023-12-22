using System;
using System.Data;
using System.Threading.Tasks;
using Entity.DBContext;
using Repository.Implements;

namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }

        DatabaseContext DbContext { get; }


        IDbTransaction Transaction { get; }

        string ConnectionString { get; }

        Task<IDbTransaction> OpenTransaction();

        Task<IDbTransaction> OpenTransaction(IsolationLevel level);

        void CommitTransaction(bool disposeTrans = true);

        void RollbackTransaction(bool disposeTrans = true);

        void BeginTransaction();

        int SaveChanges();

        void Commit();

        void Rollback();

        Task<int> SaveChangesAsync();

        IZonesRepository ZonesRepository { get; }

        IBasementsRepository BasementsRepository { get; }

        IBrandsRepository BrandsRepository { get; }

        ICarModelsRepository CarModelsRepository { get; }

        ICarsRepository CarsRepository { get; }

        IClassRepository ClassRepository { get; }

        IColumnsRepository ColumnsRepository { get; }

        ICustomersRepository CustomersRepository { get; }

        IJobsRepository JobsRepository { get; }

        IPlacesRepository PlacesRepository { get; }

        IPricesRepository PricesRepository { get; }

        IProjectRepository ProjectRepository { get; }

        IProvinceRepository ProvinceRepository { get; }

        IRulesRepository RulesRepository { get; }

        ISlotsRepository SlotsRepository { get; }

        IStaffsRepository StaffsRepository { get; }

        IStreetRepository StreetRepository { get; }

        ISubscriptionsRepository SubscriptionsRepository { get; }

        ITeamsRepository TeamsRepository { get; }

        IWardRepository WardRepository { get; }

        IWithdrawsRepository WithdrawsRepository { get; }

        IWorkersRepository WorkersRepository { get; }

        IDistrictRepository DistrictRepository { get; }

        IWorkerPlaceRepository WorkerPlaceRepository { get; }

        ITeamPlacesRepository TeamPlacesRepository { get; }

        ITeamZoneRepository TeamZoneRepository { get; }

        ITeamWorkerRepository TeamWorkerRepository { get; }

        IZoneColumnRepository ZoneColumnRepository { get; }

        ITeamLeadRepository TeamLeadRepository { get; }

        ICompanyRepository CompanyRepository { get; }
        IB2BRepository B2BRepository { get; }
        IColorCodeRepository ColorCodeRepository { get; }
        IDataTypeRepository DataTypeRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        IStaffPlaceRepository StaffPlaceRepository { get; }
    }
}
