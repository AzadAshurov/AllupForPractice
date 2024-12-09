using System.ComponentModel.DataAnnotations;

namespace Allup.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Empty area")]
        [MaxLength(30, ErrorMessage = "Limit of length is 30")]
        public string Name { get; set; }

        //relation
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
