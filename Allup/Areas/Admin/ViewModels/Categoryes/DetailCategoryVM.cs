using Allup.Models;

namespace Allup.Areas.Admin.ViewModels.Categoryes
{
    public class DetailCategoryVM
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
