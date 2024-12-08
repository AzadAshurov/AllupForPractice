using System.ComponentModel.DataAnnotations;
using Allup.Models;

namespace Allup.Areas.Admin.ViewModels.Colors
{
    public class DetailColor
    {


        [MaxLength(20)]
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductColor>? ProductColors { get; set; }
    }
}
