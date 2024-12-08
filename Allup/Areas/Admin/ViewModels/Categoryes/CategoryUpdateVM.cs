namespace Allup.Areas.Admin.ViewModels.Categoryes
{
    public class CategoryUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
