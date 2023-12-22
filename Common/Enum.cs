using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum SortOrder
    {
        [Display(Name = "asc")]
        Asc,
        [Display(Name = "desc")]
        Desc,
        [Display(Name = "none")]
        None
    }

    public enum StatusEnum
    {
        [Description("Pending Payment")]
        WaitForPay = 1,
        [Description("Paid")]
        Paid = 2,
        [Description("Pending")]
        Wait = 3,
        [Description("Approved")]
        Approved = 4,
        [Description("Cancel")]
        Cancel = 5,
        [Description("Hangon")]
        Hang = 6,
        [Description("Working")]
        Working = 7
    }

    public enum JobStatusEnum
    {
        [Description("Đang làm")]
        IN_PROGRESS = 1,
        [Description("Đã book")]
        BOOKED = 2,
        [Description("Xong")]
        DONE = 3,
        [Description("Chưa book")]
        TODO = 4
    }

    public enum WorkerType
    {
        [Description("Học việc")]
        INTERN = 1,
        [Description("Chính thức")]
        OFFICIAL = 2,
        [Description("Chuyên gia")]
        EXPERT = 3,
        [Description("Đội trưởng")]
        TEAMLEAD = 4
    }
}
