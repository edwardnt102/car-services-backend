using Common;
using Dapper;
using Entity.DBContext;
using Microsoft.Extensions.Configuration;
using Repository.Implements;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Dapper Transaction

        protected readonly IConfiguration Config;
        protected string CnStr = "Server=34.67.81.226;Database=car_service;user id=sa;password=Pass@word1";
        public string ConnectionString
        {
            get => CnStr;
        }
        protected IDbConnection Cn = null;
        public IDbConnection Connection
        {
            get => Cn;
        }

        protected DatabaseContext Context = null;
        public DatabaseContext DbContext
        {
            get => Context;
        }
        protected IDbTransaction Trans = null;
        public IDbTransaction Transaction
        {
            get => Trans;
        }
        public UnitOfWork(IConfiguration config, DatabaseContext context)
        {
            Config = config;
            CnStr = Config.GetConnectionString(ConnectionHelper.DatabaseName.DB5P10B.ToString());
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            Cn = new SqlConnection(CnStr);
            Context = context;
        }
        /// <summary>
        /// Open a transaction
        /// </summary>
        public async Task<IDbTransaction> OpenTransaction()
        {
            if (Trans != null)
                throw new Exception("A transaction is already open, you need to use a new DbContext for parallel job.");

            if (Cn.State == ConnectionState.Closed)
            {
                if (!(Cn is DbConnection))
                    throw new Exception("Connection object does not support OpenAsync.");

                await (Cn as DbConnection)?.OpenAsync();
            }

            Trans = Cn.BeginTransaction();

            return Trans;
        }


        /// <summary>
        /// Open a transaction with a specified isolation level
        /// </summary>
        public async Task<IDbTransaction> OpenTransaction(IsolationLevel level)
        {
            if (Trans != null)
                throw new Exception("A transaction is already open, you need to use a new DbContext for parallel job.");

            if (Cn.State == ConnectionState.Closed)
            {
                if (!(Cn is DbConnection))
                    throw new Exception("Connection object does not support OpenAsync.");

                await (Cn as DbConnection).OpenAsync();
            }

            Trans = Cn.BeginTransaction(level);

            return Trans;
        }

        /// <summary>
        /// Commit the current transaction, and optionally dispose all resources related to the transaction.
        /// </summary>
        public void CommitTransaction(bool disposeTrans = true)
        {
            if (Trans == null)
                throw new Exception("DB Transaction is not present.");

            Trans.Commit();
            if (disposeTrans) Trans.Dispose();
            if (disposeTrans) Trans = null;
        }

        /// <summary>
        /// Rollback the transaction and all the operations linked to it, and optionally dispose all resources related to the transaction.
        /// </summary>
        public void RollbackTransaction(bool disposeTrans = true)
        {
            if (Trans == null)
                throw new Exception("DB Transaction is not present.");

            Trans.Rollback();
            if (disposeTrans) Trans.Dispose();
            if (disposeTrans) Trans = null;
        }

        /// <summary>
        /// Will be call at the end of the service (ex : transient service in api net core)
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
            Trans?.Dispose();
            Cn?.Close();
            Cn?.Dispose();
        }

        #endregion

        #region Transaction

        public virtual void BeginTransaction() => Context.Database.BeginTransaction();

        public virtual void Commit() => Context.Database.CommitTransaction();

        public virtual void Rollback() => Context.Database.RollbackTransaction();

        public virtual int SaveChanges() => Context.SaveChanges();
        public virtual Task<int> SaveChangesAsync() => Context.SaveChangesAsync();
        #endregion
        /// <summary>
        /// UnitOfwork  for Areas
        /// </summary>

        private IZonesRepository _zonesRepository;

        public IZonesRepository ZonesRepository
        {
            get
            {
                return _zonesRepository ??= new ZonesRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Basements
        /// </summary>

        private IBasementsRepository _basementsRepository;

        public IBasementsRepository BasementsRepository
        {
            get
            {
                return _basementsRepository ??= new BasementsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Brands
        /// </summary>

        private IBrandsRepository _brandsRepository;

        public IBrandsRepository BrandsRepository
        {
            get
            {
                return _brandsRepository ??= new BrandsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for CarModels
        /// </summary>

        private ICarModelsRepository _carModelsRepository;

        public ICarModelsRepository CarModelsRepository
        {
            get
            {
                return _carModelsRepository ??= new CarModelsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Cars
        /// </summary>

        private ICarsRepository _carsRepository;

        public ICarsRepository CarsRepository
        {
            get
            {
                return _carsRepository ??= new CarsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Class
        /// </summary>

        private IClassRepository _classRepository;

        public IClassRepository ClassRepository
        {
            get
            {
                return _classRepository ??= new ClassRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Columns
        /// </summary>

        private IColumnsRepository _columnsRepository;

        public IColumnsRepository ColumnsRepository
        {
            get
            {
                return _columnsRepository ??= new ColumnsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Customers
        /// </summary>

        private ICustomersRepository _customersRepository;

        public ICustomersRepository CustomersRepository
        {
            get
            {
                return _customersRepository ??= new CustomersRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Jobs
        /// </summary>

        private IJobsRepository _jobsRepository;

        public IJobsRepository JobsRepository
        {
            get
            {
                return _jobsRepository ??= new JobsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Places
        /// </summary>

        private IPlacesRepository _placesRepository;

        public IPlacesRepository PlacesRepository
        {
            get
            {
                return _placesRepository ??= new PlacesRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Prices
        /// </summary>

        private IPricesRepository _pricesRepository;

        public IPricesRepository PricesRepository
        {
            get
            {
                return _pricesRepository ??= new PricesRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Project
        /// </summary>

        private IProjectRepository _projectRepository;

        public IProjectRepository ProjectRepository
        {
            get
            {
                return _projectRepository ??= new ProjectRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Province
        /// </summary>

        private IProvinceRepository _provinceRepository;

        public IProvinceRepository ProvinceRepository
        {
            get
            {
                return _provinceRepository ??= new ProvinceRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Rules
        /// </summary>

        private IRulesRepository _rulesRepository;

        public IRulesRepository RulesRepository
        {
            get
            {
                return _rulesRepository ??= new RulesRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Slots
        /// </summary>

        private ISlotsRepository _slotsRepository;

        public ISlotsRepository SlotsRepository
        {
            get
            {
                return _slotsRepository ??= new SlotsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Staffs
        /// </summary>

        private IStaffsRepository _staffsRepository;

        public IStaffsRepository StaffsRepository
        {
            get
            {
                return _staffsRepository ??= new StaffsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Street
        /// </summary>

        private IStreetRepository _streetRepository;

        public IStreetRepository StreetRepository
        {
            get
            {
                return _streetRepository ??= new StreetRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Subscriptions
        /// </summary>

        private ISubscriptionsRepository _subscriptionsRepository;

        public ISubscriptionsRepository SubscriptionsRepository
        {
            get
            {
                return _subscriptionsRepository ??= new SubscriptionsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Teams
        /// </summary>

        private ITeamsRepository _teamsRepository;

        public ITeamsRepository TeamsRepository
        {
            get
            {
                return _teamsRepository ??= new TeamsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Ward
        /// </summary>

        private IWardRepository _wardRepository;

        public IWardRepository WardRepository
        {
            get
            {
                return _wardRepository ??= new WardRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Withdraws
        /// </summary>

        private IWithdrawsRepository _withdrawsRepository;

        public IWithdrawsRepository WithdrawsRepository
        {
            get
            {
                return _withdrawsRepository ??= new WithdrawsRepository(this);
            }
        }

        /// <summary>
        /// UnitOfwork  for Workers
        /// </summary>

        private IWorkersRepository _workersRepository;

        public IWorkersRepository WorkersRepository
        {
            get
            {
                return _workersRepository ??= new WorkersRepository(this);
            }
        }
        /// <summary>
        /// UnitOfwork  for District
        /// </summary>

        private IDistrictRepository _districtRepository;

        public IDistrictRepository DistrictRepository
        {
            get
            {
                return _districtRepository ??= new DistrictRepository(this);
            }
        }

        private IWorkerPlaceRepository _workerPlaceRepository;

        public IWorkerPlaceRepository WorkerPlaceRepository
        {
            get
            {
                return _workerPlaceRepository ??= new WorkerPlaceRepository(this);
            }
        }

        private ITeamPlacesRepository _teamPlacesRepository;

        public ITeamPlacesRepository TeamPlacesRepository
        {
            get
            {
                return _teamPlacesRepository ??= new TeamPlacesRepository(this);
            }
        }

        private ITeamZoneRepository _teamZoneRepository;

        public ITeamZoneRepository TeamZoneRepository
        {
            get
            {
                return _teamZoneRepository ??= new TeamZoneRepository(this);
            }
        }

        private ITeamWorkerRepository _teamWorkerRepository;

        public ITeamWorkerRepository TeamWorkerRepository
        {
            get
            {
                return _teamWorkerRepository ??= new TeamWorkerRepository(this);
            }
        }

        private ITeamPlaceRepository _teamPlaceValueRepository;

        public ITeamPlaceRepository TeamPlaceValueRepository
        {
            get
            {
                return _teamPlaceValueRepository ??= new TeamPlaceValueRepository(this);
            }
        }

        private IZoneColumnRepository _zoneColumnRepository;

        public IZoneColumnRepository ZoneColumnRepository
        {
            get
            {
                return _zoneColumnRepository ??= new ZoneColumnRepository(this);
            }
        }

        private ITeamLeadRepository _teamLeadRepository;

        public ITeamLeadRepository TeamLeadRepository
        {
            get
            {
                return _teamLeadRepository ??= new TeamLeadRepository(this);
            }
        }

        private ICompanyRepository _companyRepository;

        public ICompanyRepository CompanyRepository
        {
            get
            {
                return _companyRepository ??= new CompanyRepository(this);
            }
        }

        private IB2BRepository _b2BRepository;

        public IB2BRepository B2BRepository
        {
            get
            {
                return _b2BRepository ??= new B2BRepository(this);
            }
        }

        private IColorCodeRepository _colorCodeRepository;

        public IColorCodeRepository ColorCodeRepository
        {
            get
            {
                return _colorCodeRepository ??= new ColorCodeRepository(this);
            }
        }

        private IDataTypeRepository _dataTypeRepository;

        public IDataTypeRepository DataTypeRepository
        {
            get
            {
                return _dataTypeRepository ??= new DataTypeRepository(this);
            }
        }

        private IUserProfileRepository _userProfileRepository;

        public IUserProfileRepository UserProfileRepository
        {
            get
            {
                return _userProfileRepository ??= new UserProfileRepository(this);
            }
        }

        private IStaffPlaceRepository _staffPlaceRepository;

        public IStaffPlaceRepository StaffPlaceRepository
        {
            get
            {
                return _staffPlaceRepository ??= new StaffPlaceRepository(this);
            }
        }
    }
}
