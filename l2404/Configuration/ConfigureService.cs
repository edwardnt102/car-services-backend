using Authentication.Common;
using Authentication.Model;
using Entity.DBContext;
using Entity.Model;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Services.Shared;

namespace l2404.Configuration
{
    public static class ConfigureService
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public static void InitAuthentication(this IServiceCollection services)
        {
            var configuration = ServiceLocator.Current.GetInstance<IConfiguration>();
            // Register the ConfigurationBuilder instance of FacebookAuthSetting
            //services.Configure<FacebookAuthSettings>(configuration.GetSection(nameof(FacebookAuthSettings)));
            // Get options from app settings
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var a = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SigningKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });
            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            var appSettingOptions = configuration.GetSection(nameof(AppSettings));
            services.Configure<AppSettings>(options =>
            {
                options.JwtSecret = appSettingOptions[nameof(AppSettings.JwtSecret)];
                options.GoogleClientId = appSettingOptions[nameof(AppSettings.GoogleClientId)];
                options.GoogleClientSecret = appSettingOptions[nameof(AppSettings.GoogleClientSecret)];
                options.JwtEmailEncryption = appSettingOptions[nameof(AppSettings.JwtEmailEncryption)];
                options.DomainFile = appSettingOptions[nameof(AppSettings.DomainFile)];
            });

            // add identity
            var builderDuke = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            }).AddRoles<IdentityRole>();

            builderDuke = new IdentityBuilder(builderDuke.UserType, typeof(IdentityRole), builderDuke.Services);
            builderDuke.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
            //services.Configure<FacebookAuthSettings>(configuration.GetSection(nameof(FacebookAuthSettings)));
        }
    }
}
