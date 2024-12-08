using Allup.Models;

namespace Allup.Areas.Admin.ViewModels.Colors
{
    public class GetColorAdminVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductColor>? ProductColors { get; set; }
    }
}
