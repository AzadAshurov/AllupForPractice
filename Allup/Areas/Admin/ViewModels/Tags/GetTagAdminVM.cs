using Allup.Models;

namespace Allup.Areas.Admin.ViewModels
{
    public class GetTagAdminVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductTag>? ProductTags { get; set; }
    }
}
