using System.ComponentModel.DataAnnotations;

namespace Allup.Areas.Admin.ViewModels.Colors
{
    public class UpdateColorVM
    {
        [MaxLength(20)]
        public string Name { get; set; }
        public int Id { get; set; }
        public List<int> ColorIds { get; set; } = new List<int>();

    }
}
