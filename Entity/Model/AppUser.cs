
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Serializable]
    [Table("Users")]
    public class AppUser : IdentityUser
    {
        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Gender { get; set; } //Giới tính

        public DateTime? DateOfBirth { get; set; } //Ngày tháng sinh

        public bool Active { get; set; }

        [StringLength(50)]
        public string Facebook { get; set; }

        [StringLength(50)]
        public string Zalo { get; set; }

        [StringLength(255)]
        public string GoogleId { get; set; }

        [StringLength(255)]
        public string PictureUrl { get; set; }

        [StringLength(11)]
        public string PhoneNumberOther { get; set; }

        [StringLength(255)]
        public string FullName { get; set; } //Họ Tên

    }
}
