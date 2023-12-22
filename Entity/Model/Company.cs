using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Company")]
    public class Company : BaseEntity
    {
        [StringLength(256)]
        public string Logo { get; set; } //logo

        [StringLength(256)]
        public string Color { get; set; } //Color

        public string Banner { get; set; } //Color
    }
}
