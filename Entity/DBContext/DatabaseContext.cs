using Common.Shared;
using Entity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Entity.DBContext
{
    public class DatabaseContext : IdentityDbContext<AppUser>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;
        public DatabaseContext(DbContextOptions<DatabaseContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser)
          : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            modelBuilder.Entity<AppUser>()
           .ToTable("UserProfile");
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>(entity
                =>
            { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity
                =>
            { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity
                =>
            { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity
                =>
            { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity
                =>
            { entity.ToTable("RoleClaims"); });

            // var normalId = Guid.NewGuid().ToString();
            // var adminId = Guid.NewGuid().ToString();
            // var userId = Guid.NewGuid().ToString();
            // var staffId = Guid.NewGuid().ToString();
            // var customerId = Guid.NewGuid().ToString();
            // var workerId = Guid.NewGuid().ToString();
            // modelBuilder.Entity<IdentityRole>().HasData(
            //     new List<IdentityRole>
            //     {
            //         new IdentityRole{Name = Constants.Strings.RoleIdentifiers.Administrator,NormalizedName = Constants.Strings.RoleIdentifiers.Administrator.ToLower(), Id=adminId},
            //         new IdentityRole{Name = Constants.Strings.RoleIdentifiers.Normal,NormalizedName = Constants.Strings.RoleIdentifiers.Normal.ToLower(), Id=normalId},
            //         new IdentityRole{Name = Constants.Strings.RoleIdentifiers.Staff,NormalizedName = Constants.Strings.RoleIdentifiers.Staff.ToLower(), Id=staffId},
            //         new IdentityRole{Name = Constants.Strings.RoleIdentifiers.Customer,NormalizedName = Constants.Strings.RoleIdentifiers.Customer.ToLower(), Id=customerId},
            //         new IdentityRole{Name = Constants.Strings.RoleIdentifiers.Worker,NormalizedName = Constants.Strings.RoleIdentifiers.Worker.ToLower(), Id=workerId}
            //     }
            // );
            //
            // var hasher = new PasswordHasher<AppUser>();
            // var admin = new AppUser
            // {
            //     Id = userId,
            //     UserName = "HungHoang",
            //     NormalizedUserName = "HungHoang".ToUpper(),
            //     Email = "hunghvhpu@gmail.com",
            //     NormalizedEmail = "hunghvhpu@gmail.com".ToUpper(),
            //     EmailConfirmed = true,
            //     PasswordHash = hasher.HashPassword(null, "Pass@word1"),
            //     SecurityStamp = string.Empty,
            //     CreatedDate = DateTime.Now,
            //     FullName = "Hung Hoang",
            //     PictureUrl = "https://www.w3schools.com/w3images/avatar2.png",
            //     PhoneNumber = "0356226275",
            //     DateOfBirth = new DateTime(1991, 05, 28),
            //     Gender = "Male",
            //     IsDeleted = false,
            //     Active = true,
            //     Address = "Ha Dong, Ha Noi",
            // };
            // modelBuilder.Entity<AppUser>().HasData(admin);
            //
            // modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            // {
            //     RoleId = adminId,
            //     UserId = userId
            // });
        }

        public virtual DbSet<Zones> Areas { get; set; }
        public virtual DbSet<Basements> Basements { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<CarModels> CarModels { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Columns> Columns { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<Slots> Slots { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Subscriptions> Subscriptions { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Withdraws> Withdraws { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Street> Street { get; set; }
        public virtual DbSet<Ward> Ward { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<TeamPlaces> TeamPlaces { get; set; }
        public virtual DbSet<TeamZone> TeamZones { get; set; }
        public virtual DbSet<WorkerPlace> WorkerPlace { get; set; }
        public virtual DbSet<ZoneColumn> ZoneColumn { get; set; }
        public virtual DbSet<TeamWorker> TeamWorker { get; set; }
        public virtual DbSet<TeamLead> TeamLead { get; set; }
        public virtual DbSet<Company> Companys { get; set; }
        public virtual DbSet<B2B> B2B { get; set; }
        public virtual DbSet<ColorCode> ColorCode { get; set; }
        public virtual DbSet<DataType> DataType { get; set; }
        public virtual DbSet<StaffPlace> StaffPlace { get; set; }

        //public bool HasChanges => ChangeTracker.HasChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTime.Now;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        entry.Entity.IsDeleted = false;
                        entry.Entity.CompanyId = _authenticatedUser.CompanyId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = _dateTime.Now;
                        entry.Entity.ModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
