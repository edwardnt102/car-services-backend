using Common.Shared;
using Dapper;
using System.ComponentModel;

namespace Utility
{
    public class ModelUtility: IModelUtility
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IDateTimeService _dateTimeService;
        public ModelUtility(IAuthenticatedUserService authenticatedUserService, IDateTimeService dateTimeService)
        {
            _authenticatedUserService = authenticatedUserService;
            _dateTimeService = dateTimeService;
        }
        public DynamicParameters ObjectCreateToPrams<T>(T data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DynamicParameters p = new DynamicParameters();
            foreach (PropertyDescriptor prop in properties)
            {
                switch (prop.Name)
                {
                    case "ModifiedBy":
                        p.Add($"@ModifiedBy", _authenticatedUserService.UserId );
                        break;
                    case "ModifiedDate":
                        p.Add($"@ModifiedDate", _dateTimeService.Now);
                        break;
                    case "CreatedBy":
                        p.Add($"@CreatedBy", _authenticatedUserService.UserId);
                        break;
                    case "CreatedDate":
                        p.Add($"@CreatedDate", _dateTimeService.Now);
                        break;
                    case "IsDeleted":
                        p.Add($"@IsDeleted", false);
                        break;
                    default:
                        p.Add($"@{prop.Name}", prop.GetValue(data));
                        break;
                }
            }

            return p;
        }

        public DynamicParameters ObjectUpdateToPrams<T>(T data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DynamicParameters p = new DynamicParameters();
            foreach (PropertyDescriptor prop in properties)
            {
                switch (prop.Name)
                {
                    case "ModifiedBy":
                        p.Add($"@ModifiedBy", _authenticatedUserService.UserId);
                        break;
                    case "ModifiedDate":
                        p.Add($"@ModifiedDate", _dateTimeService.Now);
                        break;
                    default:
                        p.Add($"@{prop.Name}", prop.GetValue(data));
                        break;
                }
            }

            return p;
        }
    }

    public interface IModelUtility
    {
        DynamicParameters ObjectUpdateToPrams<T>(T data);
        DynamicParameters ObjectCreateToPrams<T>(T data);
    }
}
