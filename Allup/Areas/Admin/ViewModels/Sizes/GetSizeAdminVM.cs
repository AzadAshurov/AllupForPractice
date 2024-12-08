using Allup.Models;

namespace Allup.Areas.Admin.ViewModels.Sizes
{
    public class GetSizeAdminVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
    }
}
