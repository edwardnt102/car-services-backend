namespace Common.Constants
{
    public class Constants
    {
        public static string Upload = "/upload";
        public static string Excel = "/excel";
        public static string Semicolon = "; ";
        public static string SortDesc = "desc";
        public static string SortId = "Id";
        public static string Key = "@key";
        public static string Day = "@day";
        public static string Underlined = "_";
        public static string DdMMyyyyhhmmss = "ddMMyyyyhhmmss";
        public static string ddMMyyyy = "dd/MM/yyyy";
        public static string Source = "/";
        public static string PlaceId = "@PlaceId";
        public static string BasementId = "@BasementId";
        public static string TeamId = "@TeamId";
        public static string ZoneId = "@ZoneId";
        public static string BrandId = "@BrandId";
        public static string TeamName = "@TeamName";
        public static string WorkerName = "@WorkerName";
        public static string PlaceName = "@PlaceName";
        public static string BookStatus = "@BookStatus";
        public static string StatusIP = "@StatusIP";
        public static string StatusBK = "@StatusBK";
        public static string StatusDO = "@StatusDO";
        public static string WorkerType = "@WorkerType";
        public static string WorkerId = "@WorkerId";
        public static string CompanyId = "@CompanyId";
        public static string SlotId = "@SlotId";

        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }
            public static class RoleIdentifiers
            {


                public const string Normal = "Normal";
                public const string Administrator = "Administrator";
                public const string Customer = "Customer";
                public const string Staff = "Staff";
                public const string Worker = "Worker";
            }
            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }

        public static class JobStatus
        {
            public const string InProgress = "Ðang làm";
            public const string Booked = "Ðã book";
            public const string Done = "Xong";
        }


        public const string PasswordDefault = "1";

        public static int GetUserId()
        {
            return 1;
        }
        public static class ErrorMessageCodes
        {
            public static readonly string NotExistedMessage = "Không tìm thấy dữ liệu với Id";
            #region  District

            /// <summary>
            /// Response Message for District
            /// </summary>
            public static readonly int DistrictNotFound = 404;
            public static readonly string DistrictNotFoundMessage = "District Not Found";
            public static readonly int DistrictUpdateFail = 02;
            public static readonly string DistrictUpdateFailMessage = "Updating The District Failed";
            public static readonly int DistrictNotExisted = 03;
            public static readonly string DistrictNotExistedMessage = "District Not Existed";

            #endregion
            #region  Areas

            /// <summary>
            /// Response Message for Areas
            /// </summary>
            public static readonly int AreasNotFound = 404;
            public static readonly string AreasNotFoundMessage = "Areas Not Found";
            public static readonly int AreasUpdateFail = 02;
            public static readonly string AreasUpdateFailMessage = "Updating The Areas Failed";
            public static readonly int AreasNotExisted = 03;
            public static readonly string AreasNotExistedMessage = "Areas Not Existed";

            #endregion


            #region  Basements

            /// <summary>
            /// Response Message for Basements
            /// </summary>
            public static readonly int BasementsNotFound = 404;
            public static readonly string BasementsNotFoundMessage = "Basements Not Found";
            public static readonly int BasementsUpdateFail = 02;
            public static readonly string BasementsUpdateFailMessage = "Updating The Basements Failed";
            public static readonly int BasementsNotExisted = 03;
            public static readonly string BasementsNotExistedMessage = "Basements Not Existed";

            #endregion


            #region  Brands

            /// <summary>
            /// Response Message for Brands
            /// </summary>
            public static readonly int BrandsNotFound = 404;
            public static readonly string BrandsNotFoundMessage = "Brands Not Found";
            public static readonly int BrandsUpdateFail = 02;
            public static readonly string BrandsUpdateFailMessage = "Updating The Brands Failed";
            public static readonly int BrandsNotExisted = 03;
            public static readonly string BrandsNotExistedMessage = "Brands Not Existed";

            #endregion


            #region  CarModels

            /// <summary>
            /// Response Message for CarModels
            /// </summary>
            public static readonly int CarModelsNotFound = 404;
            public static readonly string CarModelsNotFoundMessage = "CarModels Not Found";
            public static readonly string CarNotProcess = "Xe chưa được book hoặc chưa bắt đầu làm việc.";
            public static readonly int CarModelsUpdateFail = 02;
            public static readonly string CarModelsUpdateFailMessage = "Updating The CarModels Failed";
            public static readonly int CarModelsNotExisted = 03;
            public static readonly string CarModelsNotExistedMessage = "CarModels Not Existed";

            #endregion


            #region  Cars

            /// <summary>
            /// Response Message for Cars
            /// </summary>
            public static readonly int CarsNotExisted = 03;
            public static readonly string CarsNotExistedMessage = "Cars Not Existed";
            public static readonly int LicensePlateNotFound = 01;
            public static readonly string LicensePlateNotFoundMessage = "Biển số xe không tồn tại trong hệ thống";
            public static readonly int SubNotFound = 04;
            public static readonly string SubNotFoundMessage = "Xe  chưa đăng ký chương trình chăm sóc dịch vụ";
            public static readonly int JobExisted = 05;
            public static readonly string JobExistedMessage = "Xe đã được đăng ký dịch vụ ngày hôm nay";
            public static readonly string JobHasCheckedMessage = "Xe đã được đăng ký hoặc không tồn tại ngày hôm nay";
            #endregion


            #region  Class

            /// <summary>
            /// Response Message for Class
            /// </summary>
            public static readonly int ClassNotFound = 404;
            public static readonly string ClassNotFoundMessage = "Class Not Found";
            public static readonly int ClassUpdateFail = 02;
            public static readonly string ClassUpdateFailMessage = "Updating The Class Failed";
            public static readonly int ClassNotExisted = 03;
            public static readonly string ClassNotExistedMessage = "Class Not Existed";

            #endregion


            #region  Columns

            /// <summary>
            /// Response Message for Columns
            /// </summary>
            public static readonly int ColumnsNotFound = 404;
            public static readonly string ColumnsNotFoundMessage = "Columns Not Found";
            public static readonly int ColumnsUpdateFail = 02;
            public static readonly string ColumnsUpdateFailMessage = "Updating The Columns Failed";
            public static readonly int ColumnsNotExisted = 03;
            public static readonly string ColumnsNotExistedMessage = "Columns Not Existed";

            #endregion


            #region  Customers

            /// <summary>
            /// Response Message for Customers
            /// </summary>
            public static readonly int CustomersNotFound = 404;
            public static readonly string CustomersNotFoundMessage = "Customers Not Found";
            public static readonly int CustomersUpdateFail = 02;
            public static readonly string CustomersUpdateFailMessage = "Updating The Customers Failed";
            public static readonly int CustomersNotExisted = 03;
            public static readonly string CustomersNotExistedMessage = "Customers Not Existed";

            #endregion


            #region  Jobs

            /// <summary>
            /// Response Message for Jobs
            /// </summary>
            public static readonly int JobsNotFound = 404;
            public static readonly string JobsNotFoundMessage = "Jobs Not Found";
            public static readonly int JobsUpdateFail = 02;
            public static readonly string JobsUpdateFailMessage = "Updating The Jobs Failed";
            public static readonly int JobsNotExisted = 03;
            public static readonly string JobsNotExistedMessage = "Jobs Not Existed";

            #endregion


            #region  Places

            /// <summary>
            /// Response Message for Places
            /// </summary>
            public static readonly int PlacesNotFound = 404;
            public static readonly string PlacesNotFoundMessage = "Places Not Found";
            public static readonly int PlacesUpdateFail = 02;
            public static readonly string PlacesUpdateFailMessage = "Updating The Places Failed";
            public static readonly int PlacesNotExisted = 03;
            public static readonly string PlacesNotExistedMessage = "Places Not Existed";

            #endregion


            #region  Prices

            /// <summary>
            /// Response Message for Prices
            /// </summary>
            public static readonly int PricesNotFound = 404;
            public static readonly string PricesNotFoundMessage = "Prices Not Found";
            public static readonly int PricesUpdateFail = 02;
            public static readonly string PricesUpdateFailMessage = "Updating The Prices Failed";
            public static readonly int PricesNotExisted = 03;
            public static readonly string PricesNotExistedMessage = "Prices Not Existed";

            #endregion


            #region  Project

            /// <summary>
            /// Response Message for Project
            /// </summary>
            public static readonly int ProjectNotFound = 404;
            public static readonly string ProjectNotFoundMessage = "Project Not Found";
            public static readonly int ProjectUpdateFail = 02;
            public static readonly string ProjectUpdateFailMessage = "Updating The Project Failed";
            public static readonly int ProjectNotExisted = 03;
            public static readonly string ProjectNotExistedMessage = "Project Not Existed";

            #endregion


            #region  Province

            /// <summary>
            /// Response Message for Province
            /// </summary>
            public static readonly int ProvinceNotFound = 404;
            public static readonly string ProvinceNotFoundMessage = "Province Not Found";
            public static readonly int ProvinceUpdateFail = 02;
            public static readonly string ProvinceUpdateFailMessage = "Updating The Province Failed";
            public static readonly int ProvinceNotExisted = 03;
            public static readonly string ProvinceNotExistedMessage = "Province Not Existed";

            #endregion


            #region  Rules

            /// <summary>
            /// Response Message for Rules
            /// </summary>
            public static readonly int RulesNotFound = 404;
            public static readonly string RulesNotFoundMessage = "Rules Not Found";
            public static readonly int RulesUpdateFail = 02;
            public static readonly string RulesUpdateFailMessage = "Updating The Rules Failed";
            public static readonly int RulesNotExisted = 03;
            public static readonly string RulesNotExistedMessage = "Rules Not Existed";
            public static readonly int RulesNotAvailable = 04;
            public static readonly string RulesNotAvailableMessage = "Rules Not Available";


            #endregion


            #region  Slots

            /// <summary>
            /// Response Message for Slots
            /// </summary>
            public static readonly int SlotsNotFound = 404;
            public static readonly string SlotsNotFoundMessage = "Slots Not Found";
            public static readonly int SlotsUpdateFail = 02;
            public static readonly string SlotsUpdateFailMessage = "Updating The Slots Failed";
            public static readonly int SlotsNotExisted = 03;
            public static readonly string SlotsNotExistedMessage = "Slots Not Existed";
            public static readonly string TeamLeadPermissionGetList = "Bạn không phải đội trưởng";

            #endregion


            #region  Staffs

            /// <summary>
            /// Response Message for Staffs
            /// </summary>
            public static readonly int StaffsNotFound = 404;
            public static readonly string StaffsNotFoundMessage = "Staffs Not Found";
            public static readonly int StaffsUpdateFail = 02;
            public static readonly string StaffsUpdateFailMessage = "Updating The Staffs Failed";
            public static readonly int StaffsNotExisted = 03;
            public static readonly string StaffsNotExistedMessage = "Staffs Not Existed";

            #endregion


            #region  Street

            /// <summary>
            /// Response Message for Street
            /// </summary>
            public static readonly int StreetNotFound = 404;
            public static readonly string StreetNotFoundMessage = "Street Not Found";
            public static readonly int StreetUpdateFail = 02;
            public static readonly string StreetUpdateFailMessage = "Updating The Street Failed";
            public static readonly int StreetNotExisted = 03;
            public static readonly string StreetNotExistedMessage = "Street Not Existed";

            #endregion


            #region  Subscriptions

            /// <summary>
            /// Response Message for Subscriptions
            /// </summary>
            public static readonly int SubscriptionsNotFound = 404;
            public static readonly string SubscriptionsNotFoundMessage = "Subscriptions Not Found";
            public static readonly int SubscriptionsUpdateFail = 02;
            public static readonly string SubscriptionsUpdateFailMessage = "Updating The Subscriptions Failed";
            public static readonly int SubscriptionsNotExisted = 03;
            public static readonly string SubscriptionsNotExistedMessage = "Subscriptions Not Existed";

            #endregion


            #region  Teams

            /// <summary>
            /// Response Message for Teams
            /// </summary>
            public static readonly int TeamsNotFound = 404;
            public static readonly string TeamsNotFoundMessage = "Teams Not Found";
            public static readonly int TeamsUpdateFail = 02;
            public static readonly string TeamsUpdateFailMessage = "Updating The Teams Failed";
            public static readonly int TeamsNotExisted = 03;
            public static readonly string TeamsNotExistedMessage = "Teams Not Existed";

            #endregion


            #region  Ward

            /// <summary>
            /// Response Message for Ward
            /// </summary>
            public static readonly int WardNotFound = 404;
            public static readonly string WardNotFoundMessage = "Ward Not Found";
            public static readonly int WardUpdateFail = 02;
            public static readonly string WardUpdateFailMessage = "Updating The Ward Failed";
            public static readonly int WardNotExisted = 03;
            public static readonly string WardNotExistedMessage = "Ward Not Existed";

            #endregion


            #region  Withdraws

            /// <summary>
            /// Response Message for Withdraws
            /// </summary>
            public static readonly int WithdrawsNotFound = 404;
            public static readonly string WithdrawsNotFoundMessage = "Withdraws Not Found";
            public static readonly int WithdrawsUpdateFail = 02;
            public static readonly string WithdrawsUpdateFailMessage = "Updating The Withdraws Failed";
            public static readonly int WithdrawsNotExisted = 03;
            public static readonly string WithdrawsNotExistedMessage = "Withdraws Not Existed";

            #endregion


            #region  Workers

            /// <summary>
            /// Response Message for Workers
            /// </summary>
            public static readonly int WorkersNotFound = 404;
            public static readonly string WorkersNotFoundMessage = "Workers Not Found";
            public static readonly int WorkersUpdateFail = 02;
            public static readonly string WorkersUpdateFailMessage = "Updating The Workers Failed";
            public static readonly int WorkersNotExisted = 03;
            public static readonly string WorkersNotExistedMessage = "Workers Not Existed";

            #endregion

            #region Common
            public static readonly int Success = 200;
            public static readonly string SuccessMessage = "Successfully";
            public static readonly int InternalServerError = 500;
            public static readonly string InternalServerErrorMessage = "Internal Server Error";
            public static readonly int IsNullOrEmpty = 97;
            public static readonly string IsNullOrEmptyMessage = "Parameters Is Null Or Empty";
            public static readonly int ModelState = 99;
            public static readonly string ModelStateMessage = "Model State Invalid";
            public static readonly int RecordNotFound = 404;
            public static readonly string RecordNotFoundMessage = "Không tìm thấy bản ghi.";
            public static readonly int ModelIsInvalid = 99;
            public static readonly string ModelIsInvalidMessage = "Yêu cầu không hợp lệ.";
            public static readonly int GeneralException = 9;
            public static readonly string GeneralExceptionMessage = "Some error occurred. Please contact your system administrator";
            public static readonly int ValidationFailed = 92;
            public static readonly string ValidationFailedMessage = "Validations Failed";
            public static readonly int UnauthorizedUser = 94;
            public static readonly string UnauthorizedUserMessage = "Unauthorized User";
            public static readonly int InvalidCondition = 95;
            public static readonly string InvalidConditionMessage = "Độ dài chuỗi tìm kiếm phải lớn hơn 2";
            public static readonly string SearchModelInvalid = "Search Model Invalid";
            public static readonly string UserAlreadyExists = "Tài khoản này đã tồn tại";
            public static readonly string UserCreateFail = "Tài khoản tạo thất bại, vui lòng kiểm tra lại";
            public static readonly string UserCreateSuccess = "Tài khoản tạo thành công";
            public static readonly string SaveFail = "Lưu thông tin thất bại, vui lòng kiểm tra lại";
            public static readonly string SaveSuccess = "Lưu thông tin thành công";
            public static readonly string UpdateAvartarFail = "Tải ảnh lên thất bại, vui lòng kiểm tra lại";
            public static readonly string FileNotFound = "Không tìm thấy file";
            public static readonly string IdNotFound = "Không tìm thấy Id";
            public static readonly string UserIdNotFound = "Không tìm thấy UserId";
            public static readonly string DeleteSuccess = "Xóa thông tin thành công";
            public static readonly string UpdateSuccessMessage = "Cập nhật thông tin thành công";
            public static readonly string CreateSlotSuccess = "Bạn đã tạo buổi làm thành công";
            public static readonly string CreateSlotFail = "Bạn đã tạo buổi làm thất bại";
            public static readonly string CreateSlotExist = "Buổi làm đã tồn tại, tạo mới buổi làm thất bại";
            public static readonly string SaveFileFail = "Đã xảy ra lỗi trong quá trình lưu file xuống folder, vui lòng kiểm tra lại file và đường dẫn";
            public static readonly string UpdateSlotSuccess = "Bạn đã cập nhật buổi làm thành công";
            public static readonly string UpdateSlotFail = "Bạn đã cập nhật buổi làm thất bại";
            public static readonly string CancelSlotSuccess = "Bạn đã hủy buổi làm thành công";
            public static readonly string CancelSlotFail = "Bạn đã hủy buổi làm thất bại";
            public static readonly string CheckInSlotSuccess = "Bạn đã điểm danh buổi làm thành công";
            public static readonly string CheckInSlotFail = "Bạn đã điểm danh buổi làm thất bại";
            public static readonly string SlotNotApproved = "Bạn đã điểm danh buổi làm thất bại. Buổi làm chưa được duyệt";
            public static readonly string TeamNotFound = "Không tìm thấy bản ghi nào trong danh sách đội team, vui lòng kiểm tra lại";
            public static readonly string PlaceNotFound = "Không tìm thấy bản ghi nào trong danh sách đội tòa nhà, vui lòng kiểm tra lại";
            public static readonly string WorkerNotFound = "Không tìm thấy nhân viên lao động, vui lòng kiểm tra lại";
            public static readonly string InActiveUserSuccess = "Khóa tài khoản thành công";
            public static readonly string ActiveUserSuccess = "Mở tài khoản thành công";
            public static readonly string InActiveUserFail = "Khóa tài khoản thất bại";
            public static readonly string ApproveSlotSuccess = "Duyệt buổi làm thành công";
            public static readonly string ApproveSlotFail = "Duyệt buổi làm thất bại";
            public static readonly string CheckOutSlotSuccess = "Bạn đã điểm danh ra về buổi làm thành công";
            public static readonly string CheckOutSlotFail = "Bạn đã điểm danh ra về buổi làm thất bại";
            public static readonly string LicensePlatesDuplicate = "Biển số xe đã tồn tại, vui lòng kiểm tra lại";
            public static readonly string UploadSuccess = "Upload thông tin thành công";
            public static readonly string UploadFail = "Upload thông tin thất bại";
            public static readonly string FileCorrectFormat = "File tải lên chưa đúng định dạng. Hệ thống chỉ tiếp nhận file có đuôi là xls, xlsx, csv";
            #endregion

            #region Account

            public static readonly string InvalidUserName = "05";
            public static readonly string InvalidUserNameMessage = "Invalid Username or Password";

            public static readonly string KnowledgeDocumentAttachmentsNotFound = "08";
            public static readonly string KnowledgeDocumentAttachmentsNotFoundMessage = "No Attachments found";

            public static readonly string UserNotFound = "09";
            public static readonly string UserNotFoundMessage = "Không tìm thấy người dùng";
            public static readonly string UserInActive = "10";
            public static readonly string UserUserInActiveMessage = "Người dùng chưa được kích hoạt";
            public static readonly string UserExisted = "11";
            public static readonly string UserExistedMessage = "Email đã được sử dụng trong hệ thống, Vui lòng sử dụng email khác";
            public static readonly string UserNotCreated = "12";
            public static readonly string UserNotCreatedMessage = "Người dùng không được thêm thành công, thử lại lần nữa";

            public static readonly string RoleExisted = "13";
            public static readonly string RoleExistedMessage = "Role had been existed";
            public static readonly string RoleNotExisted = "14";
            public static readonly string RoleNotExistedMessage = "Role had been not existed";

            public static readonly string SendSmsFail = "15";
            public static readonly string SendSmsFailMessage = "Gửi tin nhắn không thành công";

            public static readonly string SmsTypeNotExisted = "17";
            public static readonly string SmsTypeNotExistedMessage = "Loại tin nhắn không tồn tại trong hệ thống";

            public static readonly string SmsTemplateNotExisted = "18";
            public static readonly string SmsTemplateNotExistedMessage = "Mẫu tin nhắn không tồn tại trong hệ thống";

            public static readonly string InvalidInvoice = "19";
            public static readonly string InvalidInvoiceMessage = "Hóa đơn không tồn tại";

            public static readonly string InvalidProduct = "20";
            public static readonly string InvalidProductMessage = "Sản phẩm không tồn tại";

            public static readonly int ChangePasswordFail = 23;
            public static readonly string ChangePasswordFailMessage = "Mật khẩu cũ không chính xác.";

            public static readonly string InvalidAccount = "21";
            public static readonly string InvalidAccountMessage = "Tài khoản không tồn tại";

            public static readonly string InvalidRole = "22";
            public static readonly string InvalidRoleMessage = "Quyền hạn không tồn tại";

            public static readonly string CanNotFindById = "Không tìm thấy Id";

            #endregion

        }

        public static class Statuses
        {
            public static readonly int Approved = 4;
            public static readonly int Submited = 2;
            public static readonly int Draft = 1;
            public static readonly int Ammend = 3;
            public static readonly int Rejected = 5;
        }

        public static class Months
        {
            public static readonly string Mon1 = "Jan";
            public static readonly string Mon2 = "Feb";
            public static readonly string Mon3 = "Mar";
            public static readonly string Mon4 = "Apr";
            public static readonly string Mon5 = "May";
            public static readonly string Mon6 = "Jun";
            public static readonly string Mon7 = "Jul";
            public static readonly string Mon8 = "Aug";
            public static readonly string Mon9 = "Sep";
            public static readonly string Mon10 = "Oct";
            public static readonly string Mon11 = "Nov";
            public static readonly string Mon12 = "Dec";
        }
        public static class LocationTypes
        {
            public static readonly string Province = "tinh";
            public static readonly string City = "thanh-pho";
        }
    }
}
