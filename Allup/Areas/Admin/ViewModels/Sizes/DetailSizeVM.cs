using System.ComponentModel.DataAnnotations;
using Allup.Models;

namespace Allup.Areas.Admin.ViewModels.Sizes
{
    public class DetailSizeVM
    {



        [MaxLength(20)]
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }

    }
}
