using System.ComponentModel.DataAnnotations;

namespace Allup.Models
{
    public class Color : BaseEntity
    {
        [Required(ErrorMessage = "Empty area")]
        [MaxLength(30, ErrorMessage = "Limit of length is 30")]
        public string Name { get; set; }

        public List<ProductColor> ProductColors { get; set; }

    }
}
