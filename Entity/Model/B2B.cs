using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("B2B")]
    public class B2B : BaseEntity
    {
        public string CompanyShareds { get; set; } // danh sách công ty chia sẻ dữ liệu( có nghĩa là các công ty sẽ chia sẻ dữ liệu cho công ty đó)
        public string DataType { get; set; } // loại dữ liệu mà mình được share lấy từ TableShared
    }

    public enum TableShared
    {
        [Description("Cars")]
        Cars = 1,
        [Description("Customers")]
        Customers = 2,
        [Description("Subscriptions")]
        Subscriptions = 3,
        [Description("TeamPlaces")]
        TeamPlaces = 4,
        [Description("Teams")]
        Teams = 5,
        [Description("Jobs")]
        Jobs = 6,
        [Description("Slots")]
        Slots = 7
    }
}
