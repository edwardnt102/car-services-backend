using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    [Table("Brands")]
    public class Brands : BaseEntity
    {
        [StringLength(256)]
        public string LinkRefer { get; set; } //Link tham khảo

        public string Logo { get; set; } //logo
    }
}
