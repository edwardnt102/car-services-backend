namespace Authentication.Common
{
    public class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }
        public static int GetUserId()
        {
            return 1;
        }
        public static class ErrorMessageCodes
        {
            public static readonly string ModelState = "99";
            public static readonly string ModelStateMessage = "Model State Invalid";
            public static readonly string Success = "200";
            public static readonly string SuccessMessage = "Successfully";

            public static readonly string NoRecordsFound = "01";
            public static readonly string NoRecordsFoundMessage = "No records found.";

            public static readonly string ValidationFailed = "02";
            public static readonly string ValidationFailedMessage = "Validations Failed.";

            public static readonly string ModelIsInvalid = "03";
            public static readonly string ModelIsInvalidMessage = "Model is invalid.";

            public static readonly string GeneralException = "04";
            public static readonly string GeneralExceptionMessage = "Some error occurred. Please contact your system administrator.";

            public static readonly string InvalidUserName = "05";
            public static readonly string InvalidUserNameMessage = "Invalid Username or Password.";

            public static readonly string UnauthorizedUser = "07";
            public static readonly string UnauthorizedUserMessage = "Bạn không có quyền truy cập. Chỉ có quản trị viên hoặc nhân viên mới có quyền truy cập. Vui lòng thử lại";

            public static readonly string KnowledgeDocumentAttachmentsNotFound = "08";
            public static readonly string KnowledgeDocumentAttachmentsNotFoundMessage = "No Attachments found.";

            public static readonly string UserNotFound = "09";
            public static readonly string UserNotFoundMessage = "Không tìm thấy người dùng.";
            public static readonly string UserInActive = "10";
            public static readonly string UserUserInActiveMessage = "Người dùng chưa được kích hoạt.";

            public static readonly string InternalServerError = "500";
            public static readonly string InternalServerErrorMessage = "Internal Server Error.";

            public static readonly string NotFound = "59";
            public static readonly string NotFoundMessage = "Not found.";

            public static readonly string UserInvalid = "11";
            public static readonly string UserInvalidMessage = "Tài khoản hoặc mật khẩu không đúng.";
        }

    }
}
